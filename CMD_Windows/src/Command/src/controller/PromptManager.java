package controller;

import commandInterface.CommandCommonFunctionContainer;
import commands.CommandContainer;
import commands.MoveFile;
import constant.CommandType;
import model.PromptData;
import view.PromptView;
import java.io.BufferedReader;

import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.Scanner;

public class PromptManager extends CommandCommonFunctionContainer {
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

        PromptView.getInstance().printPromptInfo(windowsVersion);

        while(this.continueInput) {
            System.out.print("\n" + promptData.getCurrentAbsolutePath() + ">");
            getCommandInput();
        }
    }

    private void getCommandInput() {
        Scanner scanner = new Scanner(System.in);
        String command = scanner.nextLine();
        saveHistory(command);

        command = command.trim();

        String []commands = command.split("&&");

        /// TODO: IMPLEMENT IF-ELSE STATEMENT

        for(String token: commands) {
            token = token.trim();

            CommandType commandType = getCommand(token);

            switch(commandType) {
                case CD:
                    CommandContainer.getInstance().getChangeDirectory().executeCommand(promptData, token);
                    break;
                case DIR:
                    CommandContainer.getInstance().getViewDirectory().executeCommand(promptData, token);
                    break;
                case CLS:
                    CommandContainer.getInstance().getClearConsole().executeCommand(promptData, token);
                    break;
                case HELP:
                    CommandContainer.getInstance().getHelp().executeCommand(promptData, token);
                    break;
                case EXIT:
                    this.continueInput = false;
                    break;
                case CMD:
                    startNewPrompt();
                    break;
                case COPY:
                    CommandContainer.getInstance().getCopyFile().executeCommand(promptData, token);
                    break;
                case MOVE:
                    CommandContainer.getInstance().getMoveFile().executeCommand(promptData, token);
                    break;
                case DOSKEY:
                    CommandContainer.getInstance().getDoskey().executeCommand(promptData, token);
                    break;
                default:
                    token = token.split(" ")[0];
                    PromptView.getInstance().printNoCommand(token);
            }
        }
    }

    private void saveHistory(String command) {
        promptData.appendCommandHistory(command);
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

    private CommandType getCommand(String command) {
        String []availableCommands = { "cd", "dir", "cls", "help", "exit", "cmd", "copy", "move", "doskey" };

        for(int i = 0; i < availableCommands.length; ++i)
        {
            if(command.toLowerCase().startsWith(availableCommands[i])) {
                switch(i) {
                    case 0:
                        return CommandType.CD;
                    case 1:
                        return CommandType.DIR;
                    case 2:
                        return CommandType.CLS;
                    case 3:
                        return CommandType.HELP;
                    case 4:
                        return CommandType.EXIT;
                    case 5:
                        return CommandType.CMD;
                    case 6:
                        return CommandType.COPY;
                    case 7:
                        return CommandType.MOVE;
                    case 8:
                        return CommandType.DOSKEY;
                }
            }
        }

        return CommandType.NONE;
    }
}
