using UnityEngine;

public class DailyReportManager : MonoBehaviour
{
    private static DailyReportManager _instance;
    public ReportPanel DailyReportPanel;
    public static float MoneySpend = 0;
    public static float MoneyEarned = 0;
    public static float ReputationGained = 0f;
    public static int ItemBuyed = 0;
    public static int ItemSelled = 0;

    private void Awake()
    {
        _instance = this;
    }
    
    public static void CreateDailyReport()
    {
        _instance.DailyReportPanel.SetDailyReportFields(ItemBuyed, ItemSelled, MoneySpend, MoneyEarned);
        UIManager.Instance.ShowPanel(Panel.DailyReportPanel);
        AudioManager.PlayOneShot(SoundType.DailyPanel);
        ResetFields();
    }

    private static void ResetFields()
    {
        MoneySpend = 0;
        MoneyEarned = 0;
        ReputationGained = 0f;
        ItemBuyed = 0;
        ItemSelled = 0;
    }
}
