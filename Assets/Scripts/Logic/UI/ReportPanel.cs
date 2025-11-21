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
        UIManager.Instance.HidePanel(Panel.DailyReportPanel);
    }

    public void SetDailyReportFields(int itemBuyed, int itemSelled, float moneySpend, float moneyEarned)
    {
        _moneySpendText.text = $"{moneySpend.ToString()} $";
        _moneyEarnedText.text = $"{moneyEarned.ToString()} $";
        _itemBuyedText.text = $"{itemBuyed}";
        _itemSelledText.text = $"{itemSelled}";
    }
}
