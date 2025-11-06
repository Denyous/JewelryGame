using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerModel : MonoBehaviour
{
    public List<CustomerSO> customers;
    public List<JewelrySO> jewels;

    public int currentCustomerIndex = 0;
    public int playerMoney = 3000;

    public CustomerModel(List<CustomerSO> customers, List<JewelrySO> jewels)
    {
        this.customers = customers;
        this.jewels = jewels;
    }

    public bool HasNextCustomer() => currentCustomerIndex < customers.Count - 1;

    public CustomerSO GetCurrentCustomer() => customers[currentCustomerIndex];

    public JewelrySO GetRandomJewel()
    {
        int r = Random.Range(0, jewels.Count);
        return jewels[r];
    }

    public void NextCustomer()
    {
        if (HasNextCustomer())
            currentCustomerIndex++;
    }
}
