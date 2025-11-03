using TMPro;
using UnityEngine;

public class CustomerModel : MonoBehaviour
{
    [Header("Customer Variables")]

    [SerializeField] public CustomerSO[] customers;
    private SpriteRenderer customerSprite;
    private TextMeshPro customerName;
    [SerializeField] private int current;



    void Start()
    {
        customerName = GetComponentInChildren<TextMeshPro>();
        customerSprite = GetComponentInChildren<SpriteRenderer>();

    }

    private void Update()
    {
        CustomerPlacement(current);
    }

    private void CustomerPlacement(int index)
    {
        customerName.text = customers[index].GetCustomerName();
        customerSprite.sprite = customers[index].GetCustomerSprite();
    }

}
