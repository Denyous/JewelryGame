using UnityEngine;
using UnityEngine.UI;

public class InventoyPanel : UIPanel
{
    [Header("UI Variables")]
    [SerializeField] Button _closeButton;


    public void CloseButtonOnClicked()
    {
        UIManager.Instance.HidePanel(Panel.InventoryPanel);
    }
   
}
