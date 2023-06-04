package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
import constant.CommandResultType;
import model.PromptData;
import view.PromptView;

public class Help extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "help");
        String[] commandToken = command.split(" ");

        CommandResultType commandResultType = isCommandValid(commandToken);

        // 명령어가 제대로 되어있으면 help 문구 출력
        if(commandResultType == CommandResultType.SUCCESS) {
            PromptView.getInstance().printHelp();
        }

        // 명령어가 이상하면 오류 문구 출력
        else if(commandResultType == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        // help이거나 help로 시작하고 띄어쓰기가 있으면 정상 명령어로 판단
        if(commandToken.length == 1) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }
}
