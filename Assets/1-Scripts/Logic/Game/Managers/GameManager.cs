using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI _dayText;
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

        if (CurrentDay == 6)
        {
            //Demo has finished panel occurs at screen!
        }

        UpdateDayText();
    }


}
