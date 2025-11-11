using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public CustomerPresenter presenter;
    public Button buyButton;
    public Button refuseButton;

    private void Start()
    {
        
        presenter.SpawnNextCustomer();
        ButtonListenerAdd();

    }

    public void ButtonListenerAdd()
    {
        // Buton listenerlarını CustomerManager içinde bağla
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyPressed);

        refuseButton.onClick.RemoveAllListeners();
        refuseButton.onClick.AddListener(OnRefusePressed);

    }
    private void OnBuyPressed()
    {
        // Şu an sadece diğer müşteriyi getir
        Debug.Log("Buy pressed - yeni müşteri geliyor.");
        presenter.SpawnNextCustomer();
    }

    private void OnRefusePressed()
    {
        // Şu an sadece diğer müşteriyi getir
        Debug.Log("Refuse pressed - yeni müşteri geliyor.");
        presenter.SpawnNextCustomer();
    }
}
