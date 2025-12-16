using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartButtonClickHandler);
        _settingsButton.onClick.AddListener(SettingsButtonClickHandler);
        _quitButton.onClick.AddListener(QuitButtonClickHandler);
    }

    private void QuitButtonClickHandler()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }

    private void SettingsButtonClickHandler()
    {
        Debug.Log("Kozmetik butonu o kral");
    }

    private void StartButtonClickHandler()
    {
        SceneManager.LoadScene("Main");
    }
}
