package store;

import java.math.BigDecimal;

public class DataStore {
    private BigDecimal firstOperand;
    private BigDecimal secondOperand;
    private char operatorChar;
    private boolean isSingleOperand;

    public DataStore()
    {
        this.firstOperand = null;
        this.operatorChar = '\0';
        this.secondOperand = null;
        this.isSingleOperand = false;
    }

    public BigDecimal getFirstOperand() {
        return firstOperand;
    }

    public void setFirstOperand(BigDecimal firstOperand) {
        this.firstOperand = firstOperand;
    }

    public BigDecimal getSecondOperand() {
        return secondOperand;
    }

    public void setSecondOperand(BigDecimal secondOperand) {
        this.secondOperand = secondOperand;
    }

    public char getOperatorChar() {
        return operatorChar;
    }

    public void setOperatorChar(char operatorChar) {
        this.operatorChar = operatorChar;
    }

    public boolean isSingleOperand() {
        return isSingleOperand;
    }

    public void setSingleOperand(boolean singleOperand) {
        isSingleOperand = singleOperand;
    }
}
