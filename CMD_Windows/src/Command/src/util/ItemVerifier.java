package util;

import constant.ItemType;

import java.io.File;
import java.util.ArrayList;
import java.util.Stack;

public class ItemVerifier {
    private static ItemVerifier _instance;

    public static ItemVerifier getInstance() {
        if(_instance == null) {
            _instance = new ItemVerifier();
        }

        return _instance;
    }

    private ItemVerifier() {

    }

    public ItemType getItemState(String path) {
        if(!isItemExist(path)) {
            return ItemType.ITEM_DOES_NOT_EXIST;
        }

        File file = new File(path);

        if(file.isFile()) {
            return ItemType.IS_FILE;
        }

        return ItemType.IS_DIRECTORY;
    }

    public boolean isItemExist(String path) {
        File file = new File(path);

        return file.exists();
    }

    public String getUpperDirectoryPath(String path) {
        String[] splitPath = path.split("[\\\\]");
        Stack<String> stringStack = new Stack<>();

        for(String str: splitPath) {
            if(str.equals("..")) {
                if(splitPath.length != 2) {
                    if(stringStack.size() > 0) {
                        stringStack.pop();
                    }
                    else {
                        return null;
                    }
                }
            }

            else if(str.equals(".")) {
                continue;
            }

            else if(!str.equals("")){
                stringStack.push(str);
            }
        }

        if(stringStack.size() != 1) {
            return String.join("\\", stringStack.toArray(new String[0]));
        }

        return stringStack.toArray()[0] + "\\";
    }
}
