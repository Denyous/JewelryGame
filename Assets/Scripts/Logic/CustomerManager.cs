using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance {  get; private set; }
    public CustomerPresenter presenter;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GetNextCustomer();
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
