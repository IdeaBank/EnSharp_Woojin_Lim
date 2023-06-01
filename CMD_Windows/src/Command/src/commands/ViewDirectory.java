package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import controller.PromptManager;
import model.PromptData;
import view.PromptView;

import java.io.File;
import java.util.ArrayList;

public class ViewDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if(isCommandValid(command) == CommandResultType.SUCCESS) {
            viewDirectoryFiles(command);
        }

        else {
            PromptView.getInstance().printError("'" + command.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        String[] commandToken = getCommandToken(command);

        if(3 <= commandToken[0].length() && commandToken[0].length() <= 5) {
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

    private void viewDirectoryFiles(String command) {
        command = command.substring(3).trim();

        String[] commandToken = getCommandToken(command);

        for (String path: commandToken) {
            File targetFolder = new File(path);

            File[] files;

            if(targetFolder.exists()) {
                if(targetFolder.isDirectory()) {
                    files = targetFolder.listFiles();
                }

                else {
                     files = new File[] { targetFolder };
                }

                char driveCharacter = files[0].getPath().charAt(0);
                String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");
                PromptView.getInstance().printItemInfo(driveInfo, files);
            }
        }
    }
}
