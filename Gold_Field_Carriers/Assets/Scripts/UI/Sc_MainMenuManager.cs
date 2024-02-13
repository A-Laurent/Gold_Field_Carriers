using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_MainMenuManager : MonoBehaviour
{
    [SerializeField] private Sc_FadeInOut fade;
    private string saveFolderPath = Application.dataPath + "/Saves";

    private string SceneToLoad;
    private float timer;
    
    private void Start()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
    }

    private IEnumerator ChangeScene()
    {
        fade.FadeIn();
        if (fade.TimeToFade == 2f)
        {
            timer = 0.5f;
        }
        else if (fade.TimeToFade == 1)
        {
            timer = fade.TimeToFade;
        }
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(SceneToLoad);

        yield return null;
    }


    public void Play()
    {
        fade.TimeToFade = 1f;
        SceneToLoad = "CharacterSelection";
        StartCoroutine(ChangeScene());
    }
    public void Settings()
    {
        fade.TimeToFade = 2f;
        SceneToLoad = "Settings";
        StartCoroutine(ChangeScene());
        //SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        fade.TimeToFade = 2f;
        SceneToLoad = "Credits";
        StartCoroutine(ChangeScene());
        //SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        if(File.Exists(Application.dataPath + "/Saves/Characters.json"))
            File.Delete(Application.dataPath + "/Saves/Characters.json");
        Application.Quit();
    }
}
