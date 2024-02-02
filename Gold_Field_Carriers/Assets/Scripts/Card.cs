using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int _cardIndex;
    public static int _goldPlayer = 25;
    public static int _bulletPlayer = 3;
    public static int _hpPlayer = 3;
    public Text _hpText;
    public Text _bulletText;
    public Text _goldText;
    public List<CardData> _cardData = new List<CardData>();

    private void Update()
    {
        _hpText.text = "HP : " + _hpPlayer.ToString();
        _bulletText.text = "Bullet : " + _bulletPlayer.ToString();
        _goldText.text = "Gold : " + _goldPlayer.ToString();

        if (Input.GetKeyDown(KeyCode.A) && _cardData.Count != 0)
        {
            _cardIndex = Random.Range(0, _cardData.Count);

            print(_cardData[_cardIndex]);
            print(_cardData[_cardIndex]._gold);
            print(_cardData[_cardIndex]._bullet);
            print(_cardData[_cardIndex]._hp);

            _goldPlayer += _cardData[_cardIndex]._gold;
            _hpPlayer += _cardData[_cardIndex]._hp;
            _bulletPlayer += _cardData[_cardIndex]._bullet;

            if (_hpPlayer > 3) _hpPlayer = 3;
            if (_hpPlayer == 0)
            {
                _goldPlayer -= 8;
                _hpPlayer = 1;
            }

            if (_bulletPlayer > 3) _bulletPlayer = 3;
            if (_bulletPlayer < 0) _bulletPlayer = 0;

            if (_goldPlayer < 0) _goldPlayer = 0;

            _cardData.RemoveAt(_cardIndex);
        }
    }
}