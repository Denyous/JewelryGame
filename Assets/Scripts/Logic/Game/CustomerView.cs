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
    [SerializeField] private Button _buyButton;
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
    public event Action<float> OnBuyButtonClicked;
    private string debugChar;
    private string debugMorale;

    public void Initialize()
    {
        RegisterCallbacks();
    }

    public void UpdateUI(CustomerModel currentCustomer,JewelrySO currentJewel,string name, Sprite portrait, int moralBar ,string jewelName, Sprite jewelSprite, int jewelPrice, string moneyText)
    {
        _nameText.text = name;
        _customerImage.sprite = portrait;

        _moraleBar.progressValue = (float)Helpers.ClampProgressBarValue(currentCustomer.Morale);

        _jewelNameText.text = jewelName;
        _jewelImage.sprite = jewelSprite;
        _jewelPriceText.text = "$" + jewelPrice;
        _moneyText.text = moneyText + "$";
        
        _offerArea.text = "";
        _buyButton.interactable = false;

        DebugCustomerCharacter(currentCustomer.Data.CustomerCharacter.ToString(), currentCustomer.Mood.ToString());
        DebugItemWorth(currentCustomer.CurrentJewelData.GetMinItemWorth().ToString(), currentCustomer.CurrentJewelData.GetMaxItemWorth().ToString());
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

    public void UpdateMoney(float money)
    {
        _moneyText.text = money.ToString();
    }

    public void UpdateMorale(double morale)
    {
        _moraleBar.progressValue = (float)morale;
        DebugCustomerCharacter(debugChar, morale.ToString());
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
                     
            ReturnStorePanel();
           
        });
    }

    
    public void ReturnStorePanel()
    {
        //Bu klas müşteriyle etkileşime girdikten sonra oyuncuyu store yönlendirir böylece envanterini düzeltebilir vs...
        //Kontrolü oyuncuya vermiş oluruz.
        UIManager.Instance.ShowPanel(Panel.StoreUI);
        UIManager.Instance.HidePanel(Panel.CustomerContainer);
        Debug.Log("Müşteri Gitti!");
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
