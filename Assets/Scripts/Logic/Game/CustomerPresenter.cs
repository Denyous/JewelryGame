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

    private List<JewelrySO> GetRandomJewels(int count)
    {
        List<JewelrySO> randomJewels = new List<JewelrySO>();
        List<int> usedIndices = new List<int>();
        while (randomJewels.Count < count && randomJewels.Count < AllJewels.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, AllJewels.Count);
            if (!usedIndices.Contains(randomIndex))
            {
                usedIndices.Add(randomIndex);
                randomJewels.Add(AllJewels[randomIndex]);
            }
        }
        return randomJewels;
    }

    public void SpawnNextCustomer()
    {
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
            DailyReportManager.CreateDailyReport();
            _currentCustomerIndex = 0;
            return;
        }

        _customerModel = CustomerModels[_currentCustomerIndex];
        _customerModel.Initialize(GetRandomJewels(3));
        _customerModel.OnCounterOffer += CounterOfferHandler;
        _customerModel.OnCustomerLeave += CustomerLeaveHandler;
        _customerModel.OnCustomerMoraleChanged += CustomerMoraleChangedHandler;

        _currentCustomerViewObject = Instantiate(CustomerViewPrefab, ParentContainer);
        _customerView = _currentCustomerViewObject.GetComponent<CustomerView>();
        _customerView.Initialize
        (
            _customerModel,
            _customerModel.CurrentJewel,
            MoneyManager.Instance.PlayerMoney.ToString()
        );

        _customerView.OnOfferButtonClicked += OfferButtonClickHandler;
        _customerView.OnSpawnNextCustomer += SpawnNextCustomerHandler;

        _currentCustomerIndex++;
    }

    private void CustomerMoraleChangedHandler(double morale)
    {
        _customerView.UpdateMoraleProgressBar(morale);
    }

    // Yeni müşteri çağırma sadece müşteri getir butonuna tıklandığında olacak.
    private void CustomerLeaveHandler(CustomerMood mood, float offerValue, bool isSeller)
    {
        if (isSeller)
        {
            if (mood == CustomerMood.Happy)
            {
                MoneyManager.Instance.RemoveMoney(offerValue);
                InventoryManager.Instance.AddItemToInventory(_customerModel.CurrentJewel);
                ReputationManager.Instance.AddReputation(30);
                _customerView.UpdateMoneyText(MoneyManager.Instance.PlayerMoney);
                Debug.Log("Tebrikler ürünü satın aldınız.");
                _customerModel.AddMoney(offerValue);
                _customerView.ReturnStorePanel();

                ResetCustomer();

                DailyReportManager.ItemBuyed += 1;
                return;
            }
            else if (mood == CustomerMood.Mad)
            {
                Debug.Log("Customer Sinirli Bir Sekilde Dukkani Terk Etti.");
                _customerView.ReturnStorePanel();

                ResetCustomer();
                return;
            }
            return;
        }

        if (mood == CustomerMood.Happy)
        {
            MoneyManager.Instance.AddMoney(offerValue);
            InventoryManager.Instance.RemoveItemFromInventory(_customerModel.CurrentJewel);
            ReputationManager.Instance.AddReputation(30);
            _customerView.UpdateMoneyText(MoneyManager.Instance.PlayerMoney);
            Debug.Log("Tebrikler ürünü sattiniz.");
            _customerModel.RemoveMoney(offerValue);
            _customerView.ReturnStorePanel();
            ResetCustomer();
            DailyReportManager.ItemSelled += 1;
        }
        else if (mood == CustomerMood.Mad)
        {
            Debug.Log("Customer Sinirli Bir Sekilde Dukkani Terk Etti.");
            _customerView.ReturnStorePanel();
            ResetCustomer();
        }
    }

    //In Customer Leave Handler call this method after the methods of _customerView and _customerModel
    private void ResetCustomer()
    {
        _customerModel.Reset();
        _customerModel.OnCounterOffer -= CounterOfferHandler;
        _customerModel.OnCustomerLeave -= CustomerLeaveHandler;
        _customerModel.OnCustomerMoraleChanged -= CustomerMoraleChangedHandler;

        _customerView.OnOfferButtonClicked -= OfferButtonClickHandler;
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

    private void OfferButtonClickHandler(float offerValue)
    {
        if (_customerModel.IsSeller)
        {

        }
        else
        {

        }

        _customerModel.HandleOffer(offerValue, _customerModel.ExpectedItemPrice, _customerModel.CurrentJewel.GetItemWorth(), _customerModel.CurrentJewel.GetMinItemWorth(), _customerModel.CurrentJewel.GetMaxItemWorth());
    }

    public void UpdateMoneyView(float money)
    {
        _customerView.UpdateMoneyText(money);
    }
}
