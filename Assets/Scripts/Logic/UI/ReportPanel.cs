using TMPro;
using UnityEngine;

public class ReportPanel : UIPanel
{
    [Header("UI References")]

    [SerializeField] private TextMeshProUGUI _reputationGainedText;
    [SerializeField] private TextMeshProUGUI _itemBuyedText;
    [SerializeField] private TextMeshProUGUI _itemSelledText;
    [SerializeField] private TextMeshProUGUI _moneySpendText;
    [SerializeField] private TextMeshProUGUI _moneyEarnedText;
    [SerializeField] private TextMeshProUGUI _profitText;

    public void NextButtonOnClicked()
    {
        UIManager.Instance.ShowPanel(Panel.StoreUI);
        UIManager.Instance.HidePanel(Panel.CustomerContainer);
    }

    public void CreateDailyReport()
    {

    }
}
