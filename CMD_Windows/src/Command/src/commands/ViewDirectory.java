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

public class ViewDirectory implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        // 정상적인 명령어이면 경로에 있는 파일들 출력
        if(isCommandValid(command) == CommandResultType.SUCCESS) {
            viewDirectoryFiles(promptData, command);
        }

        // 비정상적인 명령어이면 오류 문구 출력
        else {
            PromptView.getInstance().printMessage("'" + command.split(" ")[0] + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
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
                targetFolder = new File(promptData.getCurrentAbsolutePath(), path);
                targetFolder = new File(ItemVerifier.getInstance().getUpperDirectoryPath(targetFolder.getPath()));
            }

            File[] files;

            long availableSpace = 0;

            try {
                // 최상위 드라이브를 얻어와서 남은 공간을 구함
                File file = new File(targetFolder.getPath().charAt(0) + ":\\");

                FileStore store = Files.getFileStore(file.toPath());

                availableSpace = store.getUsableSpace();

            } catch(IOException e) {
                e.printStackTrace();
            }

            // 경로가 존재하면
            if(targetFolder.exists()) {
                // 폴더면
                if(targetFolder.isDirectory()) {
                    // 모든 폴더 및 파일 받아와서 나열
                    files = targetFolder.listFiles();
                    ArrayList<File> fileArrayList = new ArrayList<>(Arrays.asList(files));

                    // 최상위 폴더가 아니면 .과 ..을 추가
                    if(targetFolder.toPath().getRoot() != targetFolder.toPath()) {
                        fileArrayList.add(0, new File(".."));
                        fileArrayList.add(0, new File("."));
                    }

                    files = fileArrayList.toArray(new File[0]);
                }

                // 파일이면 해당 파일 하나만 출력
                else {
                     files = new File[] { targetFolder };
                }

                // 현재 경로가 위치해 있는 드라이브의 문자를 얻어옴
                char driveCharacter = files[files.length - 1].getPath().charAt(0);

                // 드라이브의 정보를 얻어와서 출력
                if(!printedDrive.contains(String.valueOf(driveCharacter))) {
                    String driveInfo = PromptManager.getCommandExecuteResult("vol " + driveCharacter + ":");

                    driveInfo = driveInfo.replace(". 볼", ".\n 볼");

                    PromptView.getInstance().printDriveInfo(driveInfo);
                    printedDrive.add(String.valueOf(driveCharacter));
                }

                // 파일들 출력
                PromptView.getInstance().printItemList(availableSpace, files);
            }
        }

        // dir만 쳤을 경우 현재 경로에 있는 폴더 및 파일들 출력
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
