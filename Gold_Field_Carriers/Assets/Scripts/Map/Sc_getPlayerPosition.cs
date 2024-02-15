using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_getPlayerPosition : MonoBehaviour
{
    public GameObject _position;

    private void OnTriggerEnter(Collider other)
    {
        _position = other.gameObject;
        //Get gold if player is dead
        if (_position.GetComponent<Sc_Neighbor>()._isThereGold)
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += _position.GetComponent<Sc_Neighbor>()._gold;
            _position.GetComponent<Sc_Neighbor>()._gold = 0;
            _position.GetComponent<Sc_Neighbor>()._isThereGold = false;
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife <= 0)
                    Sc_SpriteDeath._isGold[i] = false;
            }
        }
        if (other.name == "P1" || other.name == "P2" || other.name == "P3")
            return;
        
        if (_position.name != "End")
            _position.tag = "Occuped";    
    }

    private void OnTriggerExit(Collider other)
    {
        _position.tag = "Path";
    }
}
