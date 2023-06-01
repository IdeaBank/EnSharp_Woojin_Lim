package commandInterface;

import model.PromptData;

public interface CommandInterface {
    public abstract void executeCommand(PromptData promptData, String command);
    public abstract boolean isCommandValid(String command);
    public abstract String[] getCommandToken(String command);
}
