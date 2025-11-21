using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public List<InventorySlot> _slots;

    public void UpdateUI(List<JewelrySO> currentItems)
    {
        foreach (var slot in _slots)
        {
            slot.ClearSlot();
        }

        for (int i = 0; i < currentItems.Count && i < _slots.Count; i++)
        {
            _slots[i].SetSlot(currentItems[i]);
        }
    }
}