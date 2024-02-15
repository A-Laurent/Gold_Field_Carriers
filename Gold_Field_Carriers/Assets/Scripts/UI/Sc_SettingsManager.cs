using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_SettingsManager : MonoBehaviour
{
    public Sc_AudioData _audioData = new Sc_AudioData();
    
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    Sc_FadeInOut fade;

    public static Sc_SettingsManager Instance;

    private void Awake()
    {
        fade = FindObjectOfType<Sc_FadeInOut>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogException(new Exception("You have several objects with the Sc_SettingsManager script in your scene. With an instance, you only need one."));
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SetSliderValue();
    }
    
    public void SetSliderValue()
    {
        string filepath = Application.dataPath + "/Saves/Audio.json";
        string gamedata = File.ReadAllText(filepath);

        _audioData = JsonUtility.FromJson<Sc_AudioData>(gamedata);

        _musicSlider.value = _audioData._musicVolume;
        _sfxSlider.value = _audioData._sfxVolume;
        
        Debug.Log(_audioData._musicVolume);
    }
    
    private IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }
    public void Back()
    {
        StartCoroutine(ChangeScene());
    }

    public void Fullscreen()
    {
        Screen.fullScreen = transform.GetChild(3).GetComponent<Toggle>().isOn;
    }

    public void BackInPause()
    {
        gameObject.SetActive(false);
    }
}
