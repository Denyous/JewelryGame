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
    [SerializeField] private Button _offerButton;
    [SerializeField] private Button _refuseButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TMP_InputField _offerArea;
    [SerializeField] private TextMeshProUGUI _conversationArea;

    [Header("Customer UI")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _customerImage;
    [SerializeField] private TMP_Text _debugCustomerCharacterText;
    [SerializeField] private TMP_Text _debugJewelMinWorthText;
    [SerializeField] private TMP_Text _debugJewelMaxWorthText;
    [SerializeField] private ProgressBar _moraleBar;

    [Header("Jewel UI")]
    [SerializeField] private TMP_Text _jewelNameText;
    [SerializeField] private Image _jewelImage;
    [SerializeField] private TMP_Text _jewelPriceText;

    public event Action OnSpawnNextCustomer;
    public event Action<float> OnOfferButtonClicked;
    private string debugChar;
    private string debugMorale;

    public void Initialize(CustomerModel currentCustomer, JewelrySO currentJewel, string moneyText)
    {
        RegisterCallbacks();

        UpdateCustomerUI(currentCustomer);
        UpdateMoraleProgressBar((float)Helpers.ClampProgressBarValue(currentCustomer.Morale));
        UpdateJewelUI(currentJewel);
        UpdateMoneyText(MoneyManager.Instance.PlayerMoney);
        _offerArea.text = "";
        _offerButton.interactable = false;
        DebugCustomerCharacter(currentCustomer.Data.CustomerCharacter.ToString(), currentCustomer.Mood.ToString());
        DebugItemWorth(currentCustomer.CurrentJewel.GetMinItemWorth().ToString(), currentCustomer.CurrentJewel.GetMaxItemWorth().ToString());

        if (!currentCustomer.IsSeller)
        {
            _offerButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Counter Offer";

            UpdateConversationArea($"I'm looking to buy {currentJewel.GetItemName()}, sell it to me for {currentJewel.GetMinItemWorth()}?");
        }
    }

    public void DebugCustomerCharacter(string character, string morale)
    {
        debugChar = character;
        debugMorale = morale;
        _debugCustomerCharacterText.text = $"Char: {character}\nMorale: {morale}";
    }

    public void DebugItemWorth(string itemMinWorth, string itemMaxWorth)
    {
        _debugJewelMinWorthText.text = $"Min: {itemMinWorth}";
        _debugJewelMaxWorthText.text = $"Max: {itemMaxWorth}";
    }

    public void UpdateCustomerUI(CustomerModel currentCustomer)
    {
        _nameText.text = currentCustomer.Data.CustomerName;
        _customerImage.sprite = currentCustomer.Data.Sprite;
    }

    public void UpdateMoneyText(float money)
    {
        _moneyText.text = money.ToString();
    }

    public void UpdateMoraleProgressBar(double morale)
    {
        _moraleBar.progressValue = (float)morale;
        DebugCustomerCharacter(debugChar, morale.ToString());
    }

    public void UpdateJewelUI(JewelrySO currentJewel)
    {
        _jewelNameText.text = currentJewel.GetItemName();
        _jewelImage.sprite = currentJewel.GetItemSprite();
        _jewelPriceText.text = "$" + currentJewel.GetItemWorth();
    }

    public void SetButtonsEnabled(bool state)
    {
        _offerButton.interactable = state;
        _refuseButton.interactable = state;
    }

    public void UpdateConversationArea(string text)
    {
        _conversationArea.text = text;
    }

    public void RegisterCallbacks()
    {
        _offerButton.interactable = false;
        _offerArea.onValueChanged.AddListener(OnOfferChanged);

        _offerButton.onClick.RemoveAllListeners();
        _offerButton.onClick.AddListener(OnOfferButtonPressed);

        _refuseButton.onClick.RemoveAllListeners();

        _refuseButton.onClick.AddListener(() =>
        {
                     
            ReturnStorePanel();
           
        });
    }

    //Bu klas müşteriyle etkileşime girdikten sonra oyuncuyu store yönlendirir böylece envanterini düzeltebilir vs...
    //Kontrolü oyuncuya vermiş oluruz.
    public void ReturnStorePanel()
    {
        UIManager.Instance.ShowPanel(Panel.StoreUI);
        UIManager.Instance.HidePanel(Panel.CustomerContainer);
        Debug.Log("Müşteri Gitti!");
    }

    private void OnOfferButtonPressed()
    {
        float offerValue = (float)Convert.ToDouble(_offerArea.text);

        OnOfferButtonClicked?.Invoke(offerValue);
    }

    private void OnOfferChanged(string input)
    {
        if (int.TryParse(input, out int value) && value > 0)
        {
            _offerButton.interactable = true;
        }
        else
        {
            _offerButton.interactable = false;
        }
    }
}
