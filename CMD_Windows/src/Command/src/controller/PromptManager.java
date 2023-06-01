package controller;

import commands.CommandContainer;
import model.PromptData;
import view.PromptView;
import java.io.BufferedReader;

import java.io.IOException;
import java.io.InputStreamReader;
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
        try {
            ProcessBuilder processBuilder = new ProcessBuilder().command("cmd", "/c", command);

            processBuilder.redirectInput(ProcessBuilder.Redirect.INHERIT);
            processBuilder.redirectOutput(ProcessBuilder.Redirect.PIPE);
            processBuilder.redirectError(ProcessBuilder.Redirect.PIPE);

            Process process = processBuilder.start();
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));

            StringBuilder output = new StringBuilder();
            String line;

            while ((line = reader.readLine()) != null) {
                output.append(line).append("\n");
            }

            int exitCode = process.waitFor();

            if (exitCode == 0) {
                return output.toString();
            }

            else {
                System.err.println(exitCode);
            }
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }

        return null;
    }
}
