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

    public void ChangePlayer()
    {
        switch (SC_PlayerTurn.Instance.turn)
        {
            case 0 :
                _playerInfo[0].transform.GetChild(0).gameObject.SetActive(true);
                _playerInfo[0].transform.GetChild(1).gameObject.SetActive(false);
                
                _playerInfo[1].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[1].transform.GetChild(1).gameObject.SetActive(true);
                
                _playerInfo[2].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[2].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 1 :
                _playerInfo[0].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[0].transform.GetChild(1).gameObject.SetActive(true);
                
                _playerInfo[1].transform.GetChild(0).gameObject.SetActive(true);
                _playerInfo[1].transform.GetChild(1).gameObject.SetActive(false);
                
                _playerInfo[2].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[2].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                _playerInfo[0].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[0].transform.GetChild(1).gameObject.SetActive(true);
                
                _playerInfo[1].transform.GetChild(0).gameObject.SetActive(false);
                _playerInfo[1].transform.GetChild(1).gameObject.SetActive(true);
                
                _playerInfo[2].transform.GetChild(0).gameObject.SetActive(true);
                _playerInfo[2].transform.GetChild(1).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void SetPlayerInfo()
    {
        foreach (var t in _playerInfo)
        {
            t.transform.GetChild(0).Find("Sprite").GetComponent<Image>().sprite =
                t.GetComponent<Sc_ScriptableReader>()._sprite;  
            
            t.transform.GetChild(1).Find("Sprite").GetComponent<Image>().sprite =
                t.GetComponent<Sc_ScriptableReader>()._sprite;
            
            t.transform.GetChild(0).Find("Name").GetComponent<TMP_Text>().text =
                t.GetComponent<Sc_ScriptableReader>()._name;
        }

        foreach (var playerinfo in _playerInfo)
        {
            playerinfo.transform.GetChild(0).Find("Number of Gold").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._gold.ToString();
            
            playerinfo.transform.GetChild(1).Find("Number of Gold").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._gold.ToString();
            
            playerinfo.transform.GetChild(1).Find("Number of Hearth").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._currentLife.ToString();
            
            playerinfo.transform.GetChild(1).Find("Number of Bullet").GetComponent<TMP_Text>().text =
                playerinfo.GetComponent<Sc_ScriptableReader>()._currentAmmount.ToString();
        }
    }
}