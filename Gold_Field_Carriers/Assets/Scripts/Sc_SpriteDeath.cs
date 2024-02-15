using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Sc_SpriteDeath : MonoBehaviour
{

    public Sprite _sprite;
    public static List<bool> _isGold = new();
    private List<bool> _isDeath = new();

    private void Start()
    {
        for (int i = 0; i < Stats._nbPlayer; i++)
        {
            _isGold.Add(true);
            _isDeath.Add(false);
        }
    }

    public void Update()
    {
        for (int i = 0; i < Stats._nbPlayer; i++)
        {
            if (Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()
                ._currentLife <= 0)
            {
                SC_PlayerTurn.Instance._player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprite;
                if (_isGold[i])
                    SC_PlayerTurn.Instance._player[i].transform.GetChild(1).gameObject.SetActive(true);
                else if (!_isGold[i])
                    SC_PlayerTurn.Instance._player[i].transform.GetChild(1).gameObject.SetActive(false);
                SC_PlayerTurn.Instance._player[i].transform.GetChild(0).transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
        }
    }  
}
