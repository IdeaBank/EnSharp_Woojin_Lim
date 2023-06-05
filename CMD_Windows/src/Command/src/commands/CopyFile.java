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

    public File[] getAllFilesInDirectory(String path) {
        File targetDirectory = new File(path);
        File[] allFiles = getAllDirectoryAndFile(targetDirectory.getPath());

        ArrayList<File> files = new ArrayList<>();

        for(File file: allFiles) {
            if(file.isFile()) {
                files.add(file);
            }
        }

        return files.toArray(new File[0]);
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
            File[] sourceFiles = getAllFilesInDirectory(source.getPath());

            PromptView.getInstance().printMessage(sourceFiles[0].getPath());
            PromptView.getInstance().printMessage("같은 파일로 복사할 수 없습니다.");
            PromptView.getInstance().printMessage("        0개 파일이 복사되었습니다.");
            return;
        }

        if(sourceType == ItemType.IS_DIRECTORY) {
            File[] sourceFiles = getAllFilesInDirectory(source.getPath());

            if(destinationType == ItemType.IS_FILE) {
                copyDirectoryToFile(sourceFiles, destination, destination.getPath());
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                copyDirectoryToDirectory(sourceFiles, destination);
            }
        }

        else if(sourceType == ItemType.IS_FILE) {

            if(destinationType == ItemType.IS_FILE) {
                OverwriteType overwriteType = askOverwrite(destination.getPath());

                if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                    try {
                        Files.copy(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                        copiedFiles += 1;
                        PromptView.getInstance().printMessage(destination.getPath());
                    }
                    catch(IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                File destinationFile = new File(destination.getPath(), source.getName());

                if(destinationFile.isFile()) {
                    OverwriteType overwriteType = askOverwrite(destinationFile.getPath());

                    if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                        try {
                            Files.copy(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                            copiedFiles += 1;
                            PromptView.getInstance().printMessage(destination.getPath());
                        }
                        catch(IOException e) {
                            e.printStackTrace();
                        }
                    }
                }

                else if(destinationFile.isDirectory()) {
                    PromptView.getInstance().printMessage("액세스가 거부되었습니다.");
                }

                else {
                    try {
                        Files.copy(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                        copiedFiles += 1;
                        PromptView.getInstance().printMessage(destination.getPath());
                    }
                    catch(IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            else {
                try {
                    Files.copy(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                    copiedFiles += 1;
                    PromptView.getInstance().printMessage(destination.getPath());
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
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

    public OverwriteType askOverwrite(String path) {
        Scanner scanner = new Scanner(System.in);
        String answer = null;

        while(answer == null || isYesNoAll(answer)) {

            PromptView.getInstance().printMessageWithNoNewline(path + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");
            answer = scanner.nextLine();

            if (answer.toLowerCase().startsWith("y")) {
                return OverwriteType.YES;
            }

            else if(answer.toLowerCase().startsWith("n")) {
                return OverwriteType.NO;
            }

            else if(answer.toLowerCase().startsWith("a")) {
                return OverwriteType.ALL;
            }
        }

        return OverwriteType.NO;
    }

    private void copyDirectoryToFile(File[] sourceFiles, File destination, String destinationPath) {
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

            OverwriteType overwriteType = askOverwrite(destinationPath);

            if (overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                Files.copy(resultFile.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                copiedFiles += 1;
            }

            resultFile.delete();
        }
        catch (IOException e) {
            e.printStackTrace();
        }

        PromptView.getInstance().printMessage("        " + String.valueOf(copiedFiles) + "개 파일이 복사되었습니다.");
    }

    private void copyDirectoryToDirectory(File[] sourceFiles, File destination) {
        int copiedFiles = 0;
        boolean keepReplacing = false;

        for(File file: sourceFiles) {
            File destinationFile = new File(destination.getPath(), file.getName());
            PromptView.getInstance().printMessage(file.getPath());

            if(destinationFile.exists()) {
                if(destinationFile.isFile()) {
                    if(!keepReplacing) {
                        OverwriteType overwriteType = askOverwrite(destinationFile.getPath());

                        try {
                            if (overwriteType == OverwriteType.YES) {
                                Files.copy(file.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                                copiedFiles += 1;
                            }

                            else if (overwriteType == OverwriteType.ALL) {
                                Files.copy(file.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                                copiedFiles += 1;
                                keepReplacing = true;
                            }
                        }
                        catch(IOException e){
                            e.printStackTrace();
                        }
                    }

                    else {
                        try {
                            Files.copy(file.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                            copiedFiles += 1;
                        }
                        catch(IOException e) {
                            e.printStackTrace();
                        }
                    }
                }

                else if(destinationFile.isDirectory()) {
                    PromptView.getInstance().printMessage("액세스가 거부되었습니다.");
                }
            }

            else {
                try {
                    Files.copy(file.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                    copiedFiles += 1;
                    PromptView.getInstance().printMessage(destinationFile.getPath());
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
            }
        }

        PromptView.getInstance().printMessage("        " + String.valueOf(copiedFiles) + "개 파일이 복사되었습니다.");
    }
}
