using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomerPresenter : MonoBehaviour
{
    [Header("Data")]
    public List<CustomerSO> customerList;
    public List<JewelrySO> jewelList;

    [Header("View")]
    public CustomerView view;
    private CustomerModel model;
    private JewelrySO currentJewel;

    [Header("Variebles")]
    private int moneySpend;
    private int currentMoral;
    private int maxMoral;

    private void Start()
    {
        model = new CustomerModel(customerList, jewelList);

        ShowCurrentCustomer();
        BindButtons();
    }

    private void BindButtons()
    {
        view.acceptButton.onClick.AddListener(OnAccept);
        view.rejectButton.onClick.AddListener(OnReject);
    }

    private void ShowCurrentCustomer()
    {
        var currentCustomer = model.GetCurrentCustomer();
        maxMoral = currentCustomer.GetMoralBarAmount();
        currentMoral = maxMoral;
        
        currentJewel = model.GetRandomJewel();  
        view.UpdateView(currentCustomer, currentJewel, model.playerMoney, moneySpend,maxMoral,currentMoral);
        
        view.SetButtonsEnabled(true);
    }

    private void OnAccept()
    {

        string offerText = view.GetInput();

        if (int.TryParse(offerText, out int offerPrice))
        {
            if (offerPrice >= currentJewel.GetMinItemWorth())
            {
                if (model.playerMoney >= offerPrice)
                {
                    model.playerMoney -= offerPrice;
                    Debug.Log($"Satın alındı! {currentJewel.GetItemName()} için {offerPrice} ₺ ödendi.");
                    ProceedToNextCustomer();
                }
                else
                {
                    Debug.Log("Yeterli paran yok!");
                }
            }
            else
            {
                Debug.Log($"Teklif çok düşük!");


                currentMoral -= 10;
                view.UpdateMoralBar(currentMoral);

                if(currentMoral == 0)
                {
                    Debug.Log("Müşteri sinirden anlaşmayı bozdu.");
                }

                
               
            }
        }

        else
        {

            ProceedToNextCustomer();
        }

    }

    private void OnReject()
    {
        ProceedToNextCustomer();
    }

    private void ProceedToNextCustomer()
    {
        view.SetButtonsEnabled(false);

        if (model.HasNextCustomer())
        {
            model.NextCustomer();
            ShowCurrentCustomer();
        }
        else
        {
            Debug.Log("Tüm müşteriler geldi!");
            view.customerNameText.text = "Tüm müşteriler geldi!";
            view.dailyReport.SetActive(true);

        }
    }
}
