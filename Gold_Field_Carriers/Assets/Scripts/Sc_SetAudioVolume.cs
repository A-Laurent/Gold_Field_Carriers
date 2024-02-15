using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sc_SetAudioVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public static Sc_SetAudioVolume Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogException(new Exception("You have several objects with the Sc_SetAudioVolume script in your scene. With an instance, you only need one."));
            Destroy(gameObject);
            return;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        Sc_SaveAudio.Instance.SaveFromJSON();
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        Sc_SaveAudio.Instance.SaveFromJSON();
    }
}
