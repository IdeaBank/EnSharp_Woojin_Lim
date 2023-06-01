package commands;

import commandInterface.CommandInterface;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.File;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class ChangeDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if (!isCommandValid(command) && !isExceptionalCommandValid(command)) {
            PromptView.getInstance().printError("'" + command.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.");
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

        return commandToken.length == 1 || commandToken.length == 2;
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
        return command.startsWith("cd.") || command.equals("cd\\") || command.equals("cd/");
    }

    public void changeDirectoryWithOneParameter(PromptData promptData, String path) {
        if(path.equals("cd")) {
            PromptView.getInstance().printWorkingDirectory(promptData.getCurrentAbsolutePath());
        }

        else if(path.startsWith("cd.")) {
            changeDirectory(promptData, path.substring(2));
        }

        else if(path.equals("cd\\") || path.equals("/")) {
            Path currentPath = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot().toAbsolutePath();
            changeDirectory(promptData, currentPath.toString());
        }

        else {
            PromptView.getInstance().printError("'" + path.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.");
        }
    }
    public void changeDirectory(PromptData promptData, String path) {
        File targetFolder = new File(path);

        if(path.equals("*") || path.equals("?")) {
        }

        else if(path.equals("\\") || path.equals("/")) {
            Path root = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot();
            promptData.setCurrentAbsolutePath(root.toAbsolutePath().toString());
        }

        else if(targetFolder.toPath().isAbsolute()) {
            if(ItemVerifier.getInstance().isItemExist(targetFolder.getPath())) {
                if(targetFolder.isDirectory()) {
                    String targetFolderPathString = targetFolder.toPath().toAbsolutePath().toString();
                    promptData.setCurrentAbsolutePath(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolderPathString));
                }

                else {
                    PromptView.getInstance().printError("디렉토리 이름이 올바르지 않습니다.");
                }
            }

            else {
                PromptView.getInstance().printError("지정된 경로를 찾을 수 없습니다.");
            }
        }

        else {
            File targetFolderRelative = new File(promptData.getCurrentAbsolutePath(), path);

            if(ItemVerifier.getInstance().isItemExist(targetFolderRelative.getPath())) {
                if(targetFolderRelative.isDirectory()) {
                    String targetFolderRelativePathString = targetFolderRelative.toPath().toAbsolutePath().toString();
                    String upperDirectoryPath = ItemVerifier.getInstance().getUpperDirectoryPath(targetFolderRelativePathString);

                    if(upperDirectoryPath != null) {
                        promptData.setCurrentAbsolutePath(upperDirectoryPath);
                    }
                    else {
                        changeDirectory(promptData, "\\");
                    }
                }

                else {
                    PromptView.getInstance().printError("디렉토리 이름이 올바르지 않습니다.");
                }
            }

            else {
                PromptView.getInstance().printError("지정된 경로를 찾을 수 없습니다.");
            }
        }
    }
}
