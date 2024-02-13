using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Credits : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> m_objectsToFade = new List<CanvasGroup>();
    [SerializeField] private float m_FadeInDuration, m_fadeOutDuration, m_AttendanceTime, m_WaitBeforeStart;

    public static Sc_Credits Instance;
    Sc_FadeInOut fade;


    private void Awake()
    {
        fade = FindObjectOfType<Sc_FadeInOut>();
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("There is two FadeCredits");
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(StartFadeSequence());
    }

    private IEnumerator StartFadeSequence()
    {
        yield return new WaitForSeconds(m_WaitBeforeStart);

        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        foreach (CanvasGroup canvasGroup in m_objectsToFade)
        {
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, m_FadeInDuration));

            yield return new WaitForSeconds(m_AttendanceTime);

            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, m_fadeOutDuration));

            if (canvasGroup == m_objectsToFade[m_objectsToFade.Count - 1] && IsLastObjectDisabled())
            {
                ResetCredits();
                MainMenu();
            }
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        //Setactive false all objects of the list
        if (targetAlpha == 0f)
        {
            canvasGroup.gameObject.SetActive(false);
        }
    }

    private bool IsLastObjectDisabled()
    {
        for (var i = 0; i < m_objectsToFade.Count; i++)
        {
            if (m_objectsToFade[i].gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }


    public void ResetCredits()
    {
        StopAllCoroutines();

        foreach (CanvasGroup canvasGroup in m_objectsToFade)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(true);
        }

        StartCoroutine(StartFadeSequence());
    }

    private IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void MainMenu()
    {
        StartCoroutine(ChangeScene());
    }
}