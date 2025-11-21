using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventoryView _inventoryView;
    public InventoryModel _inventory = new InventoryModel();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _inventory.OnInventoryChanged += UpdateUI;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddJewel(JewelrySO item)
    {
        _inventory.Add(item);
    }

    public void RemoveJewel(JewelrySO item)
    {
        _inventory.Remove(item);
    }

    //Eğer envanterdeki item sayısı bellirli bir seviyeye ulaşırsa Store UI vitrinlerdeki spritelar değişecek.
    public int GetJewelCount()
    {
        return _inventory.GetJewelCount();
    }

    public List<JewelrySO> GetInventory()
    {
        return _inventory.Items;
    }

    //Envanterimizden item satın almak isteyen müşteriye kendi bakiyesinin miktarında ürün getirir.
    //CustomerCurrentBalance değerini göndericem ve
    //InventoryModel kendi içinde hangi itemlerin değeri müşteriye göre diyip listeden seçtiklerinden bir tanesini göndericek
    // return _inventory.GetJewel(customerBalance);
    public JewelrySO GetJewelToSell(CustomerModel _customer)
    {
        return null;
    }

    void UpdateUI()
    {
        _inventoryView.UpdateUI(_inventory.Items);
    }
}