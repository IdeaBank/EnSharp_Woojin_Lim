package controller;

import commands.CommandContainer;
import model.PromptData;
import view.PromptView;
import java.io.BufferedReader;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
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
        String windowsVersion = getCommandExecuteResult("ver");

        if(windowsVersion.startsWith("Windows 10 ")) {
            windowsVersion.substring(3);
        }

        PromptView.getInstance().printPromptInfo(System.getProperty("os.name"), windowsVersion);
        while(true) {
            System.out.print("\n" + promptData.getCurrentAbsolutePath() + ">");
            getCommandInput();
        }
    }

    private void getCommandInput() {
        Scanner scanner = new Scanner(System.in);
        String command = scanner.nextLine();

        /// TODO: IMPLEMENT IF-ELSE STATEMENT

        if(command.startsWith("cd")) {
            CommandContainer.getInstance().getChangeDirectory().executeCommand(promptData, command);
        }

        else if(command.startsWith("dir")) {
            CommandContainer.getInstance().getViewDirectory().executeCommand(promptData, command);
        }
    }

    private void changeDirectory(String command) {

    }

    private void showDirectoryInfo(String command) {

    }

    private void clearPrompt() {
        PromptView.getInstance().clearPrompt();
    }

    private void executeHelp() {
        PromptView.getInstance().printHelp();
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

    public static String getCommandExecuteResult(String command) {
        String str;

        try {
            Process process = new ProcessBuilder("cmd", "/c", command).start();
            BufferedReader stdOut = new BufferedReader(new InputStreamReader(process.getInputStream(), "euc-kr"));
            StringBuilder result = new StringBuilder();

            while((str = stdOut.readLine()) != null) {
                result.append(str);
            }

            return result.toString();
        }

        catch (IOException e) {
            e.printStackTrace();
        }

        return null;
    }
}
