using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour
{
    private List<int> _gold = new();
    private List<int> _bullet = new();
    private List<int> _hp = new();

    public GameObject ImagePlayer1;
    public GameObject ImagePlayer2;
    public GameObject ImagePlayer3;
    private List<GameObject> _stats = new();

    public List<GameObject> player1_bulletImage;
    public List<GameObject> player_goldImage;
    public List<GameObject> player1_hpImage;
    public List<GameObject> player2_bulletImage;
    public List<GameObject> player2_hpImage;
    public List<GameObject> player3_bulletImage;
    public List<GameObject> player3_hpImage;

    public List<GameObject> _minimizeHp;
    public List<GameObject> _minimizeGold;
    public List<GameObject> _minimizeBullet;

    public List<GameObject> _fullName;
    public List<GameObject> _minimizeName;

    void Start()
    {

        _stats = Sc_CharacterManager.Instance._playerInfo;

        for (int i = 0; i < Sc_CharacterManager.Instance._playerInfo.Count; i++)
        {
            _gold.Add(_stats[i].GetComponent<Sc_ScriptableReader>()._gold);
            _hp.Add(_stats[i].GetComponent<Sc_ScriptableReader>()._currentLife);
            _bullet.Add(_stats[i].GetComponent<Sc_ScriptableReader>()._currentAmmount);
        }

    }
    
    void Update()
    {
        //if (_gold[0] == Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold ||
        //    _bullet[0] == Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentAmmount ||
        //    _hp[0] == Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentLife ||
        //    _gold[1] == Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold ||
        //    _bullet[1] == Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentAmmount ||
        //    _hp[1] == Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentLife ||
        //    _gold[2] == Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold ||
        //    _bullet[2] == Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentAmmount ||
        //    _hp[2] == Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentLife) return;
        ShowPlayingCharacter();
        SetStats();

        
    }

    public void SetStats()
    {

        for (int i = 0; i < Sc_CharacterManager.Instance._playerInfo.Count; i++)
        {
            _gold[i] = _stats[i].GetComponent<Sc_ScriptableReader>()._gold;
            _hp[i] = _stats[i].GetComponent<Sc_ScriptableReader>()._currentLife;
            _bullet[i] = _stats[i].GetComponent<Sc_ScriptableReader>()._currentAmmount;
        }

        //Player 1 HP
        if (ImagePlayer1.activeSelf)
        {
            for (int i = 0; i < _stats[0].GetComponent<Sc_ScriptableReader>()._currentLife; i++)
            {
                player1_hpImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[0].GetComponent<Sc_ScriptableReader>()._currentLife; j++)
            {
                player1_hpImage[2 - j].SetActive(false);
            }
            for (int i = 0; i < _stats[0].GetComponent<Sc_ScriptableReader>()._currentAmmount; i++)
            {
                player1_bulletImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[0].GetComponent<Sc_ScriptableReader>()._currentAmmount; j++)
            {
                player1_bulletImage[2 - j].SetActive(false);
            }
            player_goldImage[0].GetComponent<TextMeshProUGUI>().text = _stats[0].GetComponent<Sc_ScriptableReader>()._gold.ToString();
        }

        //Player 2 HP
        if (ImagePlayer2.activeSelf)
        {
            for (int i = 0; i < _stats[1].GetComponent<Sc_ScriptableReader>()._currentLife; i++)
            {
                player2_hpImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[1].GetComponent<Sc_ScriptableReader>()._currentLife; j++)
            {
                player2_hpImage[2 - j].SetActive(false);
            }
            for (int i = 0; i < _stats[1].GetComponent<Sc_ScriptableReader>()._currentAmmount; i++)
            {
                player2_bulletImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[1].GetComponent<Sc_ScriptableReader>()._currentAmmount; j++)
            {
                player2_bulletImage[2 - j].SetActive(false);
            }
            player_goldImage[1].GetComponent<TextMeshProUGUI>().text = _stats[1].GetComponent<Sc_ScriptableReader>()._gold.ToString();
        }

        //Player 3 HP
        if (ImagePlayer3.activeSelf)
        {
            for (int i = 0; i < _stats[2].GetComponent<Sc_ScriptableReader>()._currentLife; i++)
            {
                player3_hpImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[2].GetComponent<Sc_ScriptableReader>()._currentLife; j++)
            {
                player3_hpImage[2 - j].SetActive(false);
            }
            for (int i = 0; i < _stats[2].GetComponent<Sc_ScriptableReader>()._currentAmmount; i++)
            {
                player3_bulletImage[i].SetActive(true);
            }
            for (int j = 0; j < 3 - _stats[2].GetComponent<Sc_ScriptableReader>()._currentAmmount; j++)
            {
                player3_bulletImage[2 - j].SetActive(false);
            }
            player_goldImage[2].GetComponent<TextMeshProUGUI>().text = _stats[2].GetComponent<Sc_ScriptableReader>()._gold.ToString();
        }
        for (int i = 0; i < SC_PlayerTurn.Instance.maxPlayer; i++)
        {
            _minimizeBullet[i].GetComponent<TextMeshProUGUI>().text = _stats[i].GetComponent<Sc_ScriptableReader>()._currentAmmount.ToString();
            _minimizeGold[i].GetComponent<TextMeshProUGUI>().text = _stats[i].GetComponent<Sc_ScriptableReader>()._gold.ToString();
            _minimizeHp[i].GetComponent<TextMeshProUGUI>().text = _stats[i].GetComponent<Sc_ScriptableReader>()._currentLife.ToString();
        }
    }

    public void ShowPlayingCharacter()
    {
        switch (SC_PlayerTurn.Instance.turn)
        {
            case 0:
                _fullName[0].SetActive(true);
                _fullName[1].SetActive(false);
                _fullName[2].SetActive(false);
                _minimizeName[0].SetActive(false);
                _minimizeName[1].SetActive(true);
                _minimizeName[2].SetActive(true); 
                break;

            case 1:
                _fullName[0].SetActive(false);
                _fullName[1].SetActive(true);
                _fullName[2].SetActive(false);
                _minimizeName[0].SetActive(true);
                _minimizeName[1].SetActive(false);
                _minimizeName[2].SetActive(true);
                break;

            case 2:
                _fullName[0].SetActive(false);
                _fullName[1].SetActive(false);
                _fullName[2].SetActive(true);
                _minimizeName[0].SetActive(true);
                _minimizeName[1].SetActive(true);
                _minimizeName[2].SetActive(false);
                break;

        }
    }
}
