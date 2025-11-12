using UnityEngine;

[RequireComponent(typeof(CustomerView))]
public class CustomerModel : MonoBehaviour
{

    public CustomerSO customerData;
    public JewelrySO jewelData;
    public CustomerView view;
    public CustomerPresenter presenter;
    private string moneyText;

    private void Awake()
    {
        view = GetComponent<CustomerView>();
        moneyText = MoneyManager.Instance.PlayerMoney.ToString() + "$";
        ButtonListenerAdd();
    }

    public void Initialize(CustomerPresenter _presenter)
    {
        presenter = _presenter;
    }

    public void SetData(CustomerSO customer, JewelrySO jewel)
    {
        customerData = customer;
        jewelData = jewel;
        UpdateView();
    }

    private void UpdateView()
    {
        Debug.Log("View has been updated");

        view.UpdateUI(customerData.GetCustomerName(), customerData.GetCustomerSprite(), jewelData.GetItemName(), jewelData.GetItemSprite(), jewelData.GetItemWorth(), moneyText);

        if (view == null || jewelData == null || customerData == null)
        {
            Debug.Log("data ulaşmıyor.");
        }

    }


    public void SetMorale(int newMorale)
    {
        //if (customerData == null) return;
        //customerData.g = newMorale;
        //view.UpdateMorale(newMorale);
    }

    public void ButtonListenerAdd()
    {
        // Buton listenerlarını CustomerManager içinde bağla
        view._buyButton.onClick.RemoveAllListeners();
        view._buyButton.onClick.AddListener(OnBuyPressed);

        view._refuseButton.onClick.RemoveAllListeners();
        view._refuseButton.onClick.AddListener(OnRefusePressed);

    }
    private void OnBuyPressed()
    {
        // Money Manager, Reputation Manager ve Inventory Manager Buraya gelicek.
        Debug.Log("Buy pressed - yeni müşteri geliyor.");
        presenter.SpawnNextCustomer();
    }

    private void OnRefusePressed()
    {
        // Şu an sadece diğer müşteriyi getir
        Debug.Log("Refuse pressed - yeni müşteri geliyor.");
        presenter.SpawnNextCustomer();
    }

}
