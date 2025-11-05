using System.Collections.Generic;
using UnityEngine;

public class CustomerPresenter : MonoBehaviour
{
    [Header("Data")]
    public List<CustomerSO> customerList;
    public List<JewelrySO> jewelList;

    [Header("View")]
    public CustomerView view;

    private CustomerModel model;
    private JewelrySO currentJewel;

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
        currentJewel = model.GetRandomJewel();

        view.UpdateView(currentCustomer, currentJewel, model.playerMoney);
        view.SetButtonsEnabled(true);
    }

    private void OnAccept()
    {
        if (model.playerMoney >= currentJewel.GetItemWorth())
        {
            model.playerMoney -= currentJewel.GetItemWorth();
            jewelList.Remove(currentJewel);
            ProceedToNextCustomer();
        }
        else
        {
            Debug.Log("Yeterli paran yok!");
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
            view.customerImage.sprite = null;
            view.jewelImage.sprite = null;
        }
    }
}
