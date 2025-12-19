using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [field: SerializeField] public string CustomerName { get; private set; }
    [field: SerializeField] public Sprite StoreSprite { get; private set; }
    [field: SerializeField] public Sprite SadSprite { get; private set; }
    [field: SerializeField] public Sprite AngrySprite { get; private set; }
    [field: SerializeField] public Sprite NeutralSprite { get; private set; }
    
    [field: SerializeField] public CustomerSettingsSO Settings { get; private set; }
    public CustomerCharacter CustomerCharacter => GetCustomerCharacter();
    public float MoraleMultiplier => Settings.GetMoraleMultiplier();
    public float BargainMultiplier => Settings.GetBargainMultiplier();

    public CustomerCharacter GetCustomerCharacter()
    {
        return Settings.CustomerCharacter;
    }
}

public enum CustomerCharacter
{
    Greedy,
    Normal,
    Generous
}



