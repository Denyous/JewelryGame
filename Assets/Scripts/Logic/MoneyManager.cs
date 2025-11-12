using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public float PlayerMoney { get; private set; }

    private void Awake()
    {
        PlayerMoney = 3000f;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public bool CanAfford(float amount)
    {
        return PlayerMoney >= amount;
    }

    public void AddMoney(float amount)
    {
        PlayerMoney += amount;
    }

    public void RemoveMoney(float amount)
    {
        if (CanAfford(amount))
        {
            PlayerMoney -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough money to deduct the specified amount.");
        }
    }
    public void SetMoney(float amount)
    {
        PlayerMoney = amount;
    }
}
