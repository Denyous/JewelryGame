using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image _itemSprite;
    public TextMeshProUGUI _itemWorth;

    public void SetSlot(JewelrySO item)
    {
        _itemSprite.sprite = item.GetItemSprite();
        _itemSprite.enabled = true;

        _itemWorth.text = item.GetItemWorth().ToString() + "$";
        _itemWorth.enabled = true;
    }

    public void ClearSlot()
    {
        _itemSprite.sprite = null;
        _itemSprite.enabled = false;

        _itemWorth.text = "";
        _itemWorth.enabled = false;
    }
}
