using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ControllerFade : MonoBehaviour
{
     Sc_FadeInOut fade;

    private void Start()
    {
        fade = FindObjectOfType<Sc_FadeInOut>();

        fade.FadeOut();
    }
}
