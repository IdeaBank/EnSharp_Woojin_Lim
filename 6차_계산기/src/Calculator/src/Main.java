import controller.CalculatorStart;
import view.CalculatorForm;
import view.HistoryForm;
import view.MainFrame;

import javax.swing.*;
import java.awt.*;
import java.math.BigDecimal;

public class Main {
    public static void main(String[] args){
        CalculatorStart calculatorStart = new CalculatorStart();
        calculatorStart.start();

        BigDecimal test = new BigDecimal("123.");
        System.out.println(test.toString());
    }
}