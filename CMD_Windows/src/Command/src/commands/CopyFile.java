package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
import commandInterface.DirectoryFunctionContainer;
import constant.CommandResultType;
import constant.ItemType;
import constant.OverwriteType;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import java.util.ArrayList;
import java.util.Scanner;

public class CopyFile extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "copy");
        String[] commandToken = getCommandToken(command);
        CommandResultType commandResult = isCommandValid(commandToken);

        if(commandResult == CommandResultType.SUCCESS) {
            File sourceFile = new File(commandToken[1]);
            File destinationFile = new File(promptData.getCurrentAbsolutePath());

            if(commandToken.length == 3) {
                destinationFile = new File(commandToken[2]);
            }

            copyFile(promptData, sourceFile, destinationFile);
        }

        else if(commandResult == CommandResultType.COMMAND_NOT_VALID) {
            if (commandToken.length == 1) {
                PromptView.getInstance().printMessage("지정된 파일을 찾을 수 없습니다.");
            }

            else if (commandToken.length > 3) {
                PromptView.getInstance().printMessage("명령 구문이 올바르지 않습니다.");
            }
        }

        else if(commandResult == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        if(commandToken[0].equals("copy")) {
            if(commandToken.length == 1 || commandToken.length > 3) {
                return CommandResultType.COMMAND_NOT_VALID;
            }

            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    public void copyFile(PromptData promptData, File source, File destination) {
        if (!source.isAbsolute()) {
            source = toAbsoluteFile(promptData, source);
        }

        if (!destination.isAbsolute()) {
            destination = toAbsoluteFile(promptData, destination);
        }

        ItemType sourceType = ItemVerifier.getInstance().getItemState(source.getPath());
        ItemType destinationType = ItemVerifier.getInstance().getItemState(destination.getPath());

        int copiedFiles = 0;

        if(source.equals(destination)) {
            File[] sourceFiles = getAllFile(source.getPath());

            String actualPath = ItemVerifier.getInstance().getActualPath(sourceFiles[0].getPath());
            PromptView.getInstance().printMessage(actualPath);
            PromptView.getInstance().printMessage("같은 파일로 복사할 수 없습니다.");
            PromptView.getInstance().printMessage("        " + String.valueOf(copiedFiles) + "개 파일이 복사되었습니다.");
            return;
        }

        if(sourceType == ItemType.IS_DIRECTORY) {
            File[] sourceFiles = getAllFile(source.getPath());

            if(sourceFiles.length == 0) {
                PromptView.getInstance().printMessage(source.toPath().getFileName() + "\\*");
                PromptView.getInstance().printMessage("지정된 파일을 찾을 수 없습니다.");
            }

            if(destinationType == ItemType.IS_FILE) {
                copiedFiles = copyDirectoryToFile(sourceFiles, destination);
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                copiedFiles = copyDirectoryToDirectory(sourceFiles, destination);
            }

            PromptView.getInstance().printMessage("        " + String.valueOf(copiedFiles) + "개 파일이 복사되었습니다.");
        }

        else if(sourceType == ItemType.IS_FILE) {
            if(destinationType == ItemType.IS_FILE) {
                copyFileToFile(source, destination, OverwriteType.NO);
                copiedFiles += 1;
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                copiedFiles = copyFileToDirectory(source, destination);
            }

            else {
                copiedFiles = copyFileToEmptySpace(source, destination);
            }

            PromptView.getInstance().printMessage("        " + String.valueOf(copiedFiles) + "개 파일이 복사되었습니다.");
        }

        else {
            PromptView.getInstance().printMessage("지정된 파일을 찾을 수 없습니다.");
        }
    }

    public boolean isYesNoAll(String answer) {
       return (!answer.toLowerCase().startsWith("y") &&
                !answer.toLowerCase().startsWith("n") && !answer.toLowerCase().startsWith("a"));
    }

    private int copyDirectoryToFile(File[] sourceFiles, final File destination) {
        StringBuilder result = new StringBuilder();
        int copiedFiles = 0;

        try {
            for (File file : sourceFiles) {
                BufferedReader reader = new BufferedReader(new FileReader(file.getPath()));
                String line;

                while ((line = reader.readLine()) != null) {
                    result.append(line).append("\n");
                }
            }

            File resultFile = new File(destination.getPath() + ".temp");
            BufferedWriter writer = new BufferedWriter(new FileWriter(resultFile.getPath()));

            writer.write(result.toString());
            writer.close();

            OverwriteType overwriteType = askOverwrite(destination.getPath());

            if (overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                Files.copy(resultFile.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                copiedFiles += 1;
            }

            resultFile.delete();
        }
        catch (IOException e) {
            e.printStackTrace();
        }

        return copiedFiles;
    }

    private int copyDirectoryToDirectory(File[] sourceFiles, final File destination) {
        int copiedFiles = 0;
        OverwriteType overwriteType = OverwriteType.NO;

        for(File file: sourceFiles) {
            File destinationFile = new File(destination.getPath(), file.getName());
            String actualPath = ItemVerifier.getInstance().getActualPath(file.getPath());
            PromptView.getInstance().printMessage(actualPath);

            if(destinationFile.exists()) {
                if(destinationFile.isFile()) {
                    overwriteType = copyFileToFile(file, destinationFile, overwriteType);

                    if (overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                        copiedFiles += 1;
                    }
                }

                else if(destinationFile.isDirectory()) {
                    PromptView.getInstance().printMessage("액세스가 거부되었습니다.");
                }
            }

            else {
                copiedFiles += copyFileToEmptySpace(file, destination);
            }
        }

        return copiedFiles;
    }

    private OverwriteType copyFileToFile(File source, File destination, OverwriteType overwriteType) {
        if(overwriteType == OverwriteType.NO) {
            overwriteType = askOverwrite(destination.getPath());
        }

        if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
            try {
                Files.copy(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);

                String actualPath = ItemVerifier.getInstance().getActualPath(destination.getPath());
                PromptView.getInstance().printMessage(actualPath);
            }
            catch(IOException e) {
                e.printStackTrace();
            }
        }

        return overwriteType;
    }

    private int copyFileToDirectory(File source, File destination) {
        int copiedFiles = 0;
        File destinationFile = new File(destination.getPath(), source.getName());

        if(destinationFile.isFile()) {
            OverwriteType overwriteType = askOverwrite(destinationFile.getPath());

            if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                try {
                    Files.copy(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                    copiedFiles += 1;

                    String actualPath = ItemVerifier.getInstance().getActualPath(destination.getPath());
                    PromptView.getInstance().printMessage(actualPath);
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
            }
        }

        else if(destinationFile.isDirectory()) {
            PromptView.getInstance().printMessage("액세스가 거부되었습니다.");
        }

        else if(!destinationFile.exists()) {
            try {
                Files.copy(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                copiedFiles += 1;

                String actualPath = ItemVerifier.getInstance().getActualPath(destination.getPath());
                PromptView.getInstance().printMessage(actualPath);
            }
            catch(IOException e) {
                e.printStackTrace();
            }
        }

        return copiedFiles;
    }

    private int copyFileToEmptySpace(File source, File destination) {
        int copiedFiles = 0;

        try {
            Files.copy(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
            copiedFiles += 1;

            String actualPath = ItemVerifier.getInstance().getActualPath(destination.getPath());
            PromptView.getInstance().printMessage(actualPath);
        } catch (IOException e) {
            e.printStackTrace();
        }

        return copiedFiles;
    }
}
