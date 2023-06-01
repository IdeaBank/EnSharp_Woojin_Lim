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
    private boolean continueInput;

    public PromptManager() {
        this.promptData = new PromptData();
        this.continueInput = true;
    }

    public PromptManager(String path) {
        this.promptData = new PromptData();
        promptData.setCurrentAbsolutePath(path);

        this.continueInput = true;
    }

    public void startPrompt() {
        String windowsVersion = getCommandExecuteResult("ver");

        if(windowsVersion.startsWith("Windows 10 ")) {
            windowsVersion.substring(3);
        }

        PromptView.getInstance().printPromptInfo(System.getProperty("os.name"), windowsVersion);
        while(this.continueInput) {
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

        else if(command.startsWith("cls")) {
            CommandContainer.getInstance().getClearConsole().executeCommand(promptData, command);
        }

        else if(command.startsWith("help")) {
            CommandContainer.getInstance().getHelp().executeCommand(promptData, command);
        }

        else if(command.equals("exit") || command.startsWith("exit/") ||
                command.startsWith("exit\\") || command.startsWith("exit.") ||
                command.startsWith("exit ")) {
            this.continueInput = false;
        }

        else if(command.equals("cmd") || command.startsWith("cmd/") ||
                command.startsWith("cmd\\") || command.startsWith("cmd.") ||
                command.startsWith("cmd ")) {

            startNewPrompt();
        }

        else {
            command = command.split(" ")[0];

            PromptView.getInstance().printError("'" + command + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }

    private void saveHistory(String command) {
        promptData.appendCommandHistory(command);
    }

    private void showHistory() {

    }

    private void startNewPrompt() {
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
