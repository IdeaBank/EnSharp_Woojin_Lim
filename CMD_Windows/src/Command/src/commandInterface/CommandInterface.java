package commandInterface;

import constant.CommandResultType;
import model.PromptData;

public interface CommandInterface {
    public abstract void executeCommand(PromptData promptData, String command);
    public abstract CommandResultType isCommandValid(String command);
    public abstract String[] getCommandToken(String command);
}
