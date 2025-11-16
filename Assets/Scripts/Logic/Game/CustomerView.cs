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
    [SerializeField] public TextMeshProUGUI _conversationArea;

    [Header("Customer UI")]
    public TMP_Text nameText;
    public Image customerImage;
    public Slider moraleBar;

    [Header("Jewel UI")]
    public TMP_Text jewelNameText;
    public Image jewelImage;
    public TMP_Text jewelPriceText;

    public event Action OnSpawnNextCustomer;
    public event Action<float> OnBuyButtonClicked;

    public void Initialize()
    {
        RegisterCallbacks();
    }

    public void UpdateUI(CustomerModel currentCustomer,JewelrySO currentJewel,string name, Sprite portrait, int moralBar ,string jewelName, Sprite jewelSprite, int jewelPrice, string moneyText)
    {
        nameText.text = name;
        customerImage.sprite = portrait;
       
        moraleBar.maxValue = 100;
        moraleBar.value = currentCustomer.Morale;

        jewelNameText.text = jewelName;
        jewelImage.sprite = jewelSprite;
        jewelPriceText.text = "$" + jewelPrice;
        _moneyText.text = moneyText + "$";
        
        _offerArea.text = "";
        _buyButton.interactable = false;
    }

    public void DebugCustomerCharacter()
    {
        throw new NotImplementedException();
    }


    public void UpdateMoney(float money)
    {
        _moneyText.text = money.ToString();
    }

    public void UpdateMorale(int morale)
    {
        moraleBar.value = morale;
    }

    public void SetButtonsEnabled(bool state)
    {
        _buyButton.interactable = state;
        _refuseButton.interactable = state;
    }

    public void UpdateConversationArea(string text)
    {
        _conversationArea.text = text;
    }

    public void RegisterCallbacks()
    {
        _buyButton.interactable = false;
        _offerArea.onValueChanged.AddListener(OnOfferChanged);

        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(OnBuyPressed);

        _refuseButton.onClick.RemoveAllListeners();

        _refuseButton.onClick.AddListener(() =>
        {
            Debug.Log("Refuse pressed - yeni müşteri geliyor.");
            OnSpawnNextCustomer?.Invoke();
        });
    }

    private void OnBuyPressed()
    {
        float offerValue = (float)Convert.ToDouble(_offerArea.text);

        OnBuyButtonClicked?.Invoke(offerValue);
    }

    private void OnOfferChanged(string input)
    {
        if (int.TryParse(input, out int value) && value > 0)
        {
            _buyButton.interactable = true;
        }
        else
        {
            _buyButton.interactable = false;
        }
    }
}
