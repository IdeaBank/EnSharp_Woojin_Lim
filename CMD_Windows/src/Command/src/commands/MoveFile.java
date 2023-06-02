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

public class MoveFile implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if(command.startsWith("move.") || command.startsWith("move\\")) {
            command = "move " + command.substring(2);
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

                moveFile(commandToken[1], destinationFile.getPath(), sourceFile, destinationFile);
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

                moveFile(commandToken[1], commandToken[2], sourceFile, destinationFile);
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

        if(commandToken[0].equals("move")) {
            if(commandToken.length == 1 || commandToken.length > 3) {
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

    public void moveFile(String sourcePath, String destinationPath, File source, File destination) {
        ItemType sourceType = ItemVerifier.getInstance().getItemState(source.getPath());
        ItemType destinationType = ItemVerifier.getInstance().getItemState(destination.getPath());
        boolean keepReplacing = false;

        if(sourceType == ItemType.IS_DIRECTORY) {
            if(destinationType == ItemType.IS_FILE) {
                try {
                    Files.move(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                File destinationFolder = new File(destination.getPath(), source.getName());

                if(destinationFolder.exists()) {
                    askOverwrite(destinationFolder.getPath());
                    PromptView.getInstance().printMessage("액세스가 거부되었습니다.");

                    return;
                }

                try {
                    Files.move(source.toPath(), destinationFolder.toPath(), StandardCopyOption.REPLACE_EXISTING);
                    PromptView.getInstance().printMessage("        1개의 디렉터리를 이동했습니다.");
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
            }
        }

        else if(sourceType == ItemType.IS_FILE) {

            if(destinationType == ItemType.IS_FILE) {
                OverwriteType overwriteType = askOverwrite(destinationPath);

                if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                    try {
                        Files.move(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
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
                            Files.move(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                            PromptView.getInstance().printMessage(destination.getPath());
                        }
                        catch(IOException e) {
                            e.printStackTrace();
                        }
                    }
                }

                else if(destinationFile.isDirectory()) {
                    askOverwrite(destinationFile.getPath());
                    PromptView.getInstance().printMessage("액세스가 거부되었습니다.");

                    return;
                }

                else {
                    try {
                        Files.move(source.toPath(), destinationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                        PromptView.getInstance().printMessage(destination.getPath());
                    }
                    catch(IOException e) {
                        e.printStackTrace();
                    }
                }
            }

            else {
                try {
                    Files.move(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                    PromptView.getInstance().printMessage(destination.getPath());
                }
                catch(IOException e) {
                    e.printStackTrace();
                }
            }

            PromptView.getInstance().printMessage("        1개 파일이 복사되었습니다.");
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
