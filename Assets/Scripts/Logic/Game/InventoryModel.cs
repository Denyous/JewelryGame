using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    public List<JewelrySO> Items = new List<JewelrySO>();

    public event Action OnInventoryChanged;

    public void Add(JewelrySO item)
    {
        
        Items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void Remove(JewelrySO item)
    {
        Items.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public int GetJewelCount()
    {
        return Items.Count;
    }

    public JewelrySO GetJewel(float customerBalance)
    {
        List<JewelrySO> _customerCanBuy = new List<JewelrySO>();

        foreach (JewelrySO item in Items)
        {
            if (item == null)
            {
                continue;
            }
            if (customerBalance >= item.GetMaxItemWorth()) // Burada karşılaştırma ürünün satın aldığımız offer fiyatı da olabilir ----> JewelOfferPrice
            {
                _customerCanBuy.Add(item);
            }
        }

        System.Random random = new System.Random();

        return _customerCanBuy[random.Next(_customerCanBuy.Count)]; //Alabileceği takıları listeye attıktan sonra aralarından bir tane takıyı random gönderiyorum.


    }
}
