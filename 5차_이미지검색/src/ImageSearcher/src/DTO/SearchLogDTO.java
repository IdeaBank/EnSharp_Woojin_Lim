package DTO;

import java.sql.Date;
import java.sql.Time;

public class SearchLogDTO {
    private int logId;
    private String searchQuery;
    private Time searchDate;

    public SearchLogDTO(int logId, String searchQuery, Time searchDate)
    {
        this.logId = logId;
        this.searchQuery = searchQuery;
        this.searchDate = searchDate;
    }

    public int GetLogId()
    {
        return this.logId;
    }

    public void SetLogId(int logId)
    {
        this.logId = logId;
    }

    public String GetSearchQuery()
    {
        return this.searchQuery;
    }

    public void SetSearchQuery(String searchQuery)
    {
        this.searchQuery = searchQuery;
    }

    public Time GetSearchDate()
    {
        return this.searchDate;
    }

    public void SetSearchDate(Time searchDate)
    {
        this.searchDate = searchDate;
    }
}
