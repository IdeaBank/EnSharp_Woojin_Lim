package commandInterface;

import constant.CommandResultType;
import model.PromptData;

public interface SimpleCommandInterface {
    void executeCommand(PromptData promptData, String command);
    CommandResultType isCommandValid(String[] commandTokens);
}
