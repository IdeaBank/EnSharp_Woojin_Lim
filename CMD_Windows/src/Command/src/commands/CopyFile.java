package commands;

import commandInterface.CommandInterface;
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

public class CopyFile implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if(command.startsWith("copy.") || command.startsWith("copy\\")) {
            command = "copy " + command.substring(2);
        }

        CommandResultType commandResult = isCommandValid(command);
        String[] commandToken = getCommandToken(command);

        if(commandResult == CommandResultType.SUCCESS) {

            if(commandToken.length == 2) {
                File sourceFile = new File(commandToken[1]);
                File destinationFile = new File(promptData.getCurrentAbsolutePath());

                if(!sourceFile.isAbsolute()) {
                    File tempFile = new File(promptData.getCurrentAbsolutePath(), commandToken[1]);
                    String sourcePath = ItemVerifier.getInstance().getUpperDirectoryPath(tempFile.getPath());
                    sourceFile = new File(sourcePath);
                }

                copyFile(commandToken[1], destinationFile.getPath(), sourceFile, destinationFile);
            }

            if(commandToken.length == 3) {
                File sourceFile = new File(commandToken[1]);
                File destinationFile = new File(commandToken[2]);

                if (!sourceFile.isAbsolute()) {
                    File tempFile = new File(promptData.getCurrentAbsolutePath(), commandToken[1]);
                    String sourcePath = ItemVerifier.getInstance().getUpperDirectoryPath(tempFile.getPath());
                    sourceFile = new File(sourcePath);
                }

                if (!destinationFile.isAbsolute()) {
                    File tempFile = new File(promptData.getCurrentAbsolutePath(), commandToken[2]);
                    String destinationPath = ItemVerifier.getInstance().getUpperDirectoryPath(tempFile.getPath());
                    destinationFile = new File(destinationPath);
                }

                copyFile(commandToken[1], commandToken[2], sourceFile, destinationFile);
            }
        }

        else if(commandResult == CommandResultType.COMMAND_NOT_VALID) {
            PromptView.getInstance().printMessage("명령 구문이 올바르지 않습니다.");
        }

        else {
            PromptView.getInstance().printMessage("'" + commandToken[0] +"'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        String[] commandToken = getCommandToken(command);

        if(commandToken[0].equals("copy")) {
            if(commandToken.length > 3) {
                return CommandResultType.COMMAND_NOT_VALID;
            }

            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    @Override
    public String[] getCommandToken(String command) {
        String[] tempCommandToken = command.split(" ");
        ArrayList<String> commandTokenList = new ArrayList<>();

        for(String token: tempCommandToken) {
            if(!token.equals("")) {
                commandTokenList.add(token);
            }
        }

        return commandTokenList.toArray(new String[0]);
    }

    public File[] getAllFilesInDirectory(String path) {
        File targetDirectory = new File(path);
        File[] allFiles = targetDirectory.listFiles();

        ArrayList<File> files =  new ArrayList<>();

        for(File file: allFiles) {
            if(file.isFile()) {
                files.add(file);
            }
        }

        return files.toArray(new File[0]);
    }

    public void copyFile(String sourcePath, String destinationPath, File source, File destination) {
        ItemType sourceType = ItemVerifier.getInstance().getItemState(source.getPath());
        ItemType destinationType = ItemVerifier.getInstance().getItemState(destination.getPath());
        int copiedFiles = 0;
        boolean keepReplacing = false;

        if(sourceType == ItemType.IS_DIRECTORY) {
            File[] sourceFiles = getAllFilesInDirectory(source.getPath());

            if(destinationType == ItemType.IS_FILE) {
                StringBuilder result = new StringBuilder();

                try {
                    for (File file : sourceFiles) {
                        BufferedReader reader = new BufferedReader(new FileReader(file.getPath()));
                        String line;

                        while((line = reader.readLine()) != null) {
                            result.append(line + "\n");
                        }
                    }

                    File resultFile = new File(destination.getPath() + ".temp");
                    BufferedWriter writer = new BufferedWriter(new FileWriter(resultFile.getPath()));

                    writer.write(result.toString());
                    writer.close();

                    OverwriteType overwriteType = askOverwrite(destinationPath);

                    if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                        Files.copy(destination.toPath(), resultFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                        copiedFiles += 1;
                    }

                    resultFile.delete();
                }
                catch (IOException e) {
                    e.printStackTrace();
                }
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
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

        else if(sourceType == ItemType.IS_FILE) {

            if(destinationType == ItemType.IS_FILE) {
                OverwriteType overwriteType = askOverwrite(destinationPath);

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

    public OverwriteType askOverwrite(String path) {
        Scanner scanner = new Scanner(System.in);
        String answer = null;

        while(answer == null || (!answer.equalsIgnoreCase("y") &&
                !answer.equalsIgnoreCase("n") && !answer.equalsIgnoreCase("a"))) {

            PromptView.getInstance().printMessageWithNoNewline(path + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");
            answer = scanner.nextLine();

            if (answer.equalsIgnoreCase("y")) {
                return OverwriteType.YES;
            }

            else if(answer.equalsIgnoreCase("n")) {
                return OverwriteType.NO;
            }

            else if(answer.equalsIgnoreCase("a")) {
                return OverwriteType.ALL;
            }
        }

        return OverwriteType.NO;
    }
}
