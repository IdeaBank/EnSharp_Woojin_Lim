package commands;

import commandInterface.CommandCommonFunctionContainer;
import commandInterface.ComplexCommandInterface;
import constant.CommandResultType;
import constant.ItemType;
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

public class ViewDirectory extends CommandCommonFunctionContainer implements ComplexCommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        command = getNormalCommand(command, "dir");
        String[] commandToken = getCommandToken(command);

        CommandResultType commandResultType = isCommandValid(commandToken);

        // 정상적인 명령어이면 경로에 있는 파일들 출력
        if(commandResultType == CommandResultType.SUCCESS) {
            viewDirectoryFiles(promptData, command);
        }

        // 비정상적인 명령어이면 오류 문구 출력
        else if(commandResultType == CommandResultType.COMMAND_NOT_EXIST) {
            PromptView.getInstance().printNoCommand(commandToken[0]);
        }
    }

    @Override
    public CommandResultType isCommandValid(String[] commandToken) {
        if(commandToken[0].equalsIgnoreCase("dir")) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    private void viewDirectoryFiles(PromptData promptData, String command) {
        // dir와 띄어쓰기를 없앰
        command = command.substring(3).trim();
        // 출력된 드라이브를 저장하기 위한 변수
        ArrayList<String> printedDrive = new ArrayList<>();

        // 입력된 경로를 스페이스 단위로 잘라서 반환
        String[] commandToken = getCommandToken(command);

        // 반환된 경로들을 순회
        for (String path: commandToken) {
            File targetFolder = new File(path);

            // 상대경로로 주어졌으면 절대경로로 바꿔줌
            if(!targetFolder.toPath().isAbsolute()) {
                targetFolder = toAbsoluteFile(promptData, targetFolder);
            }

            File[] files = new File[] {};

            long availableSpace = 0;

            try {
                // 최상위 드라이브를 얻어와서 남은 공간을 구함
                File file = new File(targetFolder.getPath().charAt(0) + ":\\");

                FileStore store = Files.getFileStore(file.toPath());

                availableSpace = store.getUsableSpace();

            } catch(IOException e) {
                e.printStackTrace();
            }

            ItemType itemType = ItemVerifier.getInstance().getItemState(targetFolder.getPath());

            switch(itemType) {
                case IS_DIRECTORY:
                    files = addDirectoryFilesToList(targetFolder);
                    break;
                case IS_FILE:
                    files = new File[] { targetFolder };
                    break;
            }

            if(files.length == 0) {
                files = new File[] { new File(commandToken[0]) };
            }

            printDriveInfo(files, printedDrive);

            if(itemType != ItemType.ITEM_DOES_NOT_EXIST) {

                // 파일들 출력
                PromptView.getInstance().printItemList(availableSpace, files, targetFolder.getAbsolutePath());
            }

            else if(itemType == ItemType.ITEM_DOES_NOT_EXIST) {
                PromptView.getInstance().printMessage("파일을 찾을 수 없습니다.");
            }
        }

        // dir만 쳤을 경우 현재 경로에 있는 폴더 및 파일들 출력
        if(commandToken.length == 0) {
            showCurrentDirectoryList(promptData, printedDrive);
        }
    }

    private void addDotDirectories(ArrayList<File> fileArrayList, File targetFolder) {
        File oneDot = new File(".");
        File twoDot = new File("..");

        long lastModified = targetFolder.lastModified();

        oneDot.setLastModified(lastModified);
        twoDot.setLastModified(lastModified);

        fileArrayList.add(0, oneDot);
        fileArrayList.add(1, twoDot);
    }

    private File[] addDirectoryFilesToList(File targetFolder) {
        File[] files = targetFolder.listFiles();
        ArrayList<File> fileArrayList = new ArrayList<>(Arrays.asList(files));

        // 최상위 폴더가 아니면 .과 ..을 추가
        if(targetFolder.toPath().getRoot() != targetFolder.toPath()) {
            addDotDirectories(fileArrayList, targetFolder);
        }

        return fileArrayList.toArray(new File[0]);
    }

    private void printDriveInfo(File[] files, ArrayList<String> printedDrive) {
        char driveCharacter = files[files.length - 1].getPath().charAt(0);

        // 드라이브의 정보를 얻어와서 출력
        if(!printedDrive.contains(String.valueOf(driveCharacter))) {
            String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");

            driveInfo = driveInfo.replace(". 볼", ".\n 볼");

            PromptView.getInstance().printDriveInfo(driveInfo);
            printedDrive.add(String.valueOf(driveCharacter));
        }
    }

    private void showCurrentDirectoryList(PromptData promptData, ArrayList<String> printedDrive) {
        File targetFolder = new File(promptData.getCurrentAbsolutePath());

        File[] files = targetFolder.listFiles();
        ArrayList<File> fileArrayList = new ArrayList<>(Arrays.asList(files));

        if(targetFolder.toPath().getRoot() != targetFolder.toPath()) {
            addDotDirectories(fileArrayList, targetFolder);
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

        char driveCharacter = files[files.length - 1].getAbsolutePath().charAt(0);

        String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");

        driveInfo = driveInfo.replace(". 볼", ".\n 볼");

        PromptView.getInstance().printDriveInfo(driveInfo);
        printedDrive.add(String.valueOf(driveCharacter));

        PromptView.getInstance().printItemList(availableSpace, files, targetFolder.getAbsolutePath());
    }
}
