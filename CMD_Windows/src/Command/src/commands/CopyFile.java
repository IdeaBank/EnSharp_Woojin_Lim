package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import model.PromptData;

public class CopyFile implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {

    }

    @Override
    public CommandResultType isCommandValid(String command) {
        return null;
    }

    @Override
    public String[] getCommandToken(String command) {
        return new String[0];
    }
}
