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
        Debug.Log("ManagerÇalıştı");
      _inventory.Add(item);
    }

    public void RemoveJewel(JewelrySO item)
    {
        _inventory.Remove(item);
    }

    public int GetJewelCount()
    {
        //Eğer envanterdeki item sayısı bellirli bir seviyeye ulaşırsa Store UI vitrinlerdeki spritelar değişecek.
        //StoreUI sürekli Update bu değeri kontrol edebilir.
         return _inventory.GetJewelCount();
    }
    public JewelrySO GetJewelToSell(CustomerModel _customer)
    {
        //Envanterimizden item satın almak isteyen müşteriye kendi bakiyesinin miktarında ürün getirir.
        //CustomerCurrentBalance değerini göndericem ve
        //InventoryModel kendi içinde hangi itemlerin değeri müşteriye göre diyip listeden seçtiklerinden bir tanesini göndericek
        // return _inventory.GetJewel(customerBalance);
        return null;
    }

    void UpdateUI()
    {
        _inventoryView.UpdateUI(_inventory.Items);
    }
}
