using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _uiPause;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0.0f)
                Resume();
            else
                GamePaused();
        }
    }

    public void GamePaused()
    {
        _uiPause.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        _uiPause.SetActive(false);
        Time.timeScale = 1;
    }
    public void Settings()
    {
        _settings.SetActive(true);    
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
