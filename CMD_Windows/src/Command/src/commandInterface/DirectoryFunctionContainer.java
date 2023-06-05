package commandInterface;

import java.io.File;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;

public class DirectoryFunctionContainer {
    public File[] getAllDirectoryAndFile(String path) {
        File targetFolder = new File(path);

        if(targetFolder.isFile()) {
            return new File[] { targetFolder };
        }

        else if(targetFolder.isDirectory()) {
            File[] tempFiles = targetFolder.listFiles();

            return removeJunctionFileFromList(tempFiles);
        }

        return new File[] { };
    }

    private File[] removeJunctionFileFromList(File []files) {
        ArrayList<File> result = new ArrayList<>();

        for(File file: files) {
            if((file.isFile() || file.isDirectory()) && !isJunctionFile(file)) {
                result.add(file);
            }
        }

        return result.toArray(new File[0]);
    }

    private boolean isJunctionFile(File file) {
        Path path = file.toPath();
        boolean isJunction = false;

        try {
            isJunction = path.compareTo(path.toRealPath()) != 0;
        }
        catch(Exception e) {
            e.printStackTrace();
        }

        return isJunction;
    }
}
