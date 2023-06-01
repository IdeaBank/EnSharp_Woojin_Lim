package model;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class PromptData {
    private String currentAbsolutePath;
    private ArrayList<String> commandHistoryList;

    public PromptData() {
        Path currentRelativePath = Paths.get("");
        this.currentAbsolutePath = currentRelativePath.toAbsolutePath().toString();
        this.commandHistoryList = new ArrayList<>();
    }

    public void setCurrentAbsolutePath(String path) {
        this.currentAbsolutePath = path;
    }

    public String getCurrentAbsolutePath() {
        return currentAbsolutePath;
    }

    public void appendCommandHistory(String command) {
        this.commandHistoryList.add(command);
    }

    public ArrayList<String> getCommandHistoryList() {
        return commandHistoryList;
    }
}
