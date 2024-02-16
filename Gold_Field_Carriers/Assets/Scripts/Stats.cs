using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static List<string> _zonePlayer = new();

    public static int _nbPlayer = 3;

    private void Start()
    {
        for (int i = 0; i < _nbPlayer; i++)
        {
            _zonePlayer.Add("Start");
        }
    }

    void Update()
    {
        StatLimit();
        for (int i = 0; i < _nbPlayer; ++i)
        {
            Sc_GameManager.Instance.StatsUI(Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._gold,
                        Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife,
                        Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentAmmount,
                        i);
        }       
    }

    public void StatLimit()
    {
        //HP
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife >
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._maxLife)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife =
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._maxLife;

        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife == 0)
        {
            if(Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold != 0)
                SC_PlayerTurn.Instance.currentPlayer.transform.position += new Vector3(0, 0f, 3f);
            if(!SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._isThereGold)
                SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._gold =
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold;
            
            SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._isThereGold = true;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold = 0;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount = 0;
                
                SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn] = false;
        }

        //Bullet
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount >
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._maxAmmount)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount =
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._maxAmmount;

        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount < 0)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount = 0;

        //Gold
        if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold < 0)
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold = 0;
    }
}