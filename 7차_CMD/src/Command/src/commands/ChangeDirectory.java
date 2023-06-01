package commands;

import commandInterface.CommandInterface;
import model.PromptData;
import util.ItemVerifier;

import java.io.File;
import java.nio.file.Path;
import java.util.ArrayList;

public class ChangeDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if (!isCommandValid(command) && !isExceptionalCommandValid(command)) {
            // TODO: 오류 메세지
            return;
        }

        String[] commandToken = getCommandToken(command);

        switch (commandToken.length) {
            case 1:
                changeDirectoryWithOneParameter(promptData, commandToken[0]);
                break;
            case 2:
                changeDirectory(promptData, commandToken[1]);
                break;
        }
    }

    @Override
    public boolean isCommandValid(String command) {
        String[] commandToken = getCommandToken(command);

        if(commandToken[0].length() != 2 && commandToken[0].length() != 3) {
            return false;
        }

        if (commandToken.length == 1 || commandToken.length == 2) {
            return true;
        }

        return false;
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

    private boolean isExceptionalCommandValid(String command) {
        if (command.equals("cd.") || command.equals("cd/")) {
            return true;
        }

        return false;
    }

    public void changeDirectoryWithOneParameter(PromptData promptData, String path) {
        if(path.equals("cd")) {
            // TODO: Print working directory
        }

        else if(path.equals("cd.")) {
            changeDirectory(promptData, ".");
        }

        else if(path.equals("cd/")) {
            Path currentPath = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot().toAbsolutePath();
            changeDirectory(promptData, currentPath.toString());
        }

        else {
            // TODO: 오류 표시
        }
    }
    public void changeDirectory(PromptData promptData, String path) {
        File targetFolder = new File(path);

        if(targetFolder.toPath().isAbsolute()) {
            if(ItemVerifier.getInstance().isItemExist(targetFolder.getPath())) {
                if(targetFolder.isDirectory()) {
                    String targetFolderPathString = targetFolder.toPath().toAbsolutePath().toString();

                    if(targetFolderPathString.equals("/")) {
                        promptData.setCurrentAbsolutePath("/");
                    }

                    else {
                        promptData.setCurrentAbsolutePath(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolderPathString));
                    }
                }

                else {
                    // TODO: 폴더가 아닙니다
                }
            }

            else {
                // TODO: 존재하지 않는 경로
            }
        }

        else {
            File targetFolderRelative = new File(promptData.getCurrentAbsolutePath(), path);

            if(ItemVerifier.getInstance().isItemExist(targetFolderRelative.getPath())) {
                if(targetFolderRelative.isDirectory()) {
                    String targetFolderRelativePathString = targetFolderRelative.toPath().toAbsolutePath().toString();
                    promptData.setCurrentAbsolutePath(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolderRelativePathString));
                }

                else {
                    // TODO: 폴더가 아닙니다
                }
            }

            else {
                // TODO: 존재하지 않는 경로
            }
        }
    }
}
