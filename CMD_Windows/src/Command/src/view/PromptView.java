package view;


import java.io.File;
import java.nio.file.FileStore;
import java.text.DateFormat;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class PromptView {

    // Singleton 사용
    private static PromptView _instance;

    public static PromptView getInstance() {
        if(_instance == null) {
            _instance = new PromptView();
        }

        return _instance;
    }

    private PromptView() {

    }

    public void printPromptInfo(String os, String buildVersion) {
        if(os.startsWith("Windows")) {
            System.out.println(buildVersion);
            System.out.println("(c) Microsoft Corporation. All rights reserved.");
        }

        else {
            System.out.println("MAC OS");
            System.out.println("Apple Inc.");
        }
    }

    public void printWorkingDirectory(String workingDirectoryPath) {
        System.out.println(workingDirectoryPath);
    }

    public void printDriveInfo(String driveInfo) {
        System.out.println(driveInfo + "\n");
    }

    public void printItemList(long availableSpace, File[] files) {
        int fileCount = 0, directoryCount = 0, totalSize = 0;

        System.out.println(" " + files[files.length - 1].toPath().getParent() + " 디렉터리\n");

        for(File file: files) {
            if(!file.isHidden()) {
                if (file.isFile()) {
                    printItemInfo(file);
                    fileCount += 1;
                    totalSize += file.length();
                    System.out.println(file.getName());
                }

                else if (file.isDirectory()) {
                    printItemInfo(file);
                    directoryCount += 1;
                    System.out.println(file.getName());
                }

            }
        }

        DecimalFormat formatter = new DecimalFormat("#,###");

        System.out.printf("              %d개 파일                 %d 바이트\n", fileCount, totalSize);
        System.out.printf("              %d개 디렉터리  %s 바이트 남음\n", directoryCount, formatter.format(availableSpace));
    }

    private void printItemInfo(File file) {
        long lastModifiedTimestamp = file.lastModified();
        Date lastModifiedDate = new Date(lastModifiedTimestamp);
        DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd  a hh:mm    ");
        String lastModified = dateFormat.format(lastModifiedDate);

        if(file.isFile()) {
            System.out.print(lastModified);
            System.out.printf("%14d ", file.length());
        }

        else if(file.isDirectory()) {
            System.out.print(lastModified);
            System.out.printf("%-14s ", "<DIR>");
        }
    }

    public void clearPrompt() {

    }

    public void printHelp() {

    }

    public void printCopyResult() {

    }

    public void printMoveResult() {

    }

    public void printError(String str) {
        System.out.println(str);
    }
}
