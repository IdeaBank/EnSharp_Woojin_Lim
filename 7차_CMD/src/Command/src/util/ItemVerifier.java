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

    public ItemVerifier() {

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
        String[] splitPath = path.split("[/]");
        Stack<String> stringStack = new Stack<>();

        for(String str: splitPath) {
            if(str.equals("..")) {
                stringStack.pop();
            }

            else {
                stringStack.push(str);
            }
        }

        Stack<String> reverseStack = new Stack<>();

        int stringStackSize = stringStack.size();

        for(int i = 0; i < stringStackSize; ++i) {
            reverseStack.push(stringStack.pop());
        }

        ArrayList<String> result = new ArrayList<>();

        for(int i = 0; i < stringStackSize; ++i) {
            result.add(reverseStack.pop());
        }

        return String.join("/", result.toArray(new String[0]));
    }
}
