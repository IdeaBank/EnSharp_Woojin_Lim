package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
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

public class MoveFile extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "move");

        String[] commandToken = getCommandToken(command);
        CommandResultType commandResult = isCommandValid(commandToken);

        if(commandResult == CommandResultType.SUCCESS) {
            if(commandToken.length == 2) {
                File sourceFile = new File(commandToken[1]);
                File destinationFile = new File(promptData.getCurrentAbsolutePath());

                sourceFile = toAbsoluteFile(promptData, sourceFile);

                moveFile(sourceFile, destinationFile);
            }

            if(commandToken.length == 3) {
                File sourceFile = new File(commandToken[1]);
                File destinationFile = new File(commandToken[2]);

                sourceFile = toAbsoluteFile(promptData, sourceFile);
                destinationFile = toAbsoluteFile(promptData, destinationFile);

                moveFile(sourceFile, destinationFile);
            }
        }

        else if(commandResult == CommandResultType.COMMAND_NOT_VALID) {
                PromptView.getInstance().printMessage("명령 구문이 올바르지 않습니다.");
        }

        else if(commandResult == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        if(commandToken[0].equals("move")) {
            if(commandToken.length == 1 || commandToken.length > 3) {
                return CommandResultType.COMMAND_NOT_VALID;
            }

            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    public void moveFile(File source, File destination) {
        ItemType sourceType = ItemVerifier.getInstance().getItemState(source.getPath());
        ItemType destinationType = ItemVerifier.getInstance().getItemState(destination.getPath());

        if(sourceType == ItemType.IS_DIRECTORY) {
            if(destinationType == ItemType.IS_FILE) {
                moveDirectoryToFile(source, destination);
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                moveDirectoryToDirectory(source, destination);
            }
        }

        else if(sourceType == ItemType.IS_FILE) {
            if(destinationType == ItemType.IS_FILE) {
                moveFileToFile(source, destination, OverwriteType.NO);
            }

            else if(destinationType == ItemType.IS_DIRECTORY) {
                File destinationFile = new File(destination.getPath(), source.getName());
                ItemType destinationFileType = ItemVerifier.getInstance().getItemState(destination.getPath());

                if(destinationFileType == ItemType.IS_FILE) {
                    moveFileToFile(source, destinationFile, OverwriteType.NO);
                }

                else if(destinationFileType == ItemType.IS_DIRECTORY) {
                    printBadAccess(destinationFile);
                    return;
                }

                else if(destinationFileType == ItemType.ITEM_DOES_NOT_EXIST) {
                    moveToEmptySpace(source, destination, destinationFile);
                }
            }

            else if (destinationType == ItemType.ITEM_DOES_NOT_EXIST) {
                moveToEmptySpace(source, destination, destination);
            }

            PromptView.getInstance().printMessage("        1개 파일이 복사되었습니다.");
        }

        else if (sourceType == ItemType.ITEM_DOES_NOT_EXIST) {
            PromptView.getInstance().printMessage("지정된 파일을 찾을 수 없습니다.");
        }
    }

    private void moveDirectoryToFile(File source, File destination) {
        try {
            OverwriteType overwriteType = askOverwrite(destination.getPath());

            if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
                Files.move(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
            }
        }
        catch(IOException e) {
            e.printStackTrace();
        }
    }

    private void printBadAccess(File destinationFolder) {
        askOverwrite(destinationFolder.getPath());
        PromptView.getInstance().printMessage("액세스가 거부되었습니다.");
    }

    private void moveDirectoryToDirectory(File source, File destination) {
        File destinationFolder = new File(destination.getPath(), source.getName());

        if(destinationFolder.exists()) {
            printBadAccess(destinationFolder);
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

    private OverwriteType moveFileToFile(File source, File destination, OverwriteType overwriteType) {
        if(overwriteType == OverwriteType.NO) {
            overwriteType = askOverwrite(destination.getPath());
        }

        if(overwriteType == OverwriteType.YES || overwriteType == OverwriteType.ALL) {
            try {
                Files.move(source.toPath(), destination.toPath(), StandardCopyOption.REPLACE_EXISTING);
                PromptView.getInstance().printMessage(destination.getPath());
            }
            catch(IOException e) {
                e.printStackTrace();
            }
        }

        return overwriteType;
    }

    private int moveToEmptySpace(File source, File destination, File destintationFile) {
        try {
            Files.move(source.toPath(), destintationFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
            PromptView.getInstance().printMessage(destination.getPath());
        }
        catch(IOException e) {
            e.printStackTrace();
        }
    }
}
