using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [field: SerializeField] public string CustomerName { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public CustomerSettingsSO Settings { get; private set; }
    public CustomerCharacter CustomerCharacter => GetCustomerCharacter();
    public float MoraleMultiplier => Settings.GetMoraleMultiplier();
    public float BargainMultiplier => Settings.GetBargainMultiplier();

    //Change Sprites of the customer according to Mood
    //But this will probably will go to Customer View
    public void ChangeSprite(CustomerMood mood)
    {
        switch (mood)
        {
            case CustomerMood.Mad:
                break;
            case CustomerMood.Angry:
                break;
            case CustomerMood.Upset:
                break;
            case CustomerMood.Normal:
                break;
            case CustomerMood.Cheerful:
                break;
            case CustomerMood.Happy:
                break;
            default:
                break;
        }
    }

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



