using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteHoverOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material mat;

    void Awake()
    {
        if (GetComponent<Image>() != null)
        {
            mat = GetComponent<Image>().material;
        }

        if (GetComponent<SpriteRenderer>() != null)
        {
            mat = GetComponent<SpriteRenderer>().material;
        }

        mat.SetFloat("_Hover", 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mat.SetFloat("_Hover", 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mat.SetFloat("_Hover", 0f);
    }

    private void OnMouseEnter()
    {
        mat.SetFloat("_Hover", 1f);
    }

    private void OnMouseExit()
    {
        mat.SetFloat("_Hover", 0f);
    }
}
