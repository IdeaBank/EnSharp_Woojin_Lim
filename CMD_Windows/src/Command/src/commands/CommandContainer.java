package commands;

import javax.swing.text.View;

public class CommandContainer {
    private static CommandContainer _instance;
    private ChangeDirectory changeDirectory;
    private ViewDirectory viewDirectory;

    private CommandContainer() {
        this.changeDirectory = new ChangeDirectory();
        this.viewDirectory = new ViewDirectory();
    }

    public static CommandContainer getInstance() {
        if(_instance == null) {
            _instance = new CommandContainer();
        }

        return _instance;
    }

    public ChangeDirectory getChangeDirectory() {
        return changeDirectory;
    }

    public ViewDirectory getViewDirectory() {
        return viewDirectory;
    }
}
