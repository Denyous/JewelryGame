using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;



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

    public Button acceptButton;
    public Button rejectButton;



    public void UpdateView(CustomerSO customer, JewelrySO jewel, int playerMoney,int spendMoney)
    {
        customerImage.sprite = customer.GetCustomerSprite();
        customerNameText.text = customer.GetCustomerName();

        jewelImage.sprite = jewel.GetItemSprite();
        jewelNameText.text = jewel.GetItemName();
        jewelPriceText.text = jewel.GetItemWorth() + " $";
        moneyspend.text = spendMoney.ToString() + "$";

        moneyText.text = "💰 " + playerMoney + " $";
    }

    public void SetButtonsEnabled(bool state)
    {
        acceptButton.enabled = state;
        rejectButton.enabled = state;
    }

    public void OpenDailyReport()
    {

    }
}
