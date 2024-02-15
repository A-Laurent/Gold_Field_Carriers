using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class AudioClipe
{
    public AudioClip Clip;
    public AudioSource Source;
}

public class Sc_AudioManager : MonoBehaviour
{
    public static Sc_AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogException(new Exception("You have several objects with the Sc_AudioManager script in your scene. With an instance, you only need one."));
            Destroy(gameObject);
            return;
        }
    }
    
    public List<DictionaryElement<string, AudioClipe>> sound;

    public void PlaySong(string name)
    {
        foreach (var item in sound)
        {
            if (item.Key == name)
            {
                item.Value.Source.PlayOneShot(item.Value.Clip);
            }
        }
    }
}
