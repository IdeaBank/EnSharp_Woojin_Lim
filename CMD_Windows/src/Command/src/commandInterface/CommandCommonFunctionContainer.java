package commandInterface;

import constant.OverwriteType;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.File;
import java.util.ArrayList;
import java.util.Scanner;

public class CommandCommonFunctionContainer extends DirectoryFunctionContainer {
    protected String getNormalCommand(String command, String commandString) {
        if(command.toLowerCase().startsWith(commandString + ".") || command.toLowerCase().startsWith(commandString + "/") || command.toLowerCase().startsWith(commandString + "\\")) {
            return commandString + command.substring(commandString.length());
        }

        return command;
    }

    public String[] getCommandToken(String command) {
        String[] tempCommandToken = command.split(" ");
        ArrayList<String> commandTokenList = new ArrayList<>();

        for(int i = 0; i < tempCommandToken.length; ++i) {
            if(!tempCommandToken[i].equals("")) {
                if(tempCommandToken[i].startsWith("\"") && i + 1 < tempCommandToken.length) {
                    String tempString = tempCommandToken[i] + " " + tempCommandToken[i + 1];
                    tempString = tempString.substring(1, tempString.length() - 1);

                    commandTokenList.add(tempString);
                    i = i + 1;
                }

                else {
                    commandTokenList.add(tempCommandToken[i]);
                }
            }
        }

        return commandTokenList.toArray(new String[0]);
    }

    protected File toAbsoluteFile(PromptData promptData, File file) {
        File tempFile = new File(promptData.getCurrentAbsolutePath(), file.getPath());
        String sourcePath = ItemVerifier.getInstance().getUpperDirectoryPath(tempFile.getPath());
        return new File(sourcePath);
    }

    public OverwriteType askOverwrite(String path) {
        Scanner scanner = new Scanner(System.in);
        String answer = null;

        while(answer == null || (!answer.equalsIgnoreCase("y") &&
                !answer.equalsIgnoreCase("n") && !answer.equalsIgnoreCase("a"))) {

            PromptView.getInstance().printMessageWithNoNewline(path + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");
            answer = scanner.nextLine();

            if (answer.equalsIgnoreCase("y")) {
                return OverwriteType.YES;
            }

            else if(answer.equalsIgnoreCase("n")) {
                return OverwriteType.NO;
            }

            else if(answer.equalsIgnoreCase("a")) {
                return OverwriteType.ALL;
            }
        }

        return OverwriteType.NO;
    }
}
