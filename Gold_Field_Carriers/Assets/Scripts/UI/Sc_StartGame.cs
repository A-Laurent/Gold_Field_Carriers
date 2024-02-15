using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sc_StartGame : MonoBehaviour
{
    [SerializeField] private GameObject Panel_Text;
    Sc_FadeInOut fade;
    private void Awake()
    {
        Panel_Text.SetActive(false);
        fade = FindObjectOfType<Sc_FadeInOut>();
    }
    public void StartGame()
    {
        if (Sc_SpritesCharacters.Instance._selectedCharacters.Count == 3)
        {
            Sc_SaveCharacters.Instance.SaveToJson();
            Panel_Text.SetActive(true);
        }
    }
    private IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnMainMenus()
    {
        StartCoroutine(ChangeScene());
    }
}
