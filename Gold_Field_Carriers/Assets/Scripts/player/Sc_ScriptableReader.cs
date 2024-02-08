using System;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ScriptableReader : MonoBehaviour
{
    [SerializeField] private List<SC_PlayerClass> _scriptableObject = new List<SC_PlayerClass>();

    public List<SC_PlayerClass> _playerClasses = new List<SC_PlayerClass>();
    
    [HideInInspector] public string _name { get; private set; }
    [HideInInspector] public string _desc { get; private set; }
    [HideInInspector] public Sprite _sprite { get; private set; }

    [HideInInspector] public bool _isAlive { get; private set; }
    [HideInInspector] public bool _isInTheCity { get; private set; }
    
    [HideInInspector] public int _currentLife { get; private set; }
    [HideInInspector] public int _maxLife { get; private set; }

    [HideInInspector] public int _currentAmmount { get; private set; }
    [HideInInspector] public int _maxAmmount { get; private set; }

    [HideInInspector] public int _gold { get; private set; }
    [HideInInspector] public int _id { get; private set; }

    public static Sc_ScriptableReader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Two instance of Sc_ScriptableReader");
            return;
        }
    }

    private void Start()
    {
        for (var i = 0; i < Sc_CharacterManager.Instance._ID.Count; i++)
        {
            for (var j = 0; j < _scriptableObject.Count; j++)
            {
                if (_scriptableObject[j]._id == Sc_CharacterManager.Instance._ID[i])
                    _playerClasses.Add(_scriptableObject[j]);
            }
        }
    }
}
