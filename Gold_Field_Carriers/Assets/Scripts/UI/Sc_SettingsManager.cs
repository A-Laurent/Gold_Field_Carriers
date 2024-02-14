using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_SettingsManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Fullscreen()
    {
        Screen.fullScreen = transform.GetChild(3).GetComponent<Toggle>().isOn;
    }

    public void BackInPause()
    {
        gameObject.SetActive(false);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
