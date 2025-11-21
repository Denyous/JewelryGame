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
        
        UIManager.Instance.HidePanel("StoreUI");
        AudioManager.PlayOneShot(SoundType.CUSTOMER_BELL);
        CustomerManager.Instance.GetNextCustomer();
        Debug.Log("Müşteri Geldi!");
    }

    public void InventoryButtonOnClicked()
    {
        
        UIManager.Instance.ShowPanel("Inventory");
        AudioManager.PlayOneShot(SoundType.OPEN_INVENTORY);
        Debug.Log("Kasa Açıldı");
    }


   
}
