package commands;

import javax.swing.text.View;

public class CommandContainer {
    private static CommandContainer _instance;
    private ChangeDirectory changeDirectory;
    private ViewDirectory viewDirectory;
    private ClearConsole clearConsole;
    private CopyFile copyFile;
    private Help help;

    private CommandContainer() {
        this.changeDirectory = new ChangeDirectory();
        this.viewDirectory = new ViewDirectory();
        this.clearConsole = new ClearConsole();
        this.help = new Help();
        this.copyFile = new CopyFile();
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

    public ClearConsole getClearConsole() {
        return clearConsole;
    }
    public CopyFile getCopyFile() {
        return copyFile;
    }

    public Help getHelp() {
        return help;
    }
}
