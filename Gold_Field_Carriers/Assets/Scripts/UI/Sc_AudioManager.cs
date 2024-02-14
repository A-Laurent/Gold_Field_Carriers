using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioClipe
{
    public AudioClip Clip;
    public AudioSource Source;
    
}

public class Sc_AudioManager : MonoBehaviour
{
    public static Sc_AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (sound[0].Key == "MainMenu")
            if (!sound[0].Value.Source.isPlaying)
                PlaySong("MainMenu");
        if (sound[0].Key == "Game")
            if (!sound[0].Value.Source.isPlaying)
                PlaySong("Game");
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
