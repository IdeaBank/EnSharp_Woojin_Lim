package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
import constant.CommandResultType;
import constant.ItemType;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.File;
import java.nio.file.Path;
import java.util.ArrayList;

public class ChangeDirectory extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "cd");
        String[] commandToken = getCommandToken(command);
        // 명령어가 유효한 지 검사
        CommandResultType commandResult = isCommandValid(commandToken);

        // 유효하면 스페이스를 기준으로 명령어를 쪼갬
        if (commandResult == CommandResultType.SUCCESS) {
            switch (commandToken.length) {
                case 1:
                    // 뒤에 경로를 추가하지 않았을 경우 changeDirectoryWithOneParameter 호출
                    changeDirectoryWithOneParameter(promptData, commandToken[0]);
                    break;
                case 2:
                    // 경로를 바꿔줌
                    commandToken[1] = removeDots(commandToken[1]);
                    changeDirectory(promptData, commandToken[1]);
                    break;
            }
        }

        // cd 명령어를 이상하게 쳤으면 오류 문구 출력
        else if(commandResult == CommandResultType.COMMAND_NOT_VALID) {
            PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
        }

        // cd로 시작하는 이상한 명령어를 쳤으면 오류 문구 출력
        else if(commandResult == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        if(commandToken[0].length() != 2 && commandToken[0].length() != 3) {
            return CommandResultType.COMMAND_NOT_EXIST;
        }

        if(commandToken.length == 1 || commandToken.length == 2) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_VALID;
    }


    private boolean isAllDots(String str) {
        char []strToCharArray = str.toCharArray();

        for(char ch : strToCharArray) {
            if(ch != '.') {
                return false;
            }
        }

        return true;
    }

    private String removeDots(String path) {
        String[] pathToken = path.split("\\\\");

        for(String token: pathToken) {
            if(isAllDots(token) && token.length() > 2) {
                token = ".";
            }
        }

        return String.join("\\", pathToken);
    }

    private void changeDirectoryWithOneParameter(PromptData promptData, String command) {
        // cd만 쳤으면 현재 경로 출력
        if(command.equals("cd")) {
            PromptView.getInstance().printWorkingDirectory(promptData.getCurrentAbsolutePath());
        }

        // 이상한 명령어면 오류 문구 출력
        else {
            PromptView.getInstance().printNoCommand(command);
        }
    }
    private void changeDirectory(PromptData promptData, String path) {
        if(path.startsWith("\\\\")) {
            PromptView.getInstance().printMessage("'" + path + "'");
            PromptView.getInstance().printMessage("CMD에서 현재 디렉터리로 UNC 경로를 지원하지 않습니다.");
            return;
        }

        File targetFolder = new File(path);

        // 절대경로면 바로 이동하고 경로가 올바르지 않으면 오류 문구 출력
        if(targetFolder.toPath().isAbsolute()) {
            moveWithAbsolutePath(promptData, targetFolder);
        }

        // 상대경로이면 현재 경로에 해당 경로를 더해서 구함
        else {
            moveWithRelativePath(promptData, path);
        }
    }

    private void moveWithAbsolutePath(PromptData promptData, File targetFolder) {
        ItemType itemType = ItemVerifier.getInstance().getItemState(targetFolder.getPath());

        switch(itemType) {
            case IS_DIRECTORY:
                moveToDirectory(promptData, targetFolder.toPath().toAbsolutePath().toString());
                break;
            case IS_FILE:
                PromptView.getInstance().printMessage("디렉토리 이름이 올바르지 않습니다.");
                break;
            case ITEM_DOES_NOT_EXIST:
                PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
                break;
        }
    }

    private void moveWithRelativePath(PromptData promptData, String path) {
        File targetFolder;

        if (path.startsWith("\\") || path.startsWith("/")) {
            path = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot().toString() + path.substring(1);
            targetFolder = new File(ItemVerifier.getInstance().getUpperDirectoryPath(path));
        }

        else {
            path = new File(promptData.getCurrentAbsolutePath(), path).getPath();
            targetFolder = new File(ItemVerifier.getInstance().getUpperDirectoryPath(path));
        }

        ItemType itemType = ItemVerifier.getInstance().getItemState(targetFolder.getPath());

        switch (itemType) {
            case IS_DIRECTORY:
                moveToDirectory(promptData, targetFolder.toPath().toAbsolutePath().toString());
                break;
            case IS_FILE:
                PromptView.getInstance().printMessage("디렉토리 이름이 올바르지 않습니다.");
                break;
            case ITEM_DOES_NOT_EXIST:
                PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
                break;
        }
    }

    public void moveToDirectory(PromptData promptData, String path) {
        String upperDirectoryPath = ItemVerifier.getInstance().getUpperDirectoryPath(path);
        promptData.setCurrentAbsolutePath(upperDirectoryPath);
    }
}
