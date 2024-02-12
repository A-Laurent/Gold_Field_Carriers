using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public static List<string> _zonePlayer = new();

    private SC_PlayerTurn _playerTurnInstance = SC_PlayerTurn.Instance;
    public static int _nbPlayer = 3;
    public Text _zoneText;

    private void Awake()
    {
        _playerTurnInstance = SC_PlayerTurn.Instance;
    }

    private void Start()
    {
        for (int i = 0; i < _nbPlayer; i++)
        {
            _zonePlayer.Add("Start");
        }
    }
    void Update()
    {
        //_zoneText.text = "Zone : " + _zonePlayer[0] + " " + _zonePlayer[1] + " " + _zonePlayer[2];
        StatLimit();
    }
    public void StatLimit()
    {
        //HP
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife > 3)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife = 3;
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife == 0)
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= 8;
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<TMP_Text>().text = Sc_CharacterManager
                .Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold.ToString();
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife = 1;
        }

        //Bullet
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 3)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount = 3;
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount < 0)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount = 0;

        //Gold
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold < 0)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold = 0;
    }
}
