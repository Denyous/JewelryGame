using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance {  get; private set; }
    private CustomerPresenter presenter;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            presenter = GameObject.FindAnyObjectByType<CustomerPresenter>().GetComponent<CustomerPresenter>();
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
    public void GetNextCustomer()
    {
        presenter.SpawnNextCustomer();
    }

  

   
}
