using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;



public class CustomerView : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dailyReport;
    public TMP_Text customerNameText;
    public TMP_Text jewelNameText;
    public TMP_Text jewelPriceText;
    public TMP_Text moneyText;
    public TMP_Text moneyspend;
    
    public Image customerImage;
    public Image jewelImage;

    [Header("Buttons")]
    public Button acceptButton;
    public Button rejectButton;

    [Header("Input")]
    public TMP_InputField offerInput;

    [Header("Slider")]
    public Slider moralBar;


    public void UpdateView(CustomerSO customer, JewelrySO jewel, float playerMoney,int spendMoney,int maxValue,int value)
    {
        customerImage.sprite = customer.GetCustomerSprite();
        customerNameText.text = customer.GetCustomerName();
        
        moralBar.maxValue = maxValue;
        moralBar.value = value;
        
        jewelImage.sprite = jewel.GetItemSprite();
        jewelNameText.text = jewel.GetItemName();
        jewelPriceText.text = jewel.GetItemWorth() + " $";
        moneyspend.text = spendMoney.ToString() + "$";

        moneyText.text = ""+ playerMoney + " $";
        offerInput.text = "";
    }
    public void UpdateMoralBar(int currentValue)
    {
        
        moralBar.value = currentValue;
    }

    public void SetButtonsEnabled(bool state)
    {
        acceptButton.enabled = state;
        rejectButton.enabled = state;
    }

    public string GetInput()
    {
        return offerInput.text;
    }
}
