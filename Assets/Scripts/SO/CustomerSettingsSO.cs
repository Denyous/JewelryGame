using UnityEngine;

[CreateAssetMenu(fileName = "Customer Settings", menuName = "Scriptable Objects/Customer Settings")]
public class CustomerSettingsSO : ScriptableObject
{
    [field: SerializeField] public CustomerCharacter CustomerCharacter { get; private set; }

    [Header("Lower is more Generous")]
    [SerializeField][Range(0, 1f)] private float _moraleMultiplier;
    [Header("Lower is more Generous")]
    [SerializeField][Range(0, 1f)] private float _bargainMultiplier;

    public float GetMoraleMultiplier()
    {
        return _moraleMultiplier;
    }

    public float GetBargainMultiplier()
    {
        return _bargainMultiplier;
    }
}