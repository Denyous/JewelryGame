using UnityEngine;

[RequireComponent(typeof(CustomerView))]
public class CustomerModel : MonoBehaviour
{

    public CustomerSO currentCustomerData;
    public JewelrySO currentJewelData;
    public CustomerView view;
    public CustomerPresenter presenter;
    

    private void Awake()
    {
        view = GetComponent<CustomerView>();
        
        ButtonListenerAdd();
    }

    public void Initialize(CustomerPresenter _presenter)
    {
        presenter = _presenter;
    }

    public void SetData(CustomerSO customer, JewelrySO jewel)
    {
        currentCustomerData = customer;
        currentJewelData = jewel;
        UpdateView();
    }

    private void UpdateView()
    {
        Debug.Log("View has been updated");

        view.UpdateUI(currentCustomerData.GetCustomerName(), currentCustomerData.GetCustomerSprite(), currentCustomerData.GetMoralBarAmount(),currentJewelData.GetItemName(), currentJewelData.GetItemSprite(), currentJewelData.GetItemWorth(),MoneyManager.Instance.PlayerMoney.ToString());

        if (view == null || currentJewelData == null || currentCustomerData == null)
        {
            Debug.Log("data ulaşmıyor.");
        }

        

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
        Debug.Log("Buy pressed - yeni müşteri geliyor.");

        if (MoneyManager.Instance.PlayerMoney >= currentJewelData.GetItemWorth())
        {
            
            MoneyManager.Instance.RemoveMoney(currentJewelData.GetItemWorth());
            UpdateMoneyView(MoneyManager.Instance.PlayerMoney);
            InventoryManager.Instance.AddItemToInventory(currentJewelData);
            presenter.SpawnNextCustomer();

            //Öncesinde Offer Mekaniği ve Moral mekaniğini yapacağım. Bu mekanizmadan sonra

            // Burası tam event sistemini oluşturmalık yer. Inventory Manager, MoneyManager,Reputation Managerların Merkezi.

        }
        else
        {
            Debug.Log("PARAN YOK!");
        }

    }

    private void OnRefusePressed()
    {
        // Şu an sadece diğer müşteriyi getir
        Debug.Log("Refuse pressed - yeni müşteri geliyor.");
        presenter.SpawnNextCustomer();
    }

    public void UpdateMoneyView(float money)
    {
        view.UpdateMoney(money);
    }
    public void RemoveMoral(int newMorale)
    {
        // Ne kadar moral indirmek istiyorsak burası aracılığıyla indireceğiz.
        view.UpdateMorale(newMorale);
    }
}
