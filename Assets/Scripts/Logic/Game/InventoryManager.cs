using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private List<JewelrySO> _items;

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
        _items.Add(item);
        Debug.Log(item.GetItemName() + " envanterinize eklendi.");
    }

    public void RemoveItemFromInventory(JewelrySO item)
    {
        _items.Remove(item);
        Debug.Log(item.GetItemName() + " envanterinizden çıkarıldı.");
    }


}
