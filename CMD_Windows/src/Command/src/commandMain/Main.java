package commandMain;

import controller.PromptManager;
import util.ItemVerifier;
import view.PromptView;

import javax.swing.filechooser.FileSystemView;
import java.io.File;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.List;

public class Main {
    private Main() { }

    public static void main(String[] args) {
        PromptManager promptManager = new PromptManager();
        promptManager.startPrompt();
    }
}