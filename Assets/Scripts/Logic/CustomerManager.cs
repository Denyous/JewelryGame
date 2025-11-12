using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public CustomerPresenter presenter;
    

    private void Start()
    {
        presenter.SpawnNextCustomer();
    }

   
}
