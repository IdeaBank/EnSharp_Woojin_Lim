package commandMain;

import controller.PromptManager;

public class Main {

    public static void main(String[] args) {
        // command창 시작
        PromptManager promptManager = new PromptManager();
        promptManager.startPrompt();
    }
}