package commandMain;

import controller.PromptManager;
import util.ItemVerifier;
import view.PromptView;

import javax.swing.filechooser.FileSystemView;
import java.io.File;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.List;
import java.util.Locale;

public class Main {

    public static void main(String[] args) {
        PromptManager promptManager = new PromptManager();
        promptManager.startPrompt();

        //System.out.println(System.getProperty("os.version"));
    }
}