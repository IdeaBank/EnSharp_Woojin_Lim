package commandInterface;

import constant.CommandResultType;
import model.PromptData;

public interface CommandInterface {
    // 명령어의 기본 구조
    public abstract void executeCommand(PromptData promptData, String command);
    public abstract CommandResultType isCommandValid(String command);
    public abstract String[] getCommandToken(String command);
}
