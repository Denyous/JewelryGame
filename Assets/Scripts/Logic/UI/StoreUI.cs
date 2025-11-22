using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIPanel
{
    [Header("UI Variables")]
    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Slider _reputationBar;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Button _inventoryButton;

    private void Start()
    {
        _moneyText.text = MoneyManager.Instance.PlayerMoney.ToString() + "$";
        _reputationBar.value = ReputationManager.Instance._reputationPoints;
    }

    private void Update()
    {
        _moneyText.text = MoneyManager.Instance.PlayerMoney.ToString() + "$";
        _reputationBar.value = ReputationManager.Instance._reputationPoints;
    }

    public void GetNextCustomerOnClicked()
    {
        Debug.Log("Müşteri Geldi!");
        UIManager.Instance.HidePanel(Panel.StoreUI);
        CustomerManager.Instance.GetNextCustomer();
        AudioManager.PlayOneShot(SoundType.CustomerBell);
        AudioManager.PlayOneShot(SoundType.Click);
        AudioManager.PlayOneShot(SoundType.OpenInventory);
    }

    public void InventoryButtonOnClicked()
    {
        Debug.Log("Kasa Açıldı");
        UIManager.Instance.ShowPanel(Panel.InventoryPanel);
        AudioManager.PlayOneShot(SoundType.OpenInventory);
        AudioManager.PlayOneShot(SoundType.Click);
    }
}
