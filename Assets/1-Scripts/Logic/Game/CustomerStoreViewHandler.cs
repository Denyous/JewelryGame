using UnityEngine;
using UnityEngine.UI;

public class CustomerStoreViewHandler : UIPanel
{
    [Header("Customer Presenter Reference")]
    [SerializeField] private CustomerPresenter _customerPresenter;
    [SerializeField] private Image _customerImage;
    [SerializeField] private Image _jewelImage;

    void Start()
    {
        _customerPresenter = GameObject.Find("CustomerPresenter").GetComponent<CustomerPresenter>();

        
    }

    public void GetCustomerToStore()
    {
        if (_customerPresenter != null)
        {
            _customerImage.sprite = _customerPresenter._customerModel.Data.StoreSprite;
            _jewelImage.sprite = _customerPresenter._customerModel.CurrentJewel.GetItemSprite();
            UIManager.Instance.ShowPanel("CustomerStoreView");
        }
        else
        {
            Debug.Log("Customer presenter değişkeni atanmadı");
        }
    }


    public void OnAcceptButton()
    {
        UIManager.Instance.HidePanel(Panel.StoreUI);
    }

    public void OnRefuseButton()
    {
        UIManager.Instance.HidePanel("CustomerStoreView");
    }

}