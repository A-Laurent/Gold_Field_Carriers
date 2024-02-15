using System;
using System.IO;
using UnityEngine;

public class Sc_SaveAudio : MonoBehaviour
{
    public Sc_AudioData _audioData = new Sc_AudioData();
    public static Sc_SaveAudio Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogException(new Exception("You have several objects with the Sc_SaveAudio script in your scene. With an instance, you only need one."));
            Destroy(gameObject);
            return;
        }
    }

    public void LoadFromJSON()
    {
        string filepath = Application.persistentDataPath + "/Audio.json";
        string gamedata = File.ReadAllText(filepath);

        _audioData = JsonUtility.FromJson<Sc_AudioData>(gamedata);

        Sc_SetAudioVolume.Instance.audioMixer.SetFloat("Music", _audioData._musicVolume);
        Sc_SetAudioVolume.Instance.audioMixer.SetFloat("SFX", _audioData._sfxVolume);
    }

    public void SaveFromJSON()
    {
        Sc_SetAudioVolume.Instance.audioMixer.GetFloat("Music", out _audioData._musicVolume);
        Sc_SetAudioVolume.Instance.audioMixer.GetFloat("SFX", out _audioData._sfxVolume);

        string gamedata = JsonUtility.ToJson(_audioData);
        File.WriteAllText(Application.persistentDataPath + "/Audio.json", gamedata);
        Debug.Log("Audio is saved");
    }
}
