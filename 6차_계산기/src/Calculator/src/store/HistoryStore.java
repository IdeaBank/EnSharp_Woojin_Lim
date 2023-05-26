package store;

import java.util.ArrayList;

public class HistoryStore {
    private ArrayList<DataStore> calculationHistory;

    public HistoryStore()
    {
        calculationHistory = new ArrayList<>();
    }

    public void AddHistory(DataStore dataStore)
    {
        this.calculationHistory.add(dataStore);
    }

    public ArrayList<DataStore> getCalculationHistory()
    {
        return calculationHistory;
    }
}
