using System;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ScriptableReader : MonoBehaviour
{
    public SC_PlayerClass _playerClasse;

    [HideInInspector] public string _name; 
    [HideInInspector] public string _desc; 
    [HideInInspector] public Sprite _sprite;

    [HideInInspector] public bool _isAlive; 
    [HideInInspector] public bool _isInTheCity;

    [HideInInspector] public int _currentLife; 
    [HideInInspector] public int _maxLife; 

    [HideInInspector] public int _currentAmmount;
    [HideInInspector] public int _maxAmmount;

    [HideInInspector] public int _gold; 
    [HideInInspector] public int _id;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (Sc_CharacterManager.Instance._canSetPlayerInfo)
        {
            Sc_CharacterManager.Instance._canSetPlayerInfo = false;
            foreach (var playerClass in Sc_CharacterManager.Instance._scriptableObject)
            {
                for (var i = 0; i < Sc_CharacterManager.Instance._ID.Count; i++)
                {
                    if (Sc_CharacterManager.Instance._ID[i] == playerClass._id)
                    {
                        Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._playerClasse = playerClass;
                    }
                }
            }

            Sc_CharacterManager.Instance._canSetPlayerInfo = true;
            Sc_CharacterManager.Instance._scriptableObject.Remove(_playerClasse);
        }
        
        SetVariables();
    }

    void SetVariables()
    {
        _name = _playerClasse._name;
        _desc = _playerClasse._desc;
        _sprite = _playerClasse._sprite;

        _isAlive = _playerClasse._isAlive;
        _isInTheCity = _playerClasse._isInTheCity;

        _currentLife = _playerClasse._currentLife;
        _maxLife = _playerClasse._maxLife;

        _currentAmmount = _playerClasse._currentAmmount;
        _maxAmmount = _playerClasse._maxAmmount;

        _gold = _playerClasse._gold;

        _id = _playerClasse._id;
    }
}