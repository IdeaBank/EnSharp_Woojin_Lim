package commandMain;

import controller.PromptManager;

import java.io.File;

public class Main {
    private Main() { }

    public static void main(String[] args) {
        PromptManager promptManager = new PromptManager();
        promptManager.startPrompt();
    }
}