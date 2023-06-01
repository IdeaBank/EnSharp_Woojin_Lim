package view;

import model.PromptData;

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

    public void printPromptInfo(String os) {
        System.out.println(FileSystemView.getFileSystemView().getSystemDisplayName(new File("C:/Users")));
    }

    public void printWorkingDirectory(String workingDirectoryPath) {
        System.out.println(workingDirectoryPath);
    }

    public void printItemInfo(String os, File[] files) {

    }

    public void clearPrompt(String os) {

    }

    public void printHelp(String os) {

    }

    public void printCopyResult() {

    }

    public void printMoveResult() {

    }

    public void printError(String str) {
        System.out.println(str);
    }
}
