using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private List<Image> _slotSprites;
    [SerializeField] private Sprite _emptySlotSprite;

    void Start()
    {

    }

    public void AddToInventory(JewelrySO jewelToAdd, int jewelIndex)
    {
        _slotSprites[jewelIndex].sprite = jewelToAdd.GetItemSprite();

        //GameObject listi oluşturup burda compenantlarına ayırıp değerleri verebilirim.
    }

    public void RemoveFromInventory(int jewelIndex)
    {
        _slotSprites[jewelIndex].sprite = _emptySlotSprite;
    }
}
