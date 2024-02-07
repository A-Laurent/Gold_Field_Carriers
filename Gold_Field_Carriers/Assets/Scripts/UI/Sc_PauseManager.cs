using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _settings;

    public void Resume()
    {
        gameObject.SetActive(false);
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
