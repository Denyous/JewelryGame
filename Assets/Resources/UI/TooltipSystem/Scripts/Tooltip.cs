using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public InputSystemUIInputModule InputModule;
    public TextMeshProUGUI HeaderField;
    public TextMeshProUGUI ContentField;
    public LayoutElement LayoutElement;
    public int CharacterWrapLimit;

    public RectTransform RectTransform;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = HeaderField.text.Length;
            int contentLength = ContentField.text.Length;

            LayoutElement.enabled = (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
        }

        Vector2 position = InputModule.point.action.ReadValue<Vector2>();

        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        RectTransform.pivot = pivot;
        transform.position = position;
    }

    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            HeaderField.gameObject.SetActive(false);
        }
        else
        {
            HeaderField.gameObject.SetActive(true);
            HeaderField.text = header;
        }

        ContentField.text = content;

        int headerLength = HeaderField.text.Length;
        int contentLength = ContentField.text.Length;

        LayoutElement.enabled = (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
    }
}
