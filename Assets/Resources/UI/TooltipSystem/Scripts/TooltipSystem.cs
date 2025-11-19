using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem Instance;
    public Tooltip ToolTip;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show(string content, string header = "")
    {
        Instance.ToolTip.SetText(content, header);
        Instance.ToolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.ToolTip.gameObject.SetActive(false);
    }
}
