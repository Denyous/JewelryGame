using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;



public class CustomerView : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text customerNameText;
    public TMP_Text jewelNameText;
    public TMP_Text jewelPriceText;
    public TMP_Text moneyText;

    public Image customerImage;
    public Image jewelImage;

    public Button acceptButton;
    public Button rejectButton;



    public void UpdateView(CustomerSO customer, JewelrySO jewel, int playerMoney)
    {
        customerImage.sprite = customer.GetCustomerSprite();
        customerNameText.text = customer.GetCustomerName();

        jewelImage.sprite = jewel.GetItemSprite();
        jewelNameText.text = jewel.GetItemName();
        jewelPriceText.text = jewel.GetItemWorth() + " $";

        moneyText.text = "💰 " + playerMoney + " $";
    }

    public void SetButtonsEnabled(bool state)
    {
        acceptButton.enabled = state;
        rejectButton.enabled = state;
    }
}
