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

        if(command.toLowerCase().startsWith("cd")) {
            CommandContainer.getInstance().getChangeDirectory().executeCommand(promptData, "cd" + command.substring(2));
        }

        else if(command.toLowerCase().startsWith("dir")) {
            CommandContainer.getInstance().getViewDirectory().executeCommand(promptData, "dir" + command.substring(3));
        }

        else if(command.toLowerCase().startsWith("cls")) {
            CommandContainer.getInstance().getClearConsole().executeCommand(promptData, "cls" + command.substring(3));
        }

        else if(command.toLowerCase().startsWith("help")) {
            CommandContainer.getInstance().getHelp().executeCommand(promptData, "help" + command.substring(4));
        }

        else if(command.equalsIgnoreCase("exit") || command.toLowerCase().startsWith("exit/") ||
                command.toLowerCase().startsWith("exit\\") || command.toLowerCase().startsWith("exit.") ||
                command.toLowerCase().startsWith("exit ")) {
            this.continueInput = false;
        }

        else if(command.equalsIgnoreCase("cmd") || command.toLowerCase().startsWith("cmd/") ||
                command.toLowerCase().startsWith("cmd\\") || command.toLowerCase().startsWith("cmd.") ||
                command.toLowerCase().startsWith("cmd ")) {

            startNewPrompt();
        }

        else if(command.toLowerCase().startsWith("copy")) {
            CommandContainer.getInstance().getCopyFile().executeCommand(promptData, "copy" + command.substring(4));
        }

        else if(command.toLowerCase().startsWith("move")) {
            CommandContainer.getInstance().getMoveFile().executeCommand(promptData, "move" + command.substring(4));
        }

        else {
            command = command.split(" ")[0];

            PromptView.getInstance().printMessage("'" + command + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
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
