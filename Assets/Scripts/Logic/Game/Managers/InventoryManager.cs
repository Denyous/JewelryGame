using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventoryView _inventoryView;
    public const int SlotCount = 6;
    private int _itemIndex = 0;
    [SerializeField] private List<JewelrySO> _jewels;

    private void Awake()
    {
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

    public void AddItemToInventory(JewelrySO item)
    {
        if (_jewels.Count < SlotCount)
        {
            _jewels.Add(item);
            _inventoryView.AddToInventory(item, _itemIndex);
            Debug.Log(item.GetItemName() + " envanterinize eklendi.");
            _itemIndex++;   
        }
        else
        {
            Debug.Log("Envanteriniz Doldu!");
        }
    }

    //First Update Inventory View then remove from the list because it will throw null exception otherwise
    public void RemoveItemFromInventory(JewelrySO item)
    {
        _inventoryView.RemoveFromInventory(_jewels.IndexOf(item));
        _jewels.Remove(item);
        Debug.Log(item.GetItemName() + " envanterinizden çıkarıldı.");
    }

    public List<JewelrySO> GetInventoryItems()
    {
        return _jewels;
    }
}
