using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class CustomerView : MonoBehaviour
{
    [Header("GeneralUI")]
    [SerializeField] public Button _buyButton;
    [SerializeField] public Button _refuseButton;
    [SerializeField] public TextMeshProUGUI _moneyText;
    [SerializeField] public TMP_InputField _offerArea;


    [Header("Customer UI")]
    public TMP_Text nameText;
    public Image customerImage;
    public Slider moraleBar;


    [Header("Jewel UI")]
    public TMP_Text jewelNameText;
    public Image jewelImage;
    public TMP_Text jewelPriceText;


    public void UpdateUI(CustomerSO currentCustomer,JewelrySO currentJewel,string name, Sprite portrait, int moralBar ,string jewelName, Sprite jewelSprite, int jewelPrice, string moneyText)
    {
        
        nameText.text = name;
        customerImage.sprite = portrait;
       
        //Moral barına verilan max value müşterinin kendi moral valuesi.
        moraleBar.maxValue = currentCustomer.GetMoralBarAmount();
        moraleBar.value = currentCustomer.GetMoralBarAmount();

        jewelNameText.text = jewelName;
        jewelImage.sprite = jewelSprite;
        jewelPriceText.text = "$" + jewelPrice;
        _moneyText.text = moneyText + "$";
        
        _offerArea.text = "";
        _buyButton.interactable = false;

    }

    public void UpdateMoney(float money)
    {
        _moneyText.text = money.ToString();
    }

    public void UpdateMorale(int morale)
    {
        moraleBar.value -= morale;
    }

    public void SetButtonsEnabled(bool state)
    {
        _buyButton.interactable = state;
        _refuseButton.interactable = state;
    }
}
