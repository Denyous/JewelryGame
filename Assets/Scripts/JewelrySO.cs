using UnityEngine;

[CreateAssetMenu(fileName = "JewelrySO", menuName = "Scriptable Objects/JewelrySO")]
public class JewelrySO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemWorth;
    [SerializeField] private Sprite itemSprite;

    public string GetItemName()
    {
        return itemName;
    }
    public int GetItemWorth()
    {
        return itemWorth;
    }
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }
}
