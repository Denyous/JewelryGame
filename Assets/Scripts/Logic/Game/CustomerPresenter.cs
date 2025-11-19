using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerPresenter : MonoBehaviour
{
    [Header("Prefab ve Veri Referansları")]
    public GameObject CustomerViewPrefab;
    public Transform ParentContainer;

    public List<JewelrySO> AllJewels;

    public List<CustomerModel> CustomerModels;

    private CustomerModel _customerModel;
    private CustomerView _customerView;

    private GameObject _currentCustomerViewObject;
    private int _currentCustomerIndex = 0;

    private JewelrySO GetRandomJewel()
    {
        return AllJewels[UnityEngine.Random.Range(0, AllJewels.Count)];
    }

    public void SpawnNextCustomer()
    {
        // Önce mevcut müşteriyi sil
        if (_currentCustomerViewObject != null)
        {
            Destroy(_currentCustomerViewObject);
        }

        if (_customerModel != null)
        {
            _customerModel = null;
        }

        if (_currentCustomerIndex > CustomerModels.Count - 1)
        {
            UIManager.Instance.ShowPanel(Panel.DailyReportPanel);
            _currentCustomerIndex = 0;
            return;
        }

        _customerModel = CustomerModels[_currentCustomerIndex];
        _customerModel.Initialize(GetRandomJewel());
        _customerModel.OnCounterOffer += CounterOfferHandler;
        _customerModel.OnCustomerLeave += CustomerLeaveHandler;
        _customerModel.OnCustomerMoraleChanged += CustomerMoraleChangedHandler;

        _currentCustomerViewObject = Instantiate(CustomerViewPrefab, ParentContainer);
        _customerView = _currentCustomerViewObject.GetComponent<CustomerView>();
        _customerView.Initialize();
        _customerView.OnBuyButtonClicked += BuyButtonClickHandler;
        _customerView.OnSpawnNextCustomer += SpawnNextCustomerHandler;

        _customerView.UpdateUI
        (
            _customerModel,
            _customerModel.CurrentJewelData,
            _customerModel.Data.CustomerName,
            _customerModel.Data.Sprite,
            _customerModel.Morale,
            _customerModel.CurrentJewelData.GetItemName(),
            _customerModel.CurrentJewelData.GetItemSprite(),
            _customerModel.CurrentJewelData.GetItemWorth(),
            MoneyManager.Instance.PlayerMoney.ToString()
        );

        _currentCustomerIndex++;
    }

    private void CustomerMoraleChangedHandler(double morale)
    {
        _customerView.UpdateMorale(morale);
    }

    private void CustomerLeaveHandler(CustomerMood mood, float offerValue)
    {
       // Yeni müşteri çağırma sadece müşteri getir butonuna tıklandığında olacak.

        if (mood == CustomerMood.Happy)
        {
            MoneyManager.Instance.RemoveMoney(offerValue);
            InventoryManager.Instance.AddItemToInventory(_customerModel.CurrentJewelData);
            ReputationManager.Instance.AddReputation(30);
            _customerView.UpdateMoney(MoneyManager.Instance.PlayerMoney);
            Debug.Log("Tebrikler ürünü satın aldınız.");
            _customerModel.AddMoney(offerValue);

           // ResetCustomer();   
           //SpawnNextCustomer();

            _customerView.ReturnStorePanel();
            DailyReportManager.ItemBuyed += 1;
        }
        else if (mood == CustomerMood.Mad)
        {
            Debug.Log("Customer Sinirli Bir Sekilde Dukkani Terk Etti.");

           // ResetCustomer();
           //SpawnNextCustomer();

            _customerView.ReturnStorePanel();
        }
    }

    private void ResetCustomer()
    {
        _customerModel.Reset();
        _customerModel.OnCounterOffer -= CounterOfferHandler;
        _customerModel.OnCustomerLeave -= CustomerLeaveHandler;
        _customerModel.OnCustomerMoraleChanged -= CustomerMoraleChangedHandler;

        _customerView.OnBuyButtonClicked -= BuyButtonClickHandler;
        _customerView.OnSpawnNextCustomer -= SpawnNextCustomerHandler;

        Destroy(_currentCustomerViewObject);
        _customerView = null;
        _customerModel = null;
    }

    private void CounterOfferHandler(float counterOffer)
    {
        _customerView.UpdateConversationArea($"How about {counterOffer}?");
    }

    private void SpawnNextCustomerHandler()
    {
        SpawnNextCustomer();
    }

    private void BuyButtonClickHandler(float offerValue)
    {
        _customerModel.HandleOffer(offerValue, _customerModel.ExpectedItemPrice, _customerModel.CurrentJewelData.GetItemWorth(), _customerModel.CurrentJewelData.GetMinItemWorth());
    }

    public void UpdateMoneyView(float money)
    {
        _customerView.UpdateMoney(money);
    }
}
