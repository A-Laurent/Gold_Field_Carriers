using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Sc_MainMenuManager : MonoBehaviour
{
    private string saveFolderPath = Application.dataPath + "/Saves";
    
    private void Start()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
        Sc_AudioManager.instance.PlaySong("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        if(File.Exists(Application.dataPath + "/Saves/Characters.json"))
            File.Delete(Application.dataPath + "/Saves/Characters.json");
        Application.Quit();
    }
}
