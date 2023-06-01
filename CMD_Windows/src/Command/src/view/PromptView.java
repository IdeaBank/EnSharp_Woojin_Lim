package view;


import javax.swing.filechooser.FileSystemView;
import java.io.File;

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

    public void printItemInfo(String driveInfo, File[] files) {
        System.out.println(driveInfo);

        for(File file: files) {
            System.out.println(file.getName());
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
