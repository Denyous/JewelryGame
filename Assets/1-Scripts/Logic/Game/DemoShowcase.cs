using System.Collections.Generic;
using UnityEngine;

public class DemoShowcase : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Guider;
    [SerializeField] private GameObject _nextButton;
    private int _currentGuiderIndex = 0;

    void Start()
    {
        PlayerPrefs.SetInt("HasSeenDemo", 1);
        MiniTutorial();

    }


    public void MiniTutorial()
    {
        if (PlayerPrefs.GetInt("HasSeenDemo") == 1)
        {

            _Guider[_currentGuiderIndex].SetActive(true);
        }
    }


    public void NextOnClicked()
    {
        _Guider[_currentGuiderIndex].SetActive(false);
        _currentGuiderIndex++;

        if (_currentGuiderIndex == 4)
        {
            Debug.Log("No more guiders.");
            PlayerPrefs.SetInt("HasSeenDemo", 0);
            PlayerPrefs.Save();
            _nextButton.gameObject.SetActive(false);
        }
        else
        {
            MiniTutorial();
        }
            
    }


}
