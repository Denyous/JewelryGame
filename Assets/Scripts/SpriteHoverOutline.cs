using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class SpriteHoverEventSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material mat;

    void Awake()
    {
        mat = new Material(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = mat;
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
}
