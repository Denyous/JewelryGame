using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : UIPanel
{
    [Header("UI Variables")]
    [SerializeField] Button _closeButton;

    public void CloseButtonOnClicked()
    {
        UIManager.Instance.HidePanel(Panel.InventoryPanel);
        AudioManager.PlayOneShot(SoundType.CloseInventory);
        AudioManager.PlayOneShot(SoundType.Click);
    }
}
