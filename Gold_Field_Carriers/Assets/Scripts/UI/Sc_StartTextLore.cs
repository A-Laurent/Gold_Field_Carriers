using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_StartTextLore : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private string message;

    public float PauseBetweenLetter;        //valeur pas default 0.05
    public float PauseBetweenEachPoint;     //valeur paar default 0.2
        
    private void Start()
    {
        message = "";
        message = _textMeshProUGUI.text;
        _textMeshProUGUI.text = "";

        StartProgressText();
    }
    private IEnumerator ProgressText()
    {
        foreach (var letter in message.ToCharArray())
        {
            _textMeshProUGUI.text += letter;
            if(letter == '.')
            {
                yield return new WaitForSeconds(PauseBetweenEachPoint);
            }
            yield return new WaitForSeconds(PauseBetweenLetter);
        }
        yield return new WaitForSeconds(1);
        ButtonSkip();
    }

    public void StartProgressText()
    {
        StartCoroutine(ProgressText());
    }

    public void ButtonSkip()
    {
        SceneManager.LoadScene("Devroom_FixUI");
    }
}
