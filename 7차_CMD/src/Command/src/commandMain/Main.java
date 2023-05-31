package commandMain;

import java.io.File;

public class Main {
    private Main() { }

    public static void main(String[] args) {
        File[] directories = new File("/Users/Woojin/Development/OpenSW").listFiles();

        for(File file : directories)
        System.out.println(file.getName());
    }
}