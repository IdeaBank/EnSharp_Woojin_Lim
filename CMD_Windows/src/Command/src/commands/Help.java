package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import model.PromptData;
import view.PromptView;

public class Help implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        String[] commandToken = command.split(" ");

        if(isCommandValid(command) == CommandResultType.SUCCESS) {
            PromptView.getInstance().printHelp();
        }

        else {
            PromptView.getInstance().printError("'" + commandToken[0] +"'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        if(command.equals("help") || command.startsWith("help ")) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    @Override
    public String[] getCommandToken(String command) {
        return command.split(" ");
    }
}
