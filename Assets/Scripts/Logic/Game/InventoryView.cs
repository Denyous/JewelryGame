using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{

    [Header("UI Refferences")]

    [SerializeField] private List<Image> _slotSprites;

    void Start()
    {

    }

    public void UpdateInventoryView(JewelrySO jewelToAdd, int jewelIndex)
    {

        _slotSprites[jewelIndex].sprite = jewelToAdd.GetItemSprite();

        //GameObject listi oluşturup burda compenantlarına ayırıp değerleri verebilirim.

    }


}
