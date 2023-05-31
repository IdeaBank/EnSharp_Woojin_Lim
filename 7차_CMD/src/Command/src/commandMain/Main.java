package commandMain;

import model.PromptData;
import util.FileVerifier;

import java.io.File;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.stream.Collectors;

public class Main {
    private Main() { }

    public static void main(String[] args) {
        File[] directories = new File("/Users/Woojin/Development/OpenSW").listFiles();

        for(File file : directories)
        System.out.println(file.getName());
    }
}