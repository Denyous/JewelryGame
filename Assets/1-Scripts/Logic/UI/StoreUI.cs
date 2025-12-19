using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class StoreUI : UIPanel
{
    [Header("UI Variables")]
    [SerializeField] private Button _openStoreButton;
    [SerializeField] private Slider _reputationBar;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Button _inventoryButton;

    [Header("Drobes & ShowCase Variables")]

    [SerializeField] public Image _rightDrobe;
    [SerializeField] public Image _leftDrobe;
    [SerializeField] public Image _showCase;

    [SerializeField] public List<Sprite> _drobesSprites;
    [SerializeField] public List<Sprite> _showCaseSprites;

    private void OnEnable()
    {
        CustomerPresenter.OnItemBuyandSell += DrobesSpritesChange;
    }

    private void OnDisable()
    {
        CustomerPresenter.OnItemBuyandSell -= DrobesSpritesChange;
    }
    private void Start()
    {
        _moneyText.text = MoneyManager.Instance.PlayerMoney.ToString() + "$";
        DrobesSpritesChange();
    }

    private void Update()
    {
        _moneyText.text = MoneyManager.Instance.PlayerMoney.ToString() + "$";
    }

    public void GetNextCustomerOnClicked()
    {
        Debug.Log("Müşteri Geldi!");
        CustomerManager.Instance.GetNextCustomer();
        AudioManager.PlayOneShot(SoundType.CustomerBell);
        AudioManager.PlayOneShot(SoundType.Click);
        AudioManager.PlayOneShot(SoundType.OpenInventory);
        UIManager.Instance.ShowPanel("CustomerStoreView");
    }



    public void InventoryButtonOnClicked()
    {
        Debug.Log("Kasa Açıldı");
        UIManager.Instance.ShowPanel(Panel.InventoryPanel);
        AudioManager.PlayOneShot(SoundType.OpenInventory);
        AudioManager.PlayOneShot(SoundType.Click);
    }


    public void DrobesSpritesChange()
    {
        Debug.Log("Event Tetiklendi");

        if (InventoryManager.Instance.GetJewelCount() == 0)
        {
            Debug.Log("Drobes ve Vitrin Sprite Değişmedi % 0");
            _rightDrobe.sprite = _drobesSprites[0];
            _leftDrobe.sprite = _drobesSprites[0];
            _showCase.sprite = _showCaseSprites[0];
        }

        if (InventoryManager.Instance.GetJewelCount() == 3)
        {
            Debug.Log("Drobes ve Vitrin Sprite Değişti %50");
            _rightDrobe.sprite = _drobesSprites[1];
            _leftDrobe.sprite = _drobesSprites[1];
            _showCase.sprite = _showCaseSprites[1];
        }

        if (InventoryManager.Instance.GetJewelCount() == 6)
        {
            Debug.Log("Drobes ve Vitrin Sprite Değişti %100");
            _rightDrobe.sprite = _drobesSprites[2];
            _leftDrobe.sprite = _drobesSprites[2];
            _showCase.sprite = _showCaseSprites[2];
        }
    }
}
