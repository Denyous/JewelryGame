using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

[RequireComponent(typeof(CustomerView))]
public class CustomerModel : MonoBehaviour
{

    public CustomerSO currentCustomerData;
    public JewelrySO currentJewelData;
    public CustomerView view;
    public CustomerPresenter presenter;
    private double offerValue;


    private void Awake()
    {
        view = GetComponent<CustomerView>();
        ViewListeners();
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
        //Debug.Log("View has been updated");

        view.UpdateUI(currentCustomerData, currentJewelData, currentCustomerData.GetCustomerName(), currentCustomerData.GetCustomerSprite(), currentCustomerData.GetMoralBarAmount(), currentJewelData.GetItemName(), currentJewelData.GetItemSprite(), currentJewelData.GetItemWorth(), MoneyManager.Instance.PlayerMoney.ToString());

        if (view == null || currentJewelData == null || currentCustomerData == null)
        {
            Debug.Log("data ulaşmıyor.");
        }



    }

    public void ViewListeners()
    {
        // Viewi dinleyen button ve offer listenerlar. //Eventler.
        view._buyButton.interactable = false;
        view._offerArea.onValueChanged.AddListener(OnOfferChanged);

        view._buyButton.onClick.RemoveAllListeners();
        view._buyButton.onClick.AddListener(OnBuyPressed);

        view._refuseButton.onClick.RemoveAllListeners();
        view._refuseButton.onClick.AddListener(OnRefusePressed);
    }

    private void OnOfferChanged(string input)
    {
        // Eğer input boşsa ya da geçerli bir sayı değilse buton kilitli
        if (int.TryParse(input, out int value) && value > 0)
        {
            view._buyButton.interactable = true;
            //Debug.Log("Buton aktifleşti");
        }
        else
        {
            view._buyButton.interactable = false;
        }
    }

    private void OnBuyPressed()
    {
        float offerValue = GetOfferInput();

        if (offerValue <= MoneyManager.Instance.PlayerMoney)
        {

            if (offerValue >= currentJewelData.GetMinItemWorth())
            {
                MoneyManager.Instance.RemoveMoney(offerValue); 
                InventoryManager.Instance.AddItemToInventory(currentJewelData);
                ReputationManager.Instance.AddReputation(30);
                view.UpdateMoney(MoneyManager.Instance.PlayerMoney);
                Debug.Log("Tebrikler ürünü satın aldınız.");
                presenter.SpawnNextCustomer();
            }
            else
            {
                RemoveMoral(25);

                if (view.moraleBar.value <= 0)
                {
                    Debug.Log("Müşteri Sinirden Anlaşmayı Bozdu");

                    ReputationManager.Instance.AddReputation(-10);

                    presenter.SpawnNextCustomer();
                }
            }

        }
        else
        {
            Debug.Log("Bakiyenden yüksek teklif giremezsin!");

           
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

    public float GetOfferInput()
    {
        float offerValue = (float)Convert.ToDecimal(view._offerArea.text);
        return offerValue;
    }

}
