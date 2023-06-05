package model;

import util.ItemVerifier;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class PromptData {
    private String currentAbsolutePath;
    private ArrayList<String> commandHistoryList;

    public PromptData() {
        Path currentRelativePath = Paths.get(System.getProperty("user.home"));
        this.currentAbsolutePath = currentRelativePath.toAbsolutePath().toString();
        this.commandHistoryList = new ArrayList<>();
    }

    public void setCurrentAbsolutePath(String path) {
        this.currentAbsolutePath = ItemVerifier.getInstance().getActualPath(path);
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
