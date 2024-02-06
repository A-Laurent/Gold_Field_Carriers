using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_SettingsManager : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Fullscreen()
    {
        Screen.fullScreen = transform.GetChild(3).GetComponent<Toggle>().isOn;
    }
}
