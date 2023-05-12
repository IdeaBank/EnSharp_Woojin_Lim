import DAO.ImageInformationDAO;
import DTO.ImageInformationDTO;

import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;
import javax.swing.border.TitledBorder;
import java.awt.*;
import java.awt.event.*;
import java.io.File;
import java.lang.reflect.Array;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.file.Paths;
import java.util.ArrayList;

public class ImageSearcher extends Frame {
    private JFrame frame;
    private DisplayMode currentMode;
    private JPanel cards;
    private JPanel searchPanel;
    private JPanel historyPanel;
    private JPanel resultPanel;
    private JPanel resultPanelCover;

    private JTextField searchField;
    private JButton searchButton;
    private JButton historyButton;
    private JButton previousButton;
    private JTextField searchFieldInResult;
    private JButton searchButtonInResult;
    private JButton gotoFirstButton;
    private JComboBox<String> selectImageCounts;
    private ArrayList<ImageInformationDTO> imageInformationDTOList;

    public ImageSearcher() throws Exception
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
        cards.add(resultPanelCover, "Panel3");

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
        resultPanelCover = new JPanel(new BorderLayout());
        JPanel searchPanel = new JPanel(new FlowLayout());

        resultPanel = new JPanel(new GridLayout());
        searchFieldInResult = new JTextField(20);
        searchButtonInResult = new JButton("Q");
        selectImageCounts = new JComboBox<>(optionsToChoose);
        gotoFirstButton = new JButton("뒤로 가기");
        resultPanel.setComponentOrientation(ComponentOrientation.LEFT_TO_RIGHT);

        searchPanel.add(searchFieldInResult);
        searchPanel.add(searchButtonInResult);
        searchPanel.add(selectImageCounts);
        searchPanel.add(gotoFirstButton);

        resultPanelCover.add(searchPanel, BorderLayout.NORTH);
        resultPanelCover.add(resultPanel, BorderLayout.CENTER);
    }

    public void AddListenerToElements(CardLayout cl) throws Exception
    {
        searchButton.addActionListener(e -> {
            String searchQuery = searchField.getText();

            try {
                DisplaySearchResult(searchQuery, "10");
            } catch (Exception ex) {
                throw new RuntimeException(ex);
            }

            cl.last(cards);
            currentMode = DisplayMode.DISPLAY_SEARCH_RESULT_FROM_MAIN;
        });

        historyButton.addActionListener(e -> {
            cl.next(cards);
            currentMode = DisplayMode.DISPLAY_HISTORY;
        });

        previousButton.addActionListener(e -> {
            cl.first(cards);
            currentMode = DisplayMode.SEARCH_MAIN;
        });

        searchButtonInResult.addActionListener(e -> {
            String searchQuery = searchFieldInResult.getText();
            try {
                DisplaySearchResult(searchQuery, selectImageCounts.getSelectedItem().toString());
            } catch (Exception ex) {
                throw new RuntimeException(ex);
            }
            cl.last(cards);
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
            try {
                DisplaySearchResult(searchField.getText(), selectImageCounts.getSelectedItem().toString());
            } catch (Exception e) {
                throw new RuntimeException(e);
            }

            cl.last(cards);
        });

        searchField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if(e.getKeyCode() == KeyEvent.VK_ENTER){
                    try {
                        DisplaySearchResult(searchField.getText(), "10");
                    } catch (Exception ex) {
                        throw new RuntimeException(ex);
                    }
                    searchField.setText("");

                    currentMode = DisplayMode.DISPLAY_SEARCH_RESULT_FROM_MAIN;
                    cl.last(cards);
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
                    try {
                        DisplaySearchResult(searchFieldInResult.getText(), selectImageCounts.getSelectedItem().toString());
                    } catch (Exception ex) {
                        throw new RuntimeException(ex);
                    }
                    searchFieldInResult.setText("");

                    cl.last(cards);
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

    public void DisplaySearchResult(String query, String countString) throws Exception {
        int count = Integer.parseInt(countString);

        imageInformationDTOList = ImageInformationDAO.GetInstance().SearchImageWithQuery(query);

        DisplayImagesWithCount(count);
    }

    public void DisplayImagesWithCount(int count)
    {
        resultPanelCover.remove(resultPanel);
        resultPanel = new JPanel(new GridLayout(count / 5, 5));

        for(int i = 0; i < count; ++i)
        {
            Image img;
            try {
                img = new ImageIcon(new URL(imageInformationDTOList.get(i).GetThumbnailUrl())).getImage();
            } catch (MalformedURLException e) {
                throw new RuntimeException(e);
            }
            ImageIcon imageIcon = new ImageIcon(img);

            JLabel tempLabel = new JLabel();
            tempLabel.setIcon(imageIcon);

            final int currentIndex = i;

            tempLabel.addMouseListener(new MouseListener() {
                @Override
                public void mouseClicked(MouseEvent mouseEvent) {
                    JFrame imageView = new JFrame();

                    Image image;

                    try {
                        image = new ImageIcon(new URL(imageInformationDTOList.get(currentIndex).GetImageUrl())).getImage();
                    } catch (MalformedURLException e) {
                        throw new RuntimeException(e);
                    }
                    ImageIcon imageIcon = new ImageIcon(image);

                    JLabel imageLabel = new JLabel();
                    imageLabel.setIcon(imageIcon);

                    imageView.add(imageLabel);

                    imageView.pack();
                    imageView.setVisible(true);
                    imageView.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
                }

                @Override
                public void mousePressed(MouseEvent mouseEvent) {

                }

                @Override
                public void mouseReleased(MouseEvent mouseEvent) {

                }

                @Override
                public void mouseEntered(MouseEvent mouseEvent) {

                }

                @Override
                public void mouseExited(MouseEvent mouseEvent) {

                }
            });

            resultPanel.add(tempLabel);
        }

        resultPanelCover.add(resultPanel, BorderLayout.CENTER);
    }
}
