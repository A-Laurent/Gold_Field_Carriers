using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _riverDecoration = new List<GameObject>();
    [SerializeField] private List<GameObject> _desertDecoration = new List<GameObject>();

    public static Sc_GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogException(new Exception("You have several objects with the Sc_GameManager script in your scene. With an instance, you only need one."));
            Destroy(gameObject);
            return;
        }
    }

    public void RaiseDecoration(List<GameObject> zonedecoration)
    {
        foreach (var decoration in zonedecoration)
        {
            decoration.GetComponent<Animation>().Play("Raise");
        }
    }
    
    public void LowerDecoration(List<GameObject> zonedecoration)
    {
        foreach (var decoration in zonedecoration)
        {
            decoration.GetComponent<Animation>().Play("Lower");
        }
    }

    public void StatsUI(int gold, int life, int bullet, int turn)
    {
        Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(8).GetComponent<TMP_Text>().text = gold.ToString();
       
        Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = life.ToString();
        
        Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(1).GetChild(4).GetComponent<TMP_Text>().text = bullet.ToString();
        
        Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(1).GetChild(6).GetComponent<TMP_Text>().text = gold.ToString();
        
        switch (life)
        {
            case 1 :
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(2).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(3).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                break;
            case 2 :
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(2).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(3).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                break;
            default:
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(2).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(3).GetComponent<Image>().color = 
                    Color.white;
                break;
        }
        
        switch (bullet)
        {
            case 1 :
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(4).GetComponent<Image>().color =
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(5).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(6).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                break;
            case 2 :
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(4).GetComponent<Image>().color =
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(5).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(6).GetComponent<Image>().color = 
                    new Color32(118, 118, 118, 255);
                break;
            default:
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(4).GetComponent<Image>().color =
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(5).GetComponent<Image>().color = 
                    Color.white;
                Sc_CharacterManager.Instance._playerInfo[turn].transform.GetChild(0).GetChild(6).GetComponent<Image>().color = 
                    Color.white;
                break;
        }
    }
}
