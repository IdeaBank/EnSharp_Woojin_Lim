package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.File;
import java.nio.file.Path;
import java.util.ArrayList;

public class ChangeDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        // 명령어가 유효한 지 검사
        CommandResultType commandResult = isCommandValid(command);

        // 유효하면 스페이스를 기준으로 명령어를 쪼갬
        if (commandResult == CommandResultType.SUCCESS || isExceptionalCommandValid(command)) {
            String[] commandToken = getCommandToken(command);

            switch (commandToken.length) {
                case 1:
                    // 뒤에 경로를 추가하지 않았을 경우 changeDirectoryWithOneParameter 호출
                    changeDirectoryWithOneParameter(promptData, commandToken[0]);
                    break;
                case 2:
                    // 경로를 바꿔줌
                    changeDirectory(promptData, commandToken[1]);
                    break;
            }
        }

        // cd 명령어를 이상하게 쳤으면 오류 문구 출력
        else if(commandResult == CommandResultType.COMMAND_NOT_VALID) {
            PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
        }

        // cd로 시작하는 이상한 명령어를 쳤으면 오류 문구 출력
        else {
            PromptView.getInstance().printMessage("'" + command.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        String[] commandToken = getCommandToken(command);

        if(commandToken[0].length() != 2 && commandToken[0].length() != 3) {
            return CommandResultType.COMMAND_NOT_EXIST;
        }

        if(commandToken.length == 1 || commandToken.length == 2) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_VALID;
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

    // cd 바로 뒤에 특수문자 붙는 경우 예외처리
    private boolean isExceptionalCommandValid(String command) {
        return command.startsWith("cd.") || command.equals("cd\\") || command.equals("cd/");
    }

    private void changeDirectoryWithOneParameter(PromptData promptData, String path) {
        // cd만 쳤으면 현재 경로 출력
        if(path.equals("cd")) {
            PromptView.getInstance().printWorkingDirectory(promptData.getCurrentAbsolutePath());
        }

        // 현재 경로로 이동
        else if(path.startsWith("cd.")) {
            changeDirectory(promptData, path.substring(2));
        }

        // 최상위 경로로 이동
        else if(path.equals("cd\\") || path.equals("cd/")) {
            Path currentPath = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot().toAbsolutePath();
            changeDirectory(promptData, currentPath.toString());
        }

        // 이상한 명령어면 오류 문구 출력
        else {
            PromptView.getInstance().printMessage("'" + path.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }
    private void changeDirectory(PromptData promptData, String path) {
        File targetFolder = new File(path);

        // 특수문자는 추후에 처리 예정
        if(path.equals("*") || path.equals("?")) {
            return;
        }

        // 최상위 경로로 이동
        else if(path.equals("\\") || path.equals("/")) {
            Path root = new File(promptData.getCurrentAbsolutePath()).toPath().getRoot();
            promptData.setCurrentAbsolutePath(root.toAbsolutePath().toString());
        }

        // 절대경로면 바로 이동하고 경로가 올바르지 않으면 오류 문구 출력
        else if(targetFolder.toPath().isAbsolute()) {
            if(ItemVerifier.getInstance().isItemExist(targetFolder.getPath())) {
                if(targetFolder.isDirectory()) {
                    String targetFolderPathString = targetFolder.toPath().toAbsolutePath().toString();
                    promptData.setCurrentAbsolutePath(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolderPathString));
                }

                // 파일을 지정하면 해당 오류 문구 출력
                else {
                    PromptView.getInstance().printMessage("디렉토리 이름이 올바르지 않습니다.");
                }
            }

            else {
                PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
            }
        }

        // 상대경로이면 현재 경로에 해당 경로를 더해서 구함
        else {
            File targetFolderRelative = new File(promptData.getCurrentAbsolutePath(), path);

            // 경로를 바꿔줌. (..를 너무 많이 쳐서 C:\를 넘으면 C:\로 감)
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

                // 파일이면 해당 오류 문구 출력
                else {
                    PromptView.getInstance().printMessage("디렉토리 이름이 올바르지 않습니다.");
                }
            }

            // 경로가 존재하지 않으면 해당 오류 문구 출력
            else {
                PromptView.getInstance().printMessage("지정된 경로를 찾을 수 없습니다.");
            }
        }
    }
}
