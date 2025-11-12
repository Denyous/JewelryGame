using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private float customerBalance;
    [SerializeField] private int customerMoralBarAmount;


    public string GetCustomerName()
    {
        return customerName;
    }
    public Sprite GetCustomerSprite()
    {
        return customerSprite;
    }


    public float GetCustomerBalance()
    {
        return customerBalance;
    }
    public void AddCustomerBalance(float amount )
    {
        customerBalance += amount;
    }

    public void RemoveCustomerBalance(float amount )
    {
       
        if (customerBalance > 0)
        {
            customerBalance -= amount;
        }
        else
        {

            customerBalance = 0;
        }
    }

    public int GetMoralBarAmount()
    {
        return customerMoralBarAmount;
    }

}



