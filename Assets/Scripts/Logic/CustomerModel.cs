using UnityEngine;

[RequireComponent(typeof(CustomerView))]
public class CustomerModel : MonoBehaviour
{

    public CustomerSO customerData;
    public JewelrySO jewelData;
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
        customerData = customer;
        jewelData = jewel;
        UpdateView();
    }

    private void UpdateView()
    {
        Debug.Log("View has been updated");

        view.UpdateUI(customerData.GetCustomerName(), customerData.GetCustomerSprite(), customerData.GetMoralBarAmount(),jewelData.GetItemName(), jewelData.GetItemSprite(), jewelData.GetItemWorth(),MoneyManager.Instance.PlayerMoney.ToString());

        if (view == null || jewelData == null || customerData == null)
        {
            Debug.Log("data ulaşmıyor.");
        }

        SetMorale(50);

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
       
        if (MoneyManager.Instance.PlayerMoney >= jewelData.GetItemWorth())
        {
            MoneyManager.Instance.RemoveMoney(jewelData.GetItemWorth());
            UpdateMoneyView(MoneyManager.Instance.PlayerMoney);
            presenter.SpawnNextCustomer();

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
    public void SetMorale(int newMorale)
    {
        // İndirmek istediğimiz morali burdan indirip sonrasında viewda güncelleyeceğiz.
        view.UpdateMorale(newMorale);
    }
}
