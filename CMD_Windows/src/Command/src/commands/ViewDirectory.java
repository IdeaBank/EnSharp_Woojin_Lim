package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import controller.PromptManager;
import model.PromptData;
import util.ItemVerifier;
import view.PromptView;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileStore;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class ViewDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        if(isCommandValid(command) == CommandResultType.SUCCESS) {
            viewDirectoryFiles(promptData, command);
        }

        else {
            PromptView.getInstance().printError("'" + command.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        String[] commandToken = getCommandToken(command);

        if(3 <= commandToken[0].length() && commandToken[0].length() <= 5) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    @Override
    public String[] getCommandToken(String command) {
        String[] tempCommandToken = command.split(" ");
        ArrayList<String> commandTokenList = new ArrayList<>();

        for(String token: tempCommandToken) {
            if(!token.equals("")) {
                commandTokenList.add(token);
            }
        }

        return commandTokenList.toArray(new String[0]);
    }

    private void viewDirectoryFiles(PromptData promptData, String command) {
        command = command.substring(3).trim();
        ArrayList<String> printedDrive = new ArrayList<>();

        String[] commandToken = getCommandToken(command);

        for (String path: commandToken) {
            File targetFolder = new File(path);

            if(!targetFolder.toPath().isAbsolute()) {
                targetFolder = new File(promptData.getCurrentAbsolutePath(), path);
                targetFolder = new File(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolder.getPath()));
            }

            File[] files;

            long availableSpace = 0;

            try {
                File file = new File(targetFolder.getPath().charAt(0) + ":\\");

                FileStore store = Files.getFileStore(file.toPath());

                availableSpace = store.getUsableSpace();

            } catch(IOException e) {
                e.printStackTrace();
            }

            if(targetFolder.exists()) {
                if(targetFolder.isDirectory()) {
                    files = targetFolder.listFiles();
                    ArrayList<File> fileArrayList = new ArrayList<>(Arrays.asList(files));

                    if(targetFolder.toPath().getRoot() != targetFolder.toPath()) {
                        fileArrayList.add(0, new File(".."));
                        fileArrayList.add(0, new File("."));
                    }

                    files = fileArrayList.toArray(new File[0]);
                }

                else {
                     files = new File[] { targetFolder };
                }

                char driveCharacter = files[files.length - 1].getPath().charAt(0);

                if(!printedDrive.contains(String.valueOf(driveCharacter))) {
                    String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");

                    driveInfo = driveInfo.replace(". 볼", ".\n 볼");

                    PromptView.getInstance().printDriveInfo(driveInfo);
                    printedDrive.add(String.valueOf(driveCharacter));
                }

                PromptView.getInstance().printItemList(availableSpace, files);
            }
        }

        if(commandToken.length == 0) {
            File targetFolder = new File(promptData.getCurrentAbsolutePath());

            File[] files = targetFolder.listFiles();
            ArrayList<File> fileArrayList = new ArrayList<>(Arrays.asList(files));

            if(targetFolder.toPath().getRoot() != targetFolder.toPath()) {
                fileArrayList.add(0, new File(".."));
                fileArrayList.add(0, new File("."));
            }

            files = fileArrayList.toArray(new File[0]);

            long availableSpace = 0;

            try {
                File file = new File(promptData.getCurrentAbsolutePath().charAt(0) + ":\\");

                FileStore store = Files.getFileStore(file.toPath());

                availableSpace = store.getUsableSpace();

            } catch(IOException e) {
                e.printStackTrace();
            }

            char driveCharacter = files[files.length - 1].getPath().charAt(0);

            String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");

            driveInfo = driveInfo.replace(". 볼", ".\n 볼");

            PromptView.getInstance().printDriveInfo(driveInfo);
            printedDrive.add(String.valueOf(driveCharacter));

            PromptView.getInstance().printItemList(availableSpace, files);
        }
    }
}
