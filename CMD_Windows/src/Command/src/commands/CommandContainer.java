package commands;

public class CommandContainer {
    private static CommandContainer _instance;
    private ChangeDirectory changeDirectory;

    private CommandContainer() {
        this.changeDirectory = new ChangeDirectory();
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
}
