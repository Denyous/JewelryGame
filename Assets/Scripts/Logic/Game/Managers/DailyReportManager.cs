using UnityEngine;

public class DailyReportManager : MonoBehaviour
{
    private static DailyReportManager _instance;
    public ReportPanel DailyReportPanel;
    public static float MoneySpend = 0;
    public static float ReputationGained = 0f;
    public static int ItemBuyed = 0;
    public static int ItemSelled = 0;

    private void Awake()
    {
        _instance = this;
    }
    
    private static void CreateDailyReport()
    {
        _instance.DailyReportPanel.CreateDailyReport();
    }
}
