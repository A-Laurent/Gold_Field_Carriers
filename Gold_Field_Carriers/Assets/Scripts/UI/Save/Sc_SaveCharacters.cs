using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SaveCharacters : MonoBehaviour
{
    public Sc_DataCharacters _DataCharacters = new Sc_DataCharacters();
    public static Sc_SaveCharacters Instance;

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

    public void LoadFromJson()
    {
        string filepath = Application.dataPath + "/Saves/Characters.json";
        string gameData = File.ReadAllText(filepath);

        _DataCharacters = JsonUtility.FromJson<Sc_DataCharacters>(gameData);
        
        Sc_CharacterManager.Instance._characterSelectedNames[0] = _DataCharacters._selectedCharactersNames[0];
        Sc_CharacterManager.Instance._characterSelectedNames[1] = _DataCharacters._selectedCharactersNames[1];
        Sc_CharacterManager.Instance._characterSelectedNames[2] = _DataCharacters._selectedCharactersNames[2];

        Sc_CharacterManager.Instance._characterSelectedSprites[0] = _DataCharacters._selectedCharactersSprites[0];
        Sc_CharacterManager.Instance._characterSelectedSprites[1] = _DataCharacters._selectedCharactersSprites[1];
        Sc_CharacterManager.Instance._characterSelectedSprites[2] = _DataCharacters._selectedCharactersSprites[2];
    }

    public void SaveToJson()
    {
        _DataCharacters._selectedCharactersNames.Add(Sc_SpritesCharacters.Instance._selectedCharacters[0].GetComponentInChildren<TMP_Text>().text);
        _DataCharacters._selectedCharactersNames.Add(Sc_SpritesCharacters.Instance._selectedCharacters[1].GetComponentInChildren<TMP_Text>().text);
        _DataCharacters._selectedCharactersNames.Add(Sc_SpritesCharacters.Instance._selectedCharacters[2].GetComponentInChildren<TMP_Text>().text);  
        
        _DataCharacters._selectedCharactersSprites.Add(Sc_SpritesCharacters.Instance._selectedCharacters[0].GetComponent<Image>().sprite);
        _DataCharacters._selectedCharactersSprites.Add(Sc_SpritesCharacters.Instance._selectedCharacters[1].GetComponent<Image>().sprite);
        _DataCharacters._selectedCharactersSprites.Add(Sc_SpritesCharacters.Instance._selectedCharacters[2].GetComponent<Image>().sprite);
        
        string gameData = JsonUtility.ToJson(_DataCharacters);
        File.WriteAllText(Application.dataPath + "/Saves/Characters.json", gameData);
        Debug.Log("Characters selection saved !");
    }

    private void SaveData()
    {

    }

    private void LoadData()
    {

    }
}
