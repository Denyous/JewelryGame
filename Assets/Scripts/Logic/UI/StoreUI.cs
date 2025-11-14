using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIPanel
{
    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Slider _reputationBar;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _moneyText.text = MoneyManager.Instance.PlayerMoney.ToString() + "$";
        _reputationBar.value = ReputationManager.Instance._reputationPoints;
    }

    public void OnStoreOpenClicked()
    {
        UIManager.Instance.HidePanel("StoreUI");
        CustomerManager.Instance.GetNextCustomer();
    }
}
