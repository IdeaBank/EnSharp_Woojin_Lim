package commandInterface;

import java.util.ArrayList;

public class CommandCommonFunctionContainer {
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
}
