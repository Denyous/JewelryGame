using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventoryView _inventoryView;
    public InventoryModel _inventory = new InventoryModel();
    private const int maxInventorySize = 6;
    public GameObject _inventoryGuider;

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
        
        if (GetJewelCount() == maxInventorySize)
        {
          _inventoryGuider.SetActive(true);
        }
        else
        {
            _inventory.Add(item);
        }
    }

    public void RemoveJewel(JewelrySO item)
    {
        if (GetJewelCount() == 0)
        {
            Debug.Log("Envanter boş");
        }
        else
        {
            _inventory.Remove(item);
        }
            
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

    
    public JewelrySO GetJewelToSell(CustomerModel _customer)
    {
        return null;
    }

    void UpdateUI()
    {
        _inventoryView.UpdateUI(_inventory.Items);
    }


    public void OnOkeyInventoryButton()
    {
        _inventoryGuider.SetActive(false);
    }
}