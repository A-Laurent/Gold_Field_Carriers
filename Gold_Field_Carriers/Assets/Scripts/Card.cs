using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int _cardIndex;

    public Text _hpText;
    public Text _bulletText;
    public Text _goldText;
    public Text _zoneText;
    public Text _description;

    public List<CardData> _cardDataMountain = new();
    public List<CardData> _cardDataRiver = new();
    public List<CardData> _cardDataDesert = new();

    public List<CardData> _cardDataBackupMountain = new();
    public List<CardData> _cardDataBackupRiver = new();
    public List<CardData> _cardDataBackupDesert = new();

    public static string _zone;
    public int _theHorde;
    public GameObject _uiChoice;

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
    }
    
    private void Update()
    {
        
        _hpText.text = "HP : " + Stats._hpPlayer.ToString();
        _bulletText.text = "Bullet : " + Stats._bulletPlayer.ToString();
        _goldText.text = "Gold : " + Stats._goldPlayer.ToString();
        _zoneText.text = "Zone : " + _theHorde.ToString(); ;

        if (Zone.draw && !CardChoice._choice)
        {
            DrawCard();
            Zone.draw = false;
        }
        else
        {
            Zone.draw = false;
        }
        Shuffle();
    }
    public void DrawCard()
    {
        switch (_zone)
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

        if (_cardDataDesert[_cardIndex]._name == "Attack of a bandit" && Stats._bulletPlayer == 0)
        {
            Stats._goldPlayer -= 3; AnimationStats._goldAnim -= 3;
            Stats._hpPlayer -= 1; AnimationStats._hpAnim -= 1;
            _description.text = _cardDataDesert[_cardIndex]._description;
            _cardDataDesert.RemoveAt(_cardIndex);
        }
        else if (_cardDataDesert[_cardIndex]._name == "Choice")
        {
            CardChoice._choice = true;
            _description.text = _cardDataDesert[_cardIndex]._description;
            _uiChoice.SetActive(true);
        }
        else
        {
            Stats._goldPlayer += _cardDataDesert[_cardIndex]._gold;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._gold;

            Stats._hpPlayer += _cardDataDesert[_cardIndex]._hp;
            AnimationStats._hpAnim += _cardDataDesert[_cardIndex]._hp;

            Stats._bulletPlayer += _cardDataDesert[_cardIndex]._bullet;
            AnimationStats._bulletAnim += _cardDataDesert[_cardIndex]._bullet;

            _theHorde += _cardDataDesert[_cardIndex]._horde;
            _description.text = _cardDataDesert[_cardIndex]._description;
            _cardDataDesert.RemoveAt(_cardIndex);
        }     
    }

    public void Mountain()
    {
        _cardIndex = Random.Range(0, _cardDataMountain.Count);

        if (_cardDataMountain[_cardIndex]._name == "Attack of a bandit" && Stats._bulletPlayer == 0)
        {
            Stats._goldPlayer -= 3; AnimationStats._goldAnim -= 3;
            Stats._hpPlayer -= 1; AnimationStats._hpAnim -= 1;
            _description.text = _cardDataMountain[_cardIndex]._description;
            _cardDataMountain.RemoveAt(_cardIndex);
        }
        else if (_cardDataMountain[_cardIndex]._name == "Choice")
        {
            CardChoice._choice = true;
            _description.text = _cardDataMountain[_cardIndex]._description;
            _uiChoice.SetActive(true);
        }
        else
        {
            Stats._goldPlayer += _cardDataMountain[_cardIndex]._gold;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._gold;

            Stats._hpPlayer += _cardDataMountain[_cardIndex]._hp;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._hp;

            Stats._bulletPlayer += _cardDataMountain[_cardIndex]._bullet;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._bullet;

            _theHorde += _cardDataMountain[_cardIndex]._horde;
            _description.text = _cardDataMountain[_cardIndex]._description;
            _cardDataMountain.RemoveAt(_cardIndex);
        }      
    }

    public void River()
    {
        _cardIndex = Random.Range(0, _cardDataRiver.Count);

        if (_cardDataRiver[_cardIndex]._name == "Attack of a bandit" && Stats._bulletPlayer == 0)
        {
            Stats._goldPlayer -= 3; AnimationStats._goldAnim -= 3;
            Stats._hpPlayer -= 1; AnimationStats._hpAnim -= 1;
            _description.text = _cardDataRiver[_cardIndex]._description;
            _cardDataRiver.RemoveAt(_cardIndex);
        }
        else if (_cardDataRiver[_cardIndex]._name == "Choice")
        {
            CardChoice._choice = true;
            _description.text = _cardDataRiver[_cardIndex]._description;
            _uiChoice.SetActive(true);
        }
        else
        {
            Stats._goldPlayer += _cardDataRiver[_cardIndex]._gold;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._gold;

            Stats._hpPlayer += _cardDataRiver[_cardIndex]._hp;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._hp;

            Stats._bulletPlayer += _cardDataRiver[_cardIndex]._bullet;
            AnimationStats._goldAnim += _cardDataDesert[_cardIndex]._bullet;

            _theHorde += _cardDataRiver[_cardIndex]._horde;
            _description.text = _cardDataRiver[_cardIndex]._description;
            _cardDataRiver.RemoveAt(_cardIndex);
        }
    } 
}