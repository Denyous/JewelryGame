using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private int customerMoralBarAmount;

    public string GetCustomerName()
    {
        return customerName;
    }
    public Sprite GetCustomerSprite()
    {
        return customerSprite;
    }

    public int GetMoralBarAmount()
    {
        return customerMoralBarAmount;
    }

}



