package controller;

import model.PromptData;
import view.PromptView;

import java.util.Scanner;

public class PromptManager {
    private PromptData promptData;
    private PromptView promptView;

    public PromptManager() {
        this.promptData = new PromptData();
        this.promptView = new PromptView();
    }

    public PromptManager(String path) {
        this.promptData = new PromptData();
        promptData.setCurrentAbsolutePath(path);
        this.promptView = new PromptView();
    }

    public void startPrompt() {
        promptView.printPromptInfo();
    }

    private void getCommandInput() {
        promptView.printWorkingDirectory();

        Scanner scanner = new Scanner(System.in);
        String command = scanner.nextLine();

        /// TODO: IMPLEMENT IF-ELSE STATEMENT

        if(command.startsWith("")) {

        }
    }

    private void changeDirectory(String command) {

    }

    private void showDirectoryInfo(String command) {

    }

    private void clearPrompt() {
        promptView.clearPrompt("");
    }

    private void executeHelp() {
        promptView.printHelp("");
    }

    private void copyFile() {

    }

    private void moveFile() {

    }

    private void saveHistory(String command) {
        promptData.appendCommandHistory(command);
    }

    private void showHistory() {

    }

    private void startNewPrompt(String command) {
        PromptManager newPromptManager = new PromptManager();
        newPromptManager.startPrompt();
    }
}
