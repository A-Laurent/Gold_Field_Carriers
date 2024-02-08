using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CharacterManager : MonoBehaviour
{
    public List<SC_PlayerClass> _scriptableObject = new List<SC_PlayerClass>();

    public List<GameObject> _playerInfo = new List<GameObject>();
    public List<int> _ID = new List<int>();

    public List<SC_PlayerClass> _selectedPlayer = new List<SC_PlayerClass>();

    public bool _canSetPlayerInfo = true;

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
            Debug.LogError("Two instance of Sc_CharacterManager");
            return;
        }

        if (File.Exists(Application.dataPath + "/Saves/Characters.json"))
            Sc_SaveCharacters.Instance.LoadFromJson();
    }

    private void Start()
    {
        SetPlayerInfo();
    }

    private void SetPlayerInfo()
    {
        foreach (var playerinfo in _playerInfo)
        {
            playerinfo.transform.GetChild(0).Find("Sprite").GetComponent<Image>().sprite =
                playerinfo.GetComponent<Sc_ScriptableReader>()._sprite;

            playerinfo.transform.GetChild(1).Find("Sprite").GetComponent<Image>().sprite =
                playerinfo.GetComponent<Sc_ScriptableReader>()._sprite;
            playerinfo.transform.GetChild(0).Find("Name").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._name;
        }

        foreach (var playerinfo in _playerInfo)
        {
            playerinfo.transform.GetChild(0).Find("Number of Gold").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._gold.ToString();
        }
    }
}