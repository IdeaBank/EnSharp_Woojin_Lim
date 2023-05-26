package dispatcher;

import constant.CalculatorState;
import constant.CalculatorSymbols;
import reducer.CustomListRenderer;
import store.DataStore;
import store.HistoryStore;
import view.CalculationLog;

import javax.swing.*;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import java.awt.*;
import java.awt.font.FontRenderContext;
import java.awt.geom.AffineTransform;
import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;

public class CalculatorDispatcher {
    private final ArrayList<HistoryStore> calculatorHistories;
    private JList historyList;
    private JTextPane historyPane;
    private JTextPane inputPane;
    private JPanel glassPane;
    private CalculatorState calculatorState;
    private DataStore calculationData;
    private DefaultListModel<CalculationLog> logList;

    public CalculatorDispatcher() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
        this.calculationData = new DataStore();
        logList = new DefaultListModel<>();
    }

    private static String format(BigDecimal x) {
        NumberFormat formatter = new DecimalFormat("0.0E0");
        formatter.setRoundingMode(RoundingMode.HALF_UP);
        formatter.setMinimumFractionDigits((x.scale() > 0) ? x.precision() : x.scale());
        return formatter.format(x);
    }

    public void setHistoryList(JList historyList) {
        this.historyList = historyList;
        this.historyList.setCellRenderer(new CustomListRenderer());
    }

    public void setHistoryPane(JTextPane historyPane) {
        this.historyPane = historyPane;
    }

    public void setInputPane(JTextPane inputPane) {
        this.inputPane = inputPane;
    }

    public void setGlassPanel(JPanel glassPane) {
        this.glassPane = glassPane;
    }

    public boolean isGlassPaneVisible() {
        return glassPane.isVisible();
    }

    public void handleButtonPressed(String text) {
        switch (text) {
            case CalculatorSymbols.CE:
                clearEntry();
                break;

            case CalculatorSymbols.C:
                clear();
                break;

            case CalculatorSymbols.DEL:
                deleteEntry();
                break;

            case CalculatorSymbols.DIVIDE:
            case CalculatorSymbols.DIVIDE_SYMBOL:
                getCalculation(CalculatorSymbols.DIVIDE_CHAR);
                break;

            case CalculatorSymbols.MULTIPLY:
            case CalculatorSymbols.MULTIPLY_SYMBOL:
                getCalculation(CalculatorSymbols.MULTIPLY_CHAR);
                break;

            case CalculatorSymbols.SUBTRACT:
                getCalculation(CalculatorSymbols.SUBTRACT_CHAR);
                break;

            case CalculatorSymbols.ADD:
                getCalculation(CalculatorSymbols.ADD_CHAR);
                break;

            case CalculatorSymbols.EQUALS:
                calculate();
                if (calculatorState != CalculatorState.ERROR && calculatorState != CalculatorState.START)
                    calculatorState = CalculatorState.ENTER_KEY_PRESSED;
                break;

            case CalculatorSymbols.NEGATE:
                negate();
                break;

            case CalculatorSymbols.DOT:
                appendDot();
                break;

            case CalculatorSymbols.ZERO:
            case CalculatorSymbols.ONE:
            case CalculatorSymbols.TWO:
            case CalculatorSymbols.THREE:
            case CalculatorSymbols.FOUR:
            case CalculatorSymbols.FIVE:
            case CalculatorSymbols.SIX:
            case CalculatorSymbols.SEVEN:
            case CalculatorSymbols.EIGHT:
            case CalculatorSymbols.NINE:
                appendNumber(text.charAt(0));
                break;
        }
    }

    public void clearEntry() {
        if (calculatorState == CalculatorState.ERROR) {
            resetHistory();
            calculationData.setFirstOperand(null);
            calculationData.setSecondOperand(null);
            calculationData.setOperatorChar('\0');
            calculatorState = CalculatorState.START;
        }

        updateInputPane("0");
    }

    public void clear() {
        resetHistory();
        clearEntry();
        calculationData.setFirstOperand(null);
        calculationData.setSecondOperand(null);
        calculationData.setOperatorChar('\0');
        calculatorState = CalculatorState.START;
    }

    public void deleteEntry() {
        boolean isEndWithDot = inputPane.getText().substring(0, inputPane.getText().length() - 1).endsWith(".");

        if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            updateHistoryPane("");
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
        } else if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            String inputString = inputPane.getText();
            inputString = String.join("", inputString.split(","));

            if (inputString.length() == 1 || (inputString.length() == 2 && inputString.startsWith("-"))) {
                inputString = "0";
            } else {
                inputString = inputString.substring(0, inputString.length() - 1);
            }

            if (new BigDecimal(inputString).compareTo(new BigDecimal("0")) != 0) {
                updateInputPane(getDecimalWithFormat(new BigDecimal(inputString), true));
            } else {
                if (inputString.endsWith(".")) {
                    updateInputPane(inputString.substring(0, inputString.length() - 1));
                } else {
                    updateInputPane(inputString);
                }
            }

            if (isEndWithDot) {
                updateInputPane(inputPane.getText() + '.');
            }
        }
    }

    private void checkOverflow() {
        if (calculationData.getFirstOperand().abs().compareTo(new BigDecimal("1e+10000")) > 0) {
            updateHistoryPane("");
            updateInputPane("오버플로");
            calculatorState = CalculatorState.ERROR;
        } else if (calculationData.getFirstOperand().abs().compareTo(new BigDecimal("1e-10000")) < 0 && calculationData.getFirstOperand().abs().compareTo(new BigDecimal("0")) > 0) {
            updateHistoryPane("");
            updateInputPane("오버플로");
            calculatorState = CalculatorState.ERROR;
        }
    }

    public void divide() {
        if (calculationData.getSecondOperand().compareTo(new BigDecimal(0)) == 0) {
            resetHistory();

            if (calculationData.getFirstOperand().compareTo(new BigDecimal(0)) == 0) {
                updateInputPane("정의되지 않은 결과입니다.");
            } else {
                updateInputPane("0으로 나눌 수 없습니다");
            }

            calculatorState = CalculatorState.ERROR;
            return;
        } else {
            calculationData.setFirstOperand(calculationData.getFirstOperand().divide(calculationData.getSecondOperand(), 20000, RoundingMode.HALF_UP));
        }

        checkOverflow();
    }

    public void multiply() {
        calculationData.setFirstOperand(calculationData.getFirstOperand().multiply(calculationData.getSecondOperand(), MathContext.UNLIMITED));

        checkOverflow();
    }

    public void doCalculationWithOperator()
    {
        switch (calculationData.getOperatorChar()) {
            case CalculatorSymbols.ADD_CHAR:
                addHistory(new DataStore());
                calculationData.setFirstOperand(calculationData.getFirstOperand().add(calculationData.getSecondOperand()));
                break;
            case CalculatorSymbols.SUBTRACT_CHAR:
                calculationData.setFirstOperand(calculationData.getFirstOperand().subtract(calculationData.getSecondOperand()));
                break;
            case CalculatorSymbols.MULTIPLY_CHAR:
                multiply();
                break;
            case CalculatorSymbols.DIVIDE_CHAR:
                divide();
                break;
        }
    }

    public void getCalculation(char operatorChar) {
        if (calculatorState == CalculatorState.ERROR) {
            return;
        } else if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED || calculatorState == CalculatorState.START) {
            if (calculationData.getFirstOperand() == null) {
                calculationData.setFirstOperand(getInputDecimal());

                BigDecimal historyDecimal = getInputDecimal();
                updateHistoryPane(getFormattedHistoryDecimal(historyDecimal) + " " + operatorChar);

                if (calculationData.getSecondOperand() != null) {
                    doCalculationWithOperator();

                    if (calculatorState == CalculatorState.ERROR) {
                        return;
                    }

                    updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));

                    BigDecimal calculatedDecimal = getInputDecimal();
                    updateHistoryPane(getFormattedHistoryDecimal(calculatedDecimal) + " " + operatorChar);
                }

                calculationData.setOperatorChar(operatorChar);
                calculatorState = CalculatorState.OPERATION_KEY_PRESSED;

                return;
            } else {
                if (calculationData.getSecondOperand() == null) {
                    calculationData.setSecondOperand(getInputDecimal());
                }
            }

            if (calculationData.getSecondOperand() != null) {

                doCalculationWithOperator();

                if (calculatorState == CalculatorState.ERROR) {
                    return;
                }

                updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));

                BigDecimal calculatedDecimal = getInputDecimal();
                updateHistoryPane(getFormattedHistoryDecimal(calculatedDecimal) + " " + operatorChar);

                calculatorState = CalculatorState.OPERATION_KEY_PRESSED;

                calculationData.setOperatorChar(operatorChar);

                return;
            }
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {

            updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));

            calculationData.setOperatorChar(operatorChar);

            BigDecimal calculatedDecimal = getInputDecimal();
            updateHistoryPane(getFormattedHistoryDecimal(calculatedDecimal) + " " + operatorChar);

        } else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            calculationData.setOperatorChar(operatorChar);
            updateHistoryPane(getFormattedHistoryDecimal(calculationData.getFirstOperand()) + " " + operatorChar);

            updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));
        }

        calculationData.setOperatorChar(operatorChar);

        if (calculatorState != CalculatorState.START) {
            calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
        }
    }

    public void negate() {
        if (inputPane.getText().equals("0")) {
            return;
        }

        if (inputPane.getText().startsWith("-")) {
            updateInputPane(inputPane.getText().substring(1));
        } else {
            updateInputPane("-" + inputPane.getText());
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendNumber(char inputNumber) {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            calculationData.setSecondOperand(null);
            updateInputPane(String.valueOf(inputNumber));
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            updateInputPane(String.valueOf(inputNumber));
            calculationData.setFirstOperand(null);

            if (calculationData.getOperatorChar() != '\0') {
                updateHistoryPane("");
            }
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
            updateInputPane(String.valueOf(inputNumber));
        } else {
            String stringWithoutComma = String.join("", inputPane.getText().split(","));
            String stringWithoutDot = String.join("", stringWithoutComma.split("[.]"));

            if ((stringWithoutDot.startsWith("0") && stringWithoutDot.length() > 16) || (!stringWithoutDot.startsWith("0") && stringWithoutDot.length() > 15)) {
                return;
            }

            if (inputPane.getText().equals("0")) {
                updateInputPane(String.valueOf(inputNumber));
            } else {
                updateInputPane(getFormattedInputDecimal(inputPane.getText() + inputNumber));
            }
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendDot() {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED || calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            updateInputPane("0.");
        }

        if (!inputPane.getText().contains(".")) {
            updateInputPane(inputPane.getText() + ".");
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void calculate() {
        if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if (calculationData.getOperatorChar() == '\0') {

                calculationData.setFirstOperand(getInputDecimal());
                updateHistoryPane(getFormattedHistoryDecimal(calculationData.getFirstOperand()) + " =");
                calculatorState = CalculatorState.ENTER_KEY_PRESSED;
            } else {
                String temp;

                if (calculationData.getFirstOperand() != null) {
                    temp = getFormattedHistoryDecimal(calculationData.getFirstOperand());
                    calculationData.setSecondOperand(getInputDecimal());

                } else {
                    temp = getInputDecimal().stripTrailingZeros().toPlainString();
                }

                getCalculation(calculationData.getOperatorChar());

                if (calculatorState != CalculatorState.ERROR) {
                    updateHistoryPane(temp + " " + calculationData.getOperatorChar() + " " + calculationData.getSecondOperand().toPlainString() + " =");
                    updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));
                }
            }
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {

            if (calculationData.getOperatorChar() == '\0')
                updateHistoryPane(historyPane.getText().substring(0, historyPane.getText().length() - 2) + " =");

            else {
                BigDecimal inputDecimal = getInputDecimal();
                String temp = getFormattedHistoryDecimal(inputDecimal);

                doCalculationWithOperator();

                if (calculatorState == CalculatorState.ERROR) {
                    return;
                }

                updateHistoryPane(temp + " " + calculationData.getOperatorChar() + " " + calculationData.getSecondOperand().toPlainString() + " =");
                updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));

            }
        } else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            calculationData.setSecondOperand(new BigDecimal(calculationData.getFirstOperand().toPlainString()));
            String temp = getFormattedHistoryDecimal(calculationData.getFirstOperand());

            doCalculationWithOperator();

            if (calculatorState == CalculatorState.ERROR) {
                return;
            }

            updateHistoryPane(temp + " " + calculationData.getOperatorChar() + " " + temp + " =");
            updateInputPane(getDecimalWithFormat(calculationData.getFirstOperand(), true));
        }

        calculatorState = CalculatorState.ENTER_KEY_PRESSED;
    }

    private void resetHistory() {
        updateHistoryPane("");
    }

    private String getDecimalWithFormat(BigDecimal targetDecimal, boolean formatDecimal) {
        targetDecimal = targetDecimal.stripTrailingZeros();

        if (targetDecimal.compareTo(new BigDecimal("0")) == 0) {
            return targetDecimal.toPlainString();
        }

        if (targetDecimal.compareTo(new BigDecimal("0.001")) < 0 && targetDecimal.compareTo(new BigDecimal("0")) > 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toPlainString());
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("1")) < 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            if (tempResult.toPlainString().length() > 16) {
                if (tempResult.toPlainString().substring(0, 16).contains(".")) {
                    return tempResult.toPlainString().substring(0, 16) + "e-" + count;
                }

                return tempResult.toPlainString().substring(0, 15) + "e-" + count;
            }

            return tempResult.toPlainString() + "e-" + count;
        }

        else if (targetDecimal.compareTo(new BigDecimal("-0.001")) > 0 && targetDecimal.compareTo(new BigDecimal("0")) < 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toPlainString());
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("-1")) > 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            if (tempResult.toPlainString().length() > 18) {
                if (tempResult.toPlainString().substring(0, 17).contains(".")) {
                    return tempResult.toPlainString().substring(0, 17) + "e-" + count;
                } else {
                    return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
                }
            } else {
                if (tempResult.toPlainString().length() > 16) {
                    return tempResult.toPlainString().substring(0, 16) + "e-" + count;
                } else {
                    return tempResult.toPlainString() + "e-" + count;
                }
            }
        } else if (targetDecimal.compareTo(new BigDecimal("9999999999999999")) > 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("10")) > 0) {
                tempResult = tempResult.divide(new BigDecimal("10"), 100, RoundingMode.HALF_UP);
                count += 1;
            }

            if (tempResult.compareTo(new BigDecimal("1")) > 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"), MathContext.UNLIMITED);
                count -= 1;
            }

            if (tempResult.toPlainString().length() > 17) {
                return tempResult.toPlainString().substring(0, 16) + "e+" + count;
            } else {
                return tempResult.toPlainString() + "e+" + count;
            }
        } else if (targetDecimal.compareTo(new BigDecimal("-9999999999999999")) < 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("-10")) < 0) {
                tempResult = tempResult.divide(new BigDecimal("10"), 100, RoundingMode.HALF_UP);
                count += 1;
            }

            if (tempResult.compareTo(new BigDecimal("-1")) < 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"), MathContext.UNLIMITED);
                count -= 1;
            }

            if (tempResult.toPlainString().length() > 18) {
                return tempResult.toPlainString().substring(0, 17) + "e+" + count;
            } else {
                return tempResult.toPlainString() + "e+" + count;
            }
        }
        else {
            String result = targetDecimal.toPlainString();

            if (result.contains(".") && !result.endsWith(".")) {
                if (result.length() > 18) {
                    result = result.substring(0, 18);
                }
            } else if (result.length() > 16) {
                result = result.substring(0, 16);
            }

            DecimalFormat formatter;
            BigDecimal tempResult = new BigDecimal(result);

            if (!formatDecimal) {
                if (tempResult.toPlainString().length() > 16) {
                    return tempResult.setScale(tempResult.scale() - 1, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString();
                }

                return tempResult.toPlainString();
            }

            tempResult = tempResult.setScale(16, RoundingMode.HALF_UP);

            if (tempResult.toPlainString().contains(".")) {

                int scale = tempResult.scale();
                tempResult = tempResult.setScale(scale - 1, RoundingMode.HALF_UP);
                tempResult = tempResult.stripTrailingZeros();

                if (!tempResult.stripTrailingZeros().toPlainString().contains(".")) {
                    formatter = new DecimalFormat("#,##0");

                    return formatter.format(tempResult.stripTrailingZeros());
                }

                formatter = new DecimalFormat("#,##0." + "0".repeat(tempResult.scale()));
            } else {
                formatter = new DecimalFormat("#,##0");
            }

            return formatter.format(tempResult);
        }
    }

    public String getInputDecimal(BigDecimal targetDecimal) {
        if (targetDecimal.compareTo(new BigDecimal("0")) == 0) {
            return targetDecimal.toPlainString();
        }

        String result = targetDecimal.stripTrailingZeros().toPlainString();

        if (result.contains(".") && !result.endsWith(".")) {
            if (result.length() > 17) {
                result = result.substring(0, 17);
            }
        } else if (result.length() > 16) {
            result = result.substring(0, 16);
        }

        DecimalFormat formatter;
        BigDecimal tempResult = new BigDecimal(result).stripTrailingZeros();

        if (result.contains(".")) {
            formatter = new DecimalFormat("#,##0." + "0".repeat(tempResult.scale()));
        } else {
            formatter = new DecimalFormat("#,##0");
        }

        return formatter.format(tempResult);
    }

    private String getFormattedInputDecimal(String str)
    {
        String inputString = str;
        inputString = inputString.replace(",", "");

        String[] inputStringList = inputString.split("[.]");
        String integerString = inputStringList[0];

        BigDecimal integer = new BigDecimal(integerString);

        DecimalFormat formatter = new DecimalFormat("#,##0");

        inputStringList[0] = formatter.format(integer);

        return String.join("", inputStringList);
    }

    private String getFormattedHistoryDecimal(BigDecimal targetDecimal)
    {
        String decimalToString = targetDecimal.stripTrailingZeros().toPlainString();

        if (targetDecimal.compareTo(new BigDecimal("0.001")) < 0 && targetDecimal.compareTo(new BigDecimal("0")) > 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toPlainString());
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("1")) < 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            String tempString = tempResult.stripTrailingZeros().toPlainString();

                if (tempString.startsWith("-0.")) {
                    if (tempString.length() > 19) {
                        return tempString.substring(0, 19) + "e-" + count;
                    }
                }

                else {
                    if (tempString.length() > 18) {
                        return tempString.substring(0, 18) + "e-" + count;
                    }
                }

                return tempString + "e-" + count;
        }

        else if (targetDecimal.compareTo(new BigDecimal("-0.001")) > 0 && targetDecimal.compareTo(new BigDecimal("0")) < 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toPlainString());
            tempResult = tempResult.stripTrailingZeros();
            int count = 0;

            while (tempResult.compareTo(new BigDecimal("-1")) > 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            String tempString = tempResult.stripTrailingZeros().toPlainString();

            if (tempString.startsWith("0.")) {
                if (tempString.length() > 18) {
                    return tempString.substring(0, 18) + "e-" + count;
                }
            }

            else {
                if (tempString.length() > 17) {
                    return tempString.substring(0, 17) + "e-" + count;
                }
            }

            return tempString + "e-" + count;
        }

        else if(targetDecimal.compareTo(new BigDecimal("9999999999999999")) > 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());

            int count = 0;

            while (tempResult.compareTo(new BigDecimal("10")) > 0) {
                tempResult = tempResult.divide(new BigDecimal("10"), 100, RoundingMode.HALF_UP);
                count += 1;
            }

            if (tempResult.compareTo(new BigDecimal("1")) > 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"), MathContext.UNLIMITED);
                count -= 1;
            }

            if (tempResult.toPlainString().length() > 17) {
                return tempResult.toPlainString().substring(0, 16) + "e+" + count;
            }

            return tempResult.stripTrailingZeros().toPlainString() + "e+" + count;
        }

        else if(targetDecimal.compareTo(new BigDecimal("-9999999999999999")) < 0) {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());

            int count = 0;

            while (tempResult.compareTo(new BigDecimal("-10")) < 0) {
                tempResult = tempResult.divide(new BigDecimal("10"), 100, RoundingMode.HALF_UP);
                count += 1;
            }

            if (tempResult.compareTo(new BigDecimal("-1")) < 0) {
                tempResult = tempResult.multiply(new BigDecimal("10"), MathContext.UNLIMITED);
                count -= 1;
            }

            if (tempResult.toPlainString().length() > 18) {
                return tempResult.toPlainString().substring(0, 17) + "e+" + count;
            }

            return tempResult.stripTrailingZeros().toPlainString() + "e+" + count;
        }

        if(decimalToString.startsWith("-")) {
            if(decimalToString.startsWith("-0.") && decimalToString.length() > 19) {
                return decimalToString.substring(0, 19);
            }

            if(decimalToString.length() > 18) {
                return decimalToString.substring(0, 18);
            }

            return decimalToString;
        }

        else {
            if(decimalToString.startsWith("0.") && decimalToString.length() > 18) {
                return decimalToString.substring(0, 18);
            }

            if(decimalToString.length() > 17) {
                return decimalToString.substring(0, 17);
            }

            return decimalToString;
        }
    }

    public BigDecimal getInputDecimal() {
        return new BigDecimal(String.join("", inputPane.getText().split(","))).stripTrailingZeros();
    }

    public void updateInputPane(String text) {
        inputPane.setText(text);

        updateInputFontSize();
    }

    public void updateHistoryPane(String text) {
        historyPane.setText(text);
    }

    public void updateInputFontSize(){
        Dimension inputPaneSize = inputPane.getSize();

        AffineTransform affinetransform = new AffineTransform();
        FontRenderContext renderContext = new FontRenderContext(affinetransform, true, true);
        Font font = inputPane.getFont();
        int fontSize = font.getSize();

        int textWidth = (int)(font.getStringBounds(inputPane.getText(), renderContext).getWidth());
        int textHeight = (int)(font.getStringBounds(inputPane.getText(), renderContext).getHeight());

        while(textWidth > inputPaneSize.getWidth() && textHeight > inputPaneSize.getHeight())
        {
            fontSize -= 1;
            font = new Font(font.getName(), Font.PLAIN, fontSize);
            textWidth = (int)(font.getStringBounds(inputPane.getText(), renderContext).getWidth());
            textHeight = (int)(font.getStringBounds(inputPane.getText(), renderContext).getHeight());
        }

        while(textWidth < inputPaneSize.getWidth() && textHeight < inputPaneSize.getHeight() && fontSize < 56)
        {
            fontSize += 1;
            font = new Font(font.getName(), Font.PLAIN, fontSize);
            textWidth = (int)(font.getStringBounds(inputPane.getText(), renderContext).getWidth());
            textHeight = (int)(font.getStringBounds(inputPane.getText(), renderContext).getHeight());
        }

        fontSize -= 2;

        SimpleAttributeSet attributeSet = new SimpleAttributeSet();
        StyleConstants.setAlignment(attributeSet, StyleConstants.ALIGN_RIGHT);
        StyleConstants.setFontFamily(attributeSet, "Malgun Gothic");
        StyleConstants.setFontSize(attributeSet, fontSize);
        StyleConstants.setForeground(attributeSet, Color.black);
        inputPane.setParagraphAttributes(attributeSet, true);

    }

    public void addHistory(DataStore calculationData) {
        CalculationLog log = new CalculationLog();

        this.historyList.setOpaque(true);
        this.historyList.setVisible(true);
    }
}
