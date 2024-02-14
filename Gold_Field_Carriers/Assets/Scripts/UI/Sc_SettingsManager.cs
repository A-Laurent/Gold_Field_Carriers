using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_SettingsManager : MonoBehaviour
{
    Sc_FadeInOut fade;

    private void Awake()
    {
        fade = FindObjectOfType<Sc_FadeInOut>();
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
