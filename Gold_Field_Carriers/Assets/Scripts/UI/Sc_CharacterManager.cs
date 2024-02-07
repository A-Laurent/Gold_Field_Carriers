using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CharacterManager : MonoBehaviour
{
    public List<string> _characterSelectedNames = new List<string>();
    public List<Sprite> _characterSelectedSprites = new List<Sprite>();
    public List<GameObject> _playerInfo = new List<GameObject>();
    public static Sc_CharacterManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if(File.Exists(Application.dataPath + "/Saves/Characters.json"))
            Sc_SaveCharacters.Instance.LoadFromJson();  
        
        SetPlayerInfo();
    }

    private void SetPlayerInfo()
    {
        for (var i = 0; i < _characterSelectedNames.Count; i++)
        {
            for (var j = 0; j < _characterSelectedSprites.Count; j++)
            {
                _playerInfo[j].transform.GetChild(0).Find("Sprite").GetComponent<Image>().sprite = _characterSelectedSprites[j];
                _playerInfo[j].transform.GetChild(1).Find("Sprite").GetComponent<Image>().sprite = _characterSelectedSprites[j];

                _playerInfo[i].transform.GetChild(0).Find("Name").GetComponent<TMP_Text>().text = _characterSelectedNames[i];
            }
        }
    }
}
