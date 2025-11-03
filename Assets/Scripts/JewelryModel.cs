using TMPro;
using UnityEngine;

public class JewelryModel : MonoBehaviour
{
    [Header("Jewel Variables")]

    [SerializeField] public JewelrySO[] jewels;
    private SpriteRenderer jewelSprite;
    private TextMeshPro jewelName;
    private TextMeshPro jewelWorth;
    [SerializeField] private int current;



    void Start()
    {
        jewelName = GetComponentInChildren<TextMeshPro>();
        jewelSprite = GetComponentInChildren<SpriteRenderer>();       
    }

    private void Update()
    {
        JewelPlacement(current);
    }

    private void JewelPlacement(int index)
    {
        jewelName.text = jewels[index].GetItemName();
        jewelSprite.sprite = jewels[index].GetItemSprite();
    }
}
