using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private Sprite customerSprite;

    public string GetCustomerName()
    {
        return customerName;
    }
    public Sprite GetCustomerSprite()
    {
        return customerSprite;
    }

}



