using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Card : MonoBehaviour
{
    public int _cardIndex = 0;
    public static CardData _card;

    public List<CardData> _cardDataMountain = new();
    public List<CardData> _cardDataRiver = new();
    public List<CardData> _cardDataDesert = new();

    [HideInInspector] public List<CardData> _cardDataBackupMountain = new();
    [HideInInspector] public List<CardData> _cardDataBackupRiver = new();
    [HideInInspector] public List<CardData> _cardDataBackupDesert = new();

    public int _theHorde;
    public GameObject _uiChoice;
    public GameObject _uiTrade;
    public static List<bool> _skipTurn = new();
    public static Card Instance;
    public static bool _isChoice;

    private bool _move;

    public GameObject _horde;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void Start()
    {
        for (int i = 0; i < _cardDataDesert.Count; i++)
        {
            _cardDataBackupDesert.Add(_cardDataDesert[i]);
        }

        for (int i = 0; i < _cardDataRiver.Count; i++)
        {
            _cardDataBackupRiver.Add(_cardDataRiver[i]);
        }

        for (int i = 0; i < _cardDataMountain.Count; i++)
        {
            _cardDataBackupMountain.Add(_cardDataMountain[i]);
        }

        for (int i = 0; i < Stats._nbPlayer; i++)
        {
            _skipTurn.Add(false);
        }

        ShuffleDeck(_cardDataDesert);
        ShuffleDeck(_cardDataMountain);
        ShuffleDeck(_cardDataRiver);
    }

    private void Update()
    {
        if (Zone._draw && !CardChoice._choice)
        {
            DrawCard();
            AnimationCard._animation = true;
            Zone._draw = false;
        }

        Shuffle();
    }

    public void DrawCard()
    {
        switch (Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
        {
            case "Desert":
                Desert();
                break;
            case "River":
                River();
                break;
            case "Mountain":
                Mountain();
                break;
        }
    }

    public void ShuffleDeck<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void Shuffle()
    {
        if (_cardDataDesert.Count == 0)
        {
            for (int i = 0; i < _cardDataBackupDesert.Count; i++)
            {
                _cardDataDesert.Add(_cardDataBackupDesert[i]);
            }

            ShuffleDeck(_cardDataDesert);
        }

        if (_cardDataRiver.Count == 0)
        {
            for (int i = 0; i < _cardDataBackupRiver.Count; i++)
            {
                _cardDataRiver.Add(_cardDataBackupRiver[i]);
            }

            ShuffleDeck(_cardDataRiver);
        }


        if (_cardDataMountain.Count == 0)
        {
            for (int i = 0; i < _cardDataBackupMountain.Count; i++)
            {
                _cardDataMountain.Add(_cardDataBackupMountain[i]);
            }

            ShuffleDeck(_cardDataMountain);
        }
    }

    public void Desert()
    {
        _card = _cardDataDesert[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" &&
            _card._name != "Medium")
            _cardDataDesert.RemoveAt(_cardIndex);
    }

    public void Mountain()
    {
        _card = _cardDataMountain[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" &&
            _card._name != "Medium")
            _cardDataMountain.RemoveAt(_cardIndex);
    }

    public void River()
    {
        _card = _cardDataRiver[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" &&
            _card._name != "Medium" && _card._name != "Change of zone")
            _cardDataRiver.RemoveAt(_cardIndex);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void EffectCard()
    {
        //_description.text = _card._description;
        if (_card._name == "Attack of a bandit" && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn]
                .GetComponent<Sc_ScriptableReader>()._currentAmmount == 0)
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._gold -= 3;
            AnimationStats._goldAnim -= 3;
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._currentLife -= 1;
            AnimationStats._hpAnim -= 1;
        }
        else if (_card._name == "Choice")
        {
            CardChoice._choice = true;
            _uiChoice.SetActive(true);
            _isChoice = true;
        }
        else if (_card._name == "Skip your turn")
        {
            _skipTurn[SC_PlayerTurn.Instance.turn] = true;
        }
        else if (_card._name == "AllPlayer")
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "River")
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife +=
                        _card._hp;
                if (Stats._zonePlayer[i] == "Desert")
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._gold +=
                        _card._gold;
                if (Stats._zonePlayer[i] == "Mountain")
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife +=
                        _card._hp;
            }
        }
        else if (_card._name == "TradePlayer")
        {
            CardChoice._cardTrade = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
            _isChoice = true;
        }
        else if (_card._name == "Change of zone")
        {
            if (_card._zone == "River")
            {
                if (SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position
                    .GetComponent<Sc_Neighbor>()._neighbor[0].gameObject.CompareTag("Occuped") && SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position
                    .GetComponent<Sc_Neighbor>()._neighbor[2].gameObject.CompareTag("Occuped"))
                {
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= 1;
                    CardChoice.instance.DestroyCard();
                    return;
                }

                _uiChoice.SetActive(true);
                _isChoice = true;
                CardChoice._changeZoneRiver = true;
            }
            else
            {
                ChangeOfZone();
            }
        }
        else if (_card._name == "Donation")
        {
            CardChoice._cardDonation = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
            _isChoice = true;
        }
        else if (_card._name == "Medium")
        {
            CardChoice._medium = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
            _isChoice = true;
        }
        else if (_card._name == "Overflow")
        {
            SC_PlayerTurn.Instance.OverFlow();
        }
        else if (_card._name == "The Horde")
        {
            MoveHorde(_horde);
        }
        else
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._gold += _card._gold;
            AnimationStats._goldAnim += _card._gold;

            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._currentLife += _card._hp;
            AnimationStats._hpAnim += _card._hp;

            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._currentAmmount += _card._bullet;
            AnimationStats._bulletAnim += _card._bullet;
        }
    }

    public void MoveHorde(GameObject theHorde)
    {   // a modifier en fonction de la map.
        if (Zone._turn > 2)
        {
            _theHorde += _card._horde;
            StartCoroutine(MoveHorde(1));
            foreach (var player in SC_PlayerTurn.Instance._player)
            {
                if (player.GetComponent<Sc_getPlayerPosition>()._position.name 
                    != "Start" && player.GetComponent<Sc_getPlayerPosition>()._position.name != "End")
                {
                    int turn = Int32.Parse(player.name[1].ToString());
                    if (Int32.Parse(player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>().name[2].ToString()) <= _theHorde - 1)
                    {
                        Sc_CharacterManager.Instance._playerInfo[turn - 1].GetComponent<Sc_ScriptableReader>()
                        ._gold -= 2;
                        _skipTurn[turn - 1] = true;
                        _move = true;
                        for (int i = 0; i < player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor.Count; i++)
                        {
                            if (Int32.Parse(player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[i].name[2].ToString()) 
                                == Int32.Parse(player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>().name[2].ToString()) + 1 && _move)
                            {
                                if (!player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[i].gameObject.CompareTag("Occuped"))
                                {
                                StartCoroutine(SC_PlayerTurn.Instance.MovePlayer(1f, player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[i].transform.position, player));
                                _move = false;
                                }
                                else
                                {
                                    foreach (var player2 in SC_PlayerTurn.Instance._player)
                                    { 
                                        if (player2.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>().name == player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[i].name)
                                        {
                                            for (int j = 0; j < player2.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor.Count; j++)
                                            {
                                                if (Int32.Parse(player2.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[j].name[2].ToString())
                                                    == Int32.Parse(player2.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>().name[2].ToString()) + 1 && _move)
                                                {
                                                    int turn2 = Int32.Parse(player2.name[1].ToString());
                                                    StartCoroutine(SC_PlayerTurn.Instance.MovePlayer(1f, player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[i].transform.position, player));
                                                    StartCoroutine(SC_PlayerTurn.Instance.MovePlayer(1f, player2.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[j].transform.position, player2));
                                                    _move = false;
                                                    _skipTurn[turn2 - 1] = true;
                                                    Sc_CharacterManager.Instance._playerInfo[turn2 - 1].GetComponent<Sc_ScriptableReader>()._gold -= 2;
                                                }
                                            }                                              
                                        }
                                    }
                                }                                    
                            }
                        }                       
                    }
                }
            }
        }
    }

    private IEnumerator MoveHorde(float total_time)
    {
        float time = 0f;
        float DistEntreDeuxCase = 5.8f;
        Vector3 start_pos = _horde.transform.position;
        Vector3 endpos = _horde.transform.position + new Vector3(DistEntreDeuxCase,0,0);

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            _horde.transform.position = Vector3.Lerp(start_pos, endpos, time / total_time);

            yield return null;
        }
    }

    private void ChangeOfZone()
    {
        if (!SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position
                .GetComponent<Sc_Neighbor>()._neighbor[1].gameObject.CompareTag("Occuped"))
        {
            StartCoroutine(SC_PlayerTurn.Instance.MovePlayer(1f,
                SC_PlayerTurn.Instance.currentPlayer.GetComponent<Sc_getPlayerPosition>()._position
                    .GetComponent<Sc_Neighbor>()._neighbor[1].transform.position,
                SC_PlayerTurn.Instance.currentPlayer));
            Debug.Log("Desert");
        }
        else
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._currentLife += _card._hp;
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                ._currentAmmount += _card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()
                    ._gold += _card._gold;
        }
    }
}