package controller;

import commands.CommandContainer;
import model.PromptData;
import view.PromptView;

import java.util.Scanner;

public class PromptManager {
    private final PromptData promptData;

    public PromptManager() {
        this.promptData = new PromptData();
    }

    public PromptManager(String path) {
        this.promptData = new PromptData();
        promptData.setCurrentAbsolutePath(path);
    }

    public void startPrompt() {
        while(true) {
            getCommandInput();
            System.out.println(promptData.getCurrentAbsolutePath());
        }
    }

    private void getCommandInput() {
        PromptView.getInstance().printWorkingDirectory(promptData.getCurrentAbsolutePath());

        Scanner scanner = new Scanner(System.in);
        String command = scanner.nextLine();

        /// TODO: IMPLEMENT IF-ELSE STATEMENT

        if(command.startsWith("cd")) {
            CommandContainer.getInstance().getChangeDirectory().executeCommand(promptData, command);
        }
    }

    private void changeDirectory(String command) {

    }

    private void showDirectoryInfo(String command) {

    }

    private void clearPrompt() {
        PromptView.getInstance().clearPrompt("");
    }

    private void executeHelp() {
        PromptView.getInstance().printHelp("");
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
