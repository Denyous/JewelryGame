using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CustomerPresenter : MonoBehaviour
{
    
    [Header("Prefab ve Veri Referansları")]
    public GameObject customerTemplatePrefab;
    public Transform parentContainer;

    [Header("Veri Listeleri")]
    public List<CustomerSO> allCustomers;
    public List<JewelrySO> allJewels;

    private GameObject currentCustomerInstance;
    private int currentCustomerIndex = -1;



    public bool SpawnNextCustomer()
    {
        //ButtonListenerAdd();
        // Önce mevcut müşteriyi sil
        if (currentCustomerInstance != null)
        {
            Destroy(currentCustomerInstance);
        }

        currentCustomerIndex++;

        if (currentCustomerIndex >= allCustomers.Count)
        {
            return false; // müşteri listesi bitti
        }

        currentCustomerInstance = Instantiate(customerTemplatePrefab, parentContainer);

        var model = currentCustomerInstance.GetComponent<CustomerModel>();
        
        model.Initialize(this);
        
        if (model != null)
        {
            // Random jewel seç
            JewelrySO randomJewel = allJewels[Random.Range(0, allJewels.Count)];
            model.SetData(allCustomers[currentCustomerIndex], randomJewel);
        }

        return true;
    }


    //public void ButtonListenerAdd()
    //{
    //    // Buton listenerlarını CustomerManager içinde bağla
    //    buyButton.onClick.RemoveAllListeners();
    //    buyButton.onClick.AddListener(OnBuyPressed);

    //    refuseButton.onClick.RemoveAllListeners();
    //    refuseButton.onClick.AddListener(OnRefusePressed);

    //}
    //private void OnBuyPressed()
    //{
    //    // Şu an sadece diğer müşteriyi getir
    //    Debug.Log("Buy pressed - yeni müşteri geliyor.");
    //    SpawnNextCustomer();
    //}

    //private void OnRefusePressed()
    //{
    //    // Şu an sadece diğer müşteriyi getir
    //    Debug.Log("Refuse pressed - yeni müşteri geliyor.");
    //    SpawnNextCustomer();
    //}
}
