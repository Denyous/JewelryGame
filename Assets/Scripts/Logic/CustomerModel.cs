using UnityEngine;

[RequireComponent(typeof(View))]
public class CustomerModel : MonoBehaviour
{
    public CustomerSO customerData;
    public JewelrySO jewelData;
    public View view;
    public CustomerPresenter presenter;

    private void Awake()
    {
        view = GetComponent<View>();
    }

    public void Initialize(CustomerPresenter _presenter)
    {
        presenter = _presenter;
    }

    public void SetData(CustomerSO customer, JewelrySO jewel)
    {
        customerData = customer;
        jewelData = jewel;
        UpdateView();
    }

    private void UpdateView()
    {
        Debug.Log("View has been updated");
       
        view.UpdateUI(customerData.GetCustomerName(), customerData.GetCustomerSprite(), jewelData.GetItemName(), jewelData.GetItemSprite(), jewelData.GetItemWorth());
        
        if (view == null|| jewelData == null || customerData == null )
        {
            Debug.Log("data ulaşmıyor.");
        }
              
    }


    public void SetMorale(int newMorale)
    {
        //if (customerData == null) return;
        //customerData.g = newMorale;
        //view.UpdateMorale(newMorale);
    }



}
