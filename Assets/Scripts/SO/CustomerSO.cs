using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [field: SerializeField] public string CustomerName { get; private set; }
    [field: SerializeField] public Sprite CustomerSprite { get; private set; }

    [SerializeField] [Range(0, 1f)] private float _moraleMultiplier;
    [SerializeField] [Range(0, 1f)] private float _bargainMultiplier;

    [field: SerializeField] public CustomerCharacter CustomerCharacter { get; private set; }

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

    public float GetMoraleMultiplier()
    {
        return _moraleMultiplier;
    }

    public float GetBargainMultiplier()
    {
        return _bargainMultiplier;
    }
}

public enum CustomerCharacter
{
    Greedy,
    Normal,
    Generous
}



