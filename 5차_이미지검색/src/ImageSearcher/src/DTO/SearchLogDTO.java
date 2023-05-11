package DTO;

import java.sql.Date;

public class SearchLogDTO {
    private int logId;
    private String searchQuery;
    private Date searchDate;

    public SearchLogDTO(int logId, String searchQuery, Date searchDate)
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

    public Date GetSearchDate()
    {
        return this.searchDate;
    }

    public void SetSearchDate(Date searchDate)
    {
        this.searchDate = searchDate;
    }
}
