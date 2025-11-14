using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<UIPanel> panels;

    private Dictionary<string, UIPanel> panelDict = new Dictionary<string, UIPanel>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var panel in panels)
        {
            panelDict.Add(panel.name, panel);
        }
    }

    public void ShowPanel(string panelName)
    {
        if (panelDict.ContainsKey(panelName))
            panelDict[panelName].Show();
    }

    public void HidePanel(string panelName)
    {
        if (panelDict.ContainsKey(panelName))
            panelDict[panelName].Hide();
    }

    public T GetPanel<T>() where T : UIPanel
    {
        foreach (var p in panelDict.Values)
        {
            if (p is T)
                return (T)p;
        }
        return null;
    }
}
