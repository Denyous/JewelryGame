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

    [Header("Customer UI")]
    public TMP_Text nameText;
    public Image customerImage;
    public Slider moraleBar;

    [Header("Jewel UI")]
    public TMP_Text jewelNameText;
    public Image jewelImage;
    public TMP_Text jewelPriceText;



    
    public void UpdateUI(string name, Sprite portrait, string jewelName, Sprite jewelSprite, int jewelPrice)
    {
        nameText.text = name;
        customerImage.sprite = portrait;
        //moraleBar.maxValue = 100;
        //moraleBar.value = morale;

        jewelNameText.text = jewelName;
        jewelImage.sprite = jewelSprite;
        jewelPriceText.text = "$" + jewelPrice;
    }

    //public void UpdateMorale(int morale)
    //{
    //    moraleBar.value = morale;
    //}
}
