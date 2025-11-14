using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReportPanel : UIPanel
{
    [Header("UI Refferences")]

    [SerializeField] private Button _nexdayButton;
    [SerializeField] private TextMeshPro _moneySpend;
    public void NextButtonOnClicked()
    {

        SceneManager.LoadScene(0);
    }
}
