package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.SimpleCommandInterface;
import constant.CommandResultType;
import model.PromptData;
import view.PromptView;

import java.util.ArrayList;

public class Doskey extends CommandCommonFunctionContainer implements SimpleCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "doskey");
        String[] commandToken = getCommandToken(command);
        // 명령어가 유효한 지 검사
        CommandResultType commandResult = isCommandValid(commandToken);

        if(commandToken[0].equalsIgnoreCase("doskey") && commandToken.length == 1) {
            return;
        }

        switch(commandResult) {
            case SUCCESS:
                showAllHistory(promptData);
                break;
            case COMMAND_NOT_VALID:
                PromptView.getInstance().printMessage("잘못된 매크로 정의입니다.");
                break;
            case COMMAND_NOT_EXIST:
                PromptView.getInstance().printNoCommand(commandToken[0]);
                break;
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandTokens) {
        if(!commandTokens[0].equals("doskey")) {
            return CommandResultType.COMMAND_NOT_EXIST;
        }

        if(commandTokens.length == 2 && commandTokens[1].equals("/history")) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_VALID;
    }

    private void showAllHistory(PromptData promptData) {
        ArrayList<String> commandHistory = promptData.getCommandHistoryList();

        for (String command: commandHistory) {
            PromptView.getInstance().printMessage(command);
        }
    }
}
