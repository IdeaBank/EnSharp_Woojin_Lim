package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
import constant.CommandResultType;
import model.PromptData;
import view.PromptView;

public class ClearConsole extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "cls");
        String[] commandToken = getCommandToken(command);

        CommandResultType commandResultType = isCommandValid(commandToken);

        if(commandResultType == CommandResultType.SUCCESS) {
            PromptView.getInstance().clearPrompt();
        }

        else if(commandResultType == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        if(commandToken[0].equalsIgnoreCase("cls")) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }
}
