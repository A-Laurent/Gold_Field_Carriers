using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_Help : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    private void Start()
    {
        ButtonHelp();
    }

    public void ButtonHelp()
    {
        Panel.SetActive(true);
        this.gameObject.SetActive(false);
    }    
    public void ButtonBack()
    {
        Panel.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
