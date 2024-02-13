using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int _cardIndex = 0;
    public static CardData _card;

    private SC_PlayerTurn _playerTurnInstance = SC_PlayerTurn.Instance;

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
    public bool _enter;
    public static Card Instance;
    public static bool _isChoice;

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
        for (int i  = 0; i < _cardDataDesert.Count; i++)
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
            Zone._draw = false;
        }
        Shuffle();
    }
    public void DrawCard()
    {
        switch (Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
        {
            case "Desert": Desert(); break;
            case "River": River(); break;
            case "Mountain": Mountain(); break;
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
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" && _card._name != "Medium")
            _cardDataDesert.RemoveAt(_cardIndex);
    }

    public void Mountain()
    {
        _card = _cardDataMountain[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" && _card._name != "Medium")
            _cardDataMountain.RemoveAt(_cardIndex);
    }

    public void River()
    {
        _card = _cardDataRiver[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" && _card._name != "Medium" && _card._name != "Change of zone")
            _cardDataRiver.RemoveAt(_cardIndex);
    } 

    // ReSharper disable Unity.PerformanceAnalysis
    public void EffectCard()
    {
        //_description.text = _card._description;
        if (_card._name == "Attack of a bandit" && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount == 0)
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= 3; 
            AnimationStats._goldAnim -= 3;
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= 1; AnimationStats._hpAnim -= 1;
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
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife += _card._hp;
                if (Stats._zonePlayer[i] == "Desert")
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._gold += _card._gold;
                if (Stats._zonePlayer[i] == "Mountain")
                    Sc_CharacterManager.Instance._playerInfo[i].GetComponent<Sc_ScriptableReader>()._currentLife += _card._hp;
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
                _uiChoice.SetActive(true);
                _isChoice = true;
            }
            else
            {
                //SC_PlayerTurn.Instance.OverFlow();
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
            //SC_PlayerTurn.Instance.OverFlow();
        }
        else
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += _card._gold;
            AnimationStats._goldAnim += _card._gold;

            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += _card._hp;
            AnimationStats._hpAnim += _card._hp;

            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount += _card._bullet;
            AnimationStats._bulletAnim += _card._bullet;
            if (Zone._turn > 2)
                _theHorde += _card._horde;
        }
    }
}