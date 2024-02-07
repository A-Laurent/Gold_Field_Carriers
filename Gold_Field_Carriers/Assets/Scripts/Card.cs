using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int _cardIndex;
    public static CardData _card;

    public Text _hpText;
    public Text _bulletText;
    public Text _goldText;
    public Text _hordeText;
    public Text _description;
    public Text _turnText;
    public Text _lineText;

    public List<CardData> _cardDataMountain = new();
    public List<CardData> _cardDataRiver = new();
    public List<CardData> _cardDataDesert = new();

    public List<CardData> _cardDataBackupMountain = new();
    public List<CardData> _cardDataBackupRiver = new();
    public List<CardData> _cardDataBackupDesert = new();

    public int _theHorde;
    public GameObject _uiChoice;
    public GameObject _uiTrade;
    public static List<bool> _skipTurn = new();

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
    }
    
    private void Update()
    {
        
        _hpText.text = "HP :   " + Stats._hpPlayer[0].ToString() + "    " +
                                 Stats._hpPlayer[1].ToString() + "    " + 
                                 Stats._hpPlayer[2].ToString();
        _bulletText.text = "Bullet :   " + Stats._bulletPlayer[0].ToString() + "    " + 
                                         Stats._bulletPlayer[1].ToString() + "    " + 
                                         Stats._bulletPlayer[2].ToString();
        _goldText.text = "Gold :   " + Stats._goldPlayer[0].ToString() + "  " + 
                                     Stats._goldPlayer[1].ToString() + "  " + 
                                     Stats._goldPlayer[2].ToString();
        _hordeText.text = "The Horde : " + _theHorde.ToString();
        _turnText.text = "Turn : Joueur " + (Stats._turnPlayer + 1).ToString();
        _lineText.text = "Line :   " + Zone._line[0].ToString() + "      " + Zone._line[1].ToString() + "      " + Zone._line[2].ToString();

        if (Zone._draw && !CardChoice._choice)
        {
            DrawCard();
        }
        Shuffle();
    }
    public void DrawCard()
    {
        switch (Stats._zonePlayer[Stats._turnPlayer])
        {
            case "Desert": Desert(); break;
            case "River": River(); break;
            case "Mountain": Mountain(); break;
        }      
    }

    public void Shuffle()
    {
        if (_cardDataDesert.Count == 0)
            for (int i = 0; i < _cardDataBackupDesert.Count; i++)
            {
                _cardDataDesert.Add(_cardDataBackupDesert[i]);
            }

        if (_cardDataRiver.Count == 0)
            for (int i = 0; i < _cardDataBackupRiver.Count; i++)
            {
                _cardDataRiver.Add(_cardDataBackupRiver[i]);
            }

        if (_cardDataMountain.Count == 0)
            for (int i = 0; i < _cardDataBackupMountain.Count; i++)
            {
                _cardDataMountain.Add(_cardDataBackupMountain[i]);
            }
    }

    public void Desert()
    {
        _cardIndex = Random.Range(0, _cardDataDesert.Count);
        _card = _cardDataDesert[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer")
            _cardDataDesert.RemoveAt(_cardIndex);
    }

    public void Mountain()
    {
        _cardIndex = Random.Range(0, _cardDataMountain.Count);
        _card = _cardDataMountain[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer")
            _cardDataMountain.RemoveAt(_cardIndex);
    }

    public void River()
    {
        _cardIndex = Random.Range(0, _cardDataRiver.Count);
        _card = _cardDataRiver[_cardIndex];
        EffectCard();
        if (_card._name != "Choice" && _card._name != "TradePlayer")
            _cardDataRiver.RemoveAt(_cardIndex);
    } 

    public void EffectCard()
    {
        if (_card._name == "Attack of a bandit" && Stats._bulletPlayer[Stats._turnPlayer] == 0)
        {
            Stats._goldPlayer[Stats._turnPlayer] -= 3; AnimationStats._goldAnim -= 3;
            Stats._hpPlayer[Stats._turnPlayer] -= 1; AnimationStats._hpAnim -= 1;
            _description.text = _card._description;
        }
        else if (_card._name == "Choice")
        {
            CardChoice._choice = true;
            _description.text = _card._description;
            _uiChoice.SetActive(true);
        }
        else if (_card._name == "Skip your turn")
        {
            _skipTurn[Stats._turnPlayer] = true;
            _description.text = _card._description;
        }
        else if (_card._name == "AllPlayer")
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "River")
                    Stats._hpPlayer[i] += _card._hp; ;
            }
            _description.text = _card._description;
        }
        else if (_card._name == "TradePlayer")
        {
            CardChoice._cardTrade = true;
            _description.text = _card._description;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
        }
        else if (_card._name == "Change of zone")
        {
            _description.text = _card._description;
            if (_card._zone == "River")
            {

            }
            else
            {
                for (int i = 0; i < Stats._nbPlayer; i++)
                {
                    if (i != Stats._turnPlayer)
                        if (Zone._line[i] == Zone._line[Stats._turnPlayer] && Stats._zonePlayer[i] == "River")
                        {
                            Stats._hpPlayer[Stats._turnPlayer] += _card._hp;
                            return;
                        }
                }
                Stats._zonePlayer[Stats._turnPlayer] = "River";

            }
        }
        else
        {
            Stats._goldPlayer[Stats._turnPlayer] += _card._gold;
            AnimationStats._goldAnim += _card._gold;

            Stats._hpPlayer[Stats._turnPlayer] += _card._hp;
            AnimationStats._hpAnim += _card._hp;

            Stats._bulletPlayer[Stats._turnPlayer] += _card._bullet;
            AnimationStats._bulletAnim += _card._bullet;

            _theHorde += _card._horde;
            _description.text = _card._description;
        }
    }
}