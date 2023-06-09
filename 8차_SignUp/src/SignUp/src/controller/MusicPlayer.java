package controller;

import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;
import java.io.File;

public class MusicPlayer {
    private static MusicPlayer _instance;
    private Clip clip;

    private MusicPlayer() {
        try {
            AudioInputStream audioInputStream =
                    AudioSystem.getAudioInputStream(new File("etc/SuperMarioBros.wav").getAbsoluteFile());

            // create clip reference
            clip = AudioSystem.getClip();

            // open audioInputStream to the clip
            clip.open(audioInputStream);

            clip.loop(Clip.LOOP_CONTINUOUSLY);
        }
        catch(Exception e){
            e.printStackTrace();
        }
    }

    public static MusicPlayer getInstance() {
        if(_instance == null) {
            _instance = new MusicPlayer();
        }

        return _instance;
    }

    public void playMusic(String path) {
        if(clip.isOpen()) {
            clip.close();
        }

        try {
            AudioInputStream audioInputStream =
                    AudioSystem.getAudioInputStream(new File(path).getAbsoluteFile());

            // open audioInputStream to the clip
            clip.open(audioInputStream);
            clip.loop(1);
        }
        catch(Exception e){
            e.printStackTrace();
        }
    }

    public void playBackgroundMusic() {
        if(clip.isOpen()) {
            clip.close();
        }

        try {
            AudioInputStream audioInputStream =
                    AudioSystem.getAudioInputStream(new File("etc/SuperMarioBros.wav").getAbsoluteFile());

            // open audioInputStream to the clip
            clip.open(audioInputStream);

            clip.loop(10);
        }
        catch(Exception e){
            e.printStackTrace();
        }
    }
}
