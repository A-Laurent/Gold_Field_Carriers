using TMPro;
using UnityEngine;

public class Sc_VictoryDefeat : MonoBehaviour
{
    public static bool _endGame;

    public GameObject _win;
    public GameObject _loseGold;
    public GameObject _loseHp;

    public TextMeshProUGUI _nbGoldWin;
    public TextMeshProUGUI _nbGoldLose;
    public void Update()
    {
        if (SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.transform.parent.name == "Steps" && 
            SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.transform.name == "End")
        {
            SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn] = false;
            SC_PlayerTurn.Instance.turn += 1;
            Sc_CharacterManager.Instance.ChangePlayer();
            if (SC_PlayerTurn.Instance.turn >= Stats._nbPlayer)
                SC_PlayerTurn.Instance.turn = 0;
        } 

        if (_endGame)
        {
            
            if (Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentLife <= 0 &&
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentLife <= 0 &&
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentLife <= 0)
                _loseHp.SetActive(true);
            else if (Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold >= 70)
                _win.SetActive(true);
            else
                _loseGold.SetActive(true);

            _nbGoldWin.text = (Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold).ToString() + " / " + " 70";

            _nbGoldLose.text = (Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold +
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold).ToString();
            Time.timeScale = 0.0f;
        }
    }
}
