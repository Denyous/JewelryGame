using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private GameObject _demoFinishedPanel;
    public int CurrentDay { get; private set; } = 1;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateDayText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void UpdateDayText()
    {
        _dayText.text = "Day " + CurrentDay.ToString();
    }

    public void StartNewDay()
    {
        CurrentDay++;

        if (CurrentDay == 4)
        {
            Time.timeScale = 0f;
            _demoFinishedPanel.SetActive(true);
        }

        UpdateDayText();
    }

    public void OkOnClicked()
    {
        Application.Quit();
    }


}
