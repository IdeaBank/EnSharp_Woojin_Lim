package reducer;

import view.CalculationLog;

import javax.swing.*;
import java.awt.*;

public class CustomListRenderer extends JPanel implements ListCellRenderer<CalculationLog> {
    @Override
    public Component getListCellRendererComponent(JList<? extends CalculationLog> list, CalculationLog country, int index,
                                                  boolean isSelected, boolean cellHasFocus) {
        CalculationLog calculationLog = new CalculationLog();

        calculationLog.getHistoryText().setText("TEST");
        calculationLog.getResultText().setText("TEST");

        add(calculationLog);

        return this;
    }
}