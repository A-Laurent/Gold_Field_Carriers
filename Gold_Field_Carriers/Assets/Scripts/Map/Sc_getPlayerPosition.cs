using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_getPlayerPosition : MonoBehaviour
{
    public GameObject _position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "P1" || other.name == "P2" || other.name == "P3")
            return;
        _position = other.gameObject;
        if (_position.name != "End")
            _position.tag = "Occuped";
        
        //Get gold if player is dead
        if (_position.GetComponent<Sc_Neighbor>()._isThereGold) 
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += _position.GetComponent<Sc_Neighbor>()._gold;
            _position.GetComponent<Sc_Neighbor>()._gold = 0;
            _position.GetComponent<Sc_Neighbor>()._isThereGold = false;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _position.tag = "Path";
    }
}
