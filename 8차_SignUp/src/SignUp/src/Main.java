import controller.MusicPlayer;
import view.LoginFrame;

public class Main {
    public static void main(String[] args) {
        LoginFrame loginFrame = new LoginFrame();
        loginFrame.start();

        MusicPlayer.getInstance().playBackgroundMusic();
    }
}