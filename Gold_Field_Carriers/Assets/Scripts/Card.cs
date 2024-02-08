using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int _cardIndex = 0;
    public static CardData _card;

    public Text _hpText;
    public Text _bulletText;
    public Text _goldText;
    public Text _hordeText;
    public Text _description;
    public Text _turnText;
    public Text _lineText;
    public Text _nbTurnText;

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
    public bool _enter;

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
        
        _hpText.text = "HP :   " + Stats._hpPlayer[0].ToString() + "    " +
                                   Stats._hpPlayer[1].ToString() + "    " + 
                                   Stats._hpPlayer[2].ToString();
        _bulletText.text = "Bullet :   " + Stats._bulletPlayer[0].ToString() + "    " + 
                                           Stats._bulletPlayer[1].ToString() + "    " + 
                                           Stats._bulletPlayer[2].ToString();
        _hordeText.text = "The Horde : " + _theHorde.ToString();
        _turnText.text = "Turn : Joueur " + (Stats._turnPlayer + 1).ToString();
        _lineText.text = "Line :   " + Zone._line[0].ToString() + "      " 
                                     + Zone._line[1].ToString() + "      " + Zone._line[2].ToString();
        _nbTurnText.text = "Nb turn :  " + Zone._turn.ToString();

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
        if (_card._name != "Choice" && _card._name != "TradePlayer" && _card._name != "Donation" && _card._name != "Medium")
            _cardDataRiver.RemoveAt(_cardIndex);
    } 

    public void EffectCard()
    {
        _description.text = _card._description;
        if (_card._name == "Attack of a bandit" && Stats._bulletPlayer[Stats._turnPlayer] == 0)
        {
            Sc_CharacterManager.Instance._playerInfo[Stats._turnPlayer].GetComponent<Sc_ScriptableReader>()._gold -= 3; 
            AnimationStats._goldAnim -= 3;
            Stats._hpPlayer[Stats._turnPlayer] -= 1; AnimationStats._hpAnim -= 1;
        }
        else if (_card._name == "Choice")
        {
            CardChoice._choice = true;
            _uiChoice.SetActive(true);
        }
        else if (_card._name == "Skip your turn")
        {
            _skipTurn[Stats._turnPlayer] = true;
        }
        else if (_card._name == "AllPlayer")
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "River")
                    Stats._hpPlayer[i] += _card._hp; ;
            }
        }
        else if (_card._name == "TradePlayer")
        {
            CardChoice._cardTrade = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
        }
        else if (_card._name == "Change of zone")
        {
            if (_card._zone == "River")
            {
                if (Zone._line[0] == Zone._line[1] && Zone._line[1] == Zone._line[2])
                {
                    Stats._hpPlayer[Stats._turnPlayer] += _card._hp;
                    return;
                }
                CardChoice._changeZoneRiver = true;
                _uiChoice.SetActive(true);
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
        else if (_card._name == "Donation")
        {
            CardChoice._cardDonation = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
        }
        else if (_card._name == "Medium")
        {
            CardChoice._medium = true;
            _uiChoice.SetActive(true);
            _uiTrade.SetActive(true);
        }
        else if (_card._name == "Overflow")
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "River")
                {
                    int de = Random.Range(0, 1);
                    if (de == 0)
                    {
                        _enter = false;
                        for (int j = 0; j < Stats._nbPlayer; j++)
                        {
                            if (Zone._line[j] == Zone._line[i] && Stats._zonePlayer[j] == "Desert")
                            {
                                _enter = true;
                                Stats._hpPlayer[i] += _card._hp;
                            }                                    
                        }
                        if (!_enter)
                            Stats._zonePlayer[i] = "Desert";
                    }
                    else
                    {
                        _enter = false;
                        for (int j = 0; j < Stats._nbPlayer; j++)
                        {
                            if (Zone._line[j] == Zone._line[i] && Stats._zonePlayer[j] == "Mountain")
                            {
                                _enter = true;
                                Stats._hpPlayer[i] += _card._hp;
                            }
                        }
                        if (!_enter)
                            Stats._zonePlayer[i] = "Mountain";
                    }
                }
            }
        }
        else
        {
            Sc_CharacterManager.Instance._playerInfo[Stats._turnPlayer].GetComponent<Sc_ScriptableReader>()._gold += _card._gold;
            AnimationStats._goldAnim += _card._gold;

            Stats._hpPlayer[Stats._turnPlayer] += _card._hp;
            AnimationStats._hpAnim += _card._hp;

            Stats._bulletPlayer[Stats._turnPlayer] += _card._bullet;
            AnimationStats._bulletAnim += _card._bullet;
            if (Zone._turn > 2)
                _theHorde += _card._horde;
        }
    }
}