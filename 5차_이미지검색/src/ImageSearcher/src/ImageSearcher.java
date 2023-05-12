import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.LineBorder;
import javax.swing.border.TitledBorder;
import java.awt.*;
import java.awt.event.*;
import java.io.File;
import java.net.URL;
import java.nio.file.Paths;

public class ImageSearcher extends Frame {
    private JFrame frame;
    private DisplayMode currentMode;
    private JPanel cards;
    private JPanel searchPanel;
    private JPanel historyPanel;
    private JPanel resultPanel;

    private JTextField searchField;
    private JButton searchButton;
    private JButton historyButton;
    private JButton previousButton;
    private JTextField searchFieldInResult;
    private JButton searchButtonInResult;
    private JButton gotoFirstButton;
    private JComboBox<String> selectImageCounts;

    public ImageSearcher()
    {
        frame = new JFrame();
        currentMode = DisplayMode.SEARCH_MAIN;

        InitializeFrame();

        cards = new JPanel(new CardLayout());
        cards.setOpaque(true);

        InitializeFirstCard();
        InitializeSecondCard();
        InitializeThirdCard();

        // CardLayout에 패널들 추가
        cards.add(searchPanel, "Panel1");
        cards.add(historyPanel, "Panel2");
        cards.add(resultPanel, "Panel3");

        // 전체에 추가
        add(cards);
        // 화면 레이아웃 가져오기
        CardLayout cl = (CardLayout) cards.getLayout();

        frame.setContentPane(cards);

        // Display the window.
        frame.setVisible(true);

        AddListenerToElements(cl);
    }

    private void InitializeFrame()
    {
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        int width = (int)screenSize.getWidth();
        int height = (int)screenSize.getHeight();

        String currentWorkingDirectory = Paths.get("").toAbsolutePath().toString();
        String iconImagePath = currentWorkingDirectory + "/resource/icon.jpg";

        Image iconImage = Toolkit.getDefaultToolkit().getImage(iconImagePath);
        Image updatedImage = iconImage.getScaledInstance(100, 100, Image.SCALE_SMOOTH);

        frame.setIconImage(updatedImage);
        frame.setTitle("Search Image");
        frame.setSize((int)(width * 0.75), (int)(height * 0.75));
        frame.setLocationRelativeTo(null);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private void InitializeFirstCard()
    {
        searchPanel = new JPanel();
        searchField = new JTextField(20);
        searchButton = new JButton("Q");
        historyButton = new JButton("기록 보기");

        searchPanel.add(searchField);
        searchPanel.add(searchButton);
        searchPanel.add(historyButton);
    }

    public void InitializeSecondCard()
    {
        historyPanel = new JPanel();
        previousButton = new JButton("뒤로 가기");
        TitledBorder border2 = new TitledBorder(new LineBorder(Color.black),"View history", TitledBorder.CENTER,TitledBorder.BELOW_TOP);
        historyPanel.setBorder(border2);
        historyPanel.add(previousButton);
    }

    public void InitializeThirdCard()
    {
        String []optionsToChoose = {"10", "20", "30"};
        resultPanel = new JPanel();
        searchFieldInResult = new JTextField(20);
        resultPanel.add(searchFieldInResult);
        searchButtonInResult = new JButton("Q");
        selectImageCounts = new JComboBox<>(optionsToChoose);
        gotoFirstButton = new JButton("뒤로 가기");
        TitledBorder border3 = new TitledBorder(new LineBorder(Color.black),"검색 결과", TitledBorder.CENTER,TitledBorder.BELOW_TOP);
        resultPanel.setBorder(border3);
        resultPanel.add(searchButtonInResult);
        resultPanel.add(selectImageCounts);
        resultPanel.add(gotoFirstButton);
    }

    public void AddListenerToElements(CardLayout cl)
    {
        searchButton.addActionListener(e -> {
            String searchQuery = searchField.getText();
            cl.last(cards);
            currentMode = DisplayMode.DISPLAY_SEARCH_RESULT_FROM_MAIN;
        });

        historyButton.addActionListener(e -> {
            cl.next(cards);
            currentMode = DisplayMode.DISPLAY_HISTORY;
        });

        previousButton.addActionListener(e -> {
            cl.previous(cards);
            currentMode = DisplayMode.DISPLAY_SEARCH_RESULT_FROM_HISTORY;
        });

        gotoFirstButton.addActionListener(actionEvent -> {
            if(currentMode == DisplayMode.DISPLAY_SEARCH_RESULT_FROM_MAIN) {
                cl.first(cards);
                currentMode = DisplayMode.SEARCH_MAIN;
            } else {
                cl.previous(cards);
                currentMode = DisplayMode.DISPLAY_HISTORY;
            }
        });

        selectImageCounts.addActionListener(actionEvent -> {
            switch(selectImageCounts.getSelectedItem().toString())
            {
                case "10":
                    System.out.println(10);
                    break;
                case "20":
                    System.out.println(20);
                    break;
                default:
                    System.out.println(30);
            }
        });

        searchField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if(e.getKeyCode() == KeyEvent.VK_ENTER){
                    DisplaySearchResult(searchField.getText());
                    searchField.setText("");
                }
            }
        });

        searchField.setForeground(Color.gray);
        searchField.setText("검색어를 입력하세요");
        searchField.addFocusListener(new FocusListener() {
            @Override
            public void focusGained(FocusEvent e) {
                if (searchField.getText().equals("검색어를 입력하세요") && searchField.getForeground() == Color.gray) {
                    searchField.setText("");
                    searchField.setForeground(Color.BLACK);
                }
            }
            @Override
            public void focusLost(FocusEvent e) {
                if (searchField.getText().isEmpty()) {
                    searchField.setForeground(Color.gray);
                    searchField.setText("검색어를 입력하세요");
                }
            }
        });

        searchFieldInResult.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if(e.getKeyCode() == KeyEvent.VK_ENTER){
                    DisplaySearchResult(searchFieldInResult.getText());
                    searchFieldInResult.setText("");
                }
            }
        });

        searchFieldInResult.setForeground(Color.gray);
        searchFieldInResult.setText("검색어를 입력하세요");
        searchFieldInResult.addFocusListener(new FocusListener() {
            @Override
            public void focusGained(FocusEvent e) {
                if (searchFieldInResult.getText().equals("검색어를 입력하세요") && searchFieldInResult.getForeground() == Color.gray) {
                    searchFieldInResult.setText("");
                    searchFieldInResult.setForeground(Color.BLACK);
                }
            }
            @Override
            public void focusLost(FocusEvent e) {
                if (searchFieldInResult.getText().isEmpty()) {
                    searchFieldInResult.setForeground(Color.gray);
                    searchFieldInResult.setText("검색어를 입력하세요");
                }
            }
        });
    }

    public void DisplaySearchResult(String query)
    {

    }

    public void DisplayImagesWithCount(int count)
    {

    }
}
