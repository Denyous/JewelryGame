using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReportPanel : UIPanel
{
    [Header("UI References")]

    [SerializeField] private Button _nextDayButton;
    [SerializeField] private TextMeshPro _moneySpend;

    public void NextButtonOnClicked()
    {
        UIManager.Instance.ShowPanel("StoreUI");
        UIManager.Instance.HidePanel("DailyReportPanel");
    }
}
