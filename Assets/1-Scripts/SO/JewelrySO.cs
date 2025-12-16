using UnityEngine;

[CreateAssetMenu(fileName = "JewelrySO", menuName = "Scriptable Objects/JewelrySO")]
public class JewelrySO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int maxItemWorth;
    [SerializeField] private int shownItemWorth;
    [SerializeField] private int minItemHorth;
    [SerializeField] private Sprite itemSprite;

    public string GetItemName()
    {
        return itemName;
    }
    public int GetMaxItemWorth()
    {
        return maxItemWorth;
    }

    public int GetItemWorth()
    {
        return shownItemWorth;
    }
    public int GetMinItemWorth()
    {
        return minItemHorth;
    }
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }
}
