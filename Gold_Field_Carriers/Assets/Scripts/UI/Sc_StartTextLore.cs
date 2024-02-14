using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_StartTextLore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private GameObject ButtonStartGame;
    [SerializeField] private GameObject ButtonSkip;

    private string message;
    private bool SkipIntro = false;

    public float PauseBetweenLetter;        //valeur pas default 0.035
    public float PauseBetweenPoint;     //valeur par default 0.5


    Sc_FadeInOut fade;

    private void Awake()
    {
        fade = FindObjectOfType<Sc_FadeInOut>();
    }

    private void Start()
    {
        ButtonStartGame.SetActive(false);
        message = "";
        message = _textMeshProUGUI.text;
        _textMeshProUGUI.text = "";

        StartProgressText();
    }
    private IEnumerator ProgressText()
    {
        if (SkipIntro == false)
        {
            foreach (var letter in message.ToCharArray())
            {
                _textMeshProUGUI.text += letter;
                if (letter == '.')
                {
                    yield return new WaitForSeconds(PauseBetweenPoint);
                }
                yield return new WaitForSeconds(PauseBetweenLetter);
            }
            ButtonSkip.SetActive(false);
            yield return new WaitForSeconds(1f);
            ButtonStartGame.SetActive(true);
        }
    }

    public void StartProgressText()
    {
        StartCoroutine(ProgressText());
    }

    private IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Devroom_FixUI");
    }

    public void StartGame()
    {
        SkipIntro = true;
        StartCoroutine(ChangeScene());
    }
}
