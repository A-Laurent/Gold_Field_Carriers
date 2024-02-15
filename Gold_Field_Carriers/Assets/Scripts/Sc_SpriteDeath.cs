using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SpriteDeath : MonoBehaviour
{

    public Sprite _sprite;
    void Update()
    {
        for (int i = 0; i <Stats._nbPlayer; i++)
        {
            if (Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()
                ._currentLife <= 0)
            {
                SC_PlayerTurn.Instance._player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprite;
                SC_PlayerTurn.Instance._player[i].transform.GetChild(1).gameObject.SetActive(true);
                SC_PlayerTurn.Instance._player[i].transform.GetChild(0).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
