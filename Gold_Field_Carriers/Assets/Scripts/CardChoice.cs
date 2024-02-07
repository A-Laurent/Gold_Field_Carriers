using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    public static bool _choice;
    public static bool _cardTrade;
    public Card _card;

    public Text _textChoice1;
    public Text _textChoice2;
    public Text _textChoice3;
   public void Choice1()
    {
        if (_choice)
        {
            switch (Stats._zonePlayer[Stats._turnPlayer])
            {
                case "Desert":
                    Stats._bulletPlayer[Stats._turnPlayer] += Card._card._bullet;
                    AnimationStats._bulletAnim += Card._card._bullet;
                    _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                case "River":
                    Stats._hpPlayer[Stats._turnPlayer] += Card._card._hp;
                    AnimationStats._hpAnim += Card._card._hp;
                    _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                case "Mountain":
                    Stats._goldPlayer[Stats._turnPlayer] += Card._card._gold;
                    AnimationStats._goldAnim += Card._card._gold;
                    _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
            }
            _card._uiChoice.SetActive(false);
            _choice = false;
        }
        if (_cardTrade)
        {
            Stats._bulletPlayer[Stats._turnPlayer] += Card._card._bullet;
            AnimationStats._bulletAnim += Card._card._bullet;
            Stats._hpPlayer[Stats._turnPlayer] += Card._card._hp;
            AnimationStats._hpAnim += Card._card._hp;
            switch (Stats._zonePlayer[Stats._turnPlayer])
            {         
                case "Desert":
                    _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                case "River":
                    _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                case "Mountain":
                    _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
            }
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _cardTrade = false;
        }
        
    }
    public void Choice2() 
    {
        if (_choice)
        {
            switch (Stats._zonePlayer[Stats._turnPlayer])
            {
                case "Desert":
                    Stats._bulletPlayer[Stats._turnPlayer] += Card._card._bullet + 1;
                    AnimationStats._bulletAnim += Card._card._bullet + 1;

                    Stats._hpPlayer[Stats._turnPlayer] += Card._card._hp;
                    AnimationStats._hpAnim += Card._card._hp;
                    _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                case "River":
                    if (Stats._goldPlayer[Stats._turnPlayer] > 4)
                    {
                        Stats._hpPlayer[Stats._turnPlayer] += Card._card._hp + 1;
                        AnimationStats._hpAnim += Card._card._hp + 1;

                        Stats._goldPlayer[Stats._turnPlayer] += Card._card._gold;
                        AnimationStats._goldAnim += Card._card._gold;
                    }
                    else
                    {
                        Stats._hpPlayer[Stats._turnPlayer] += Card._card._hp;
                        AnimationStats._hpAnim += Card._card._hp;
                    }
                    _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                case "Mountain":
                    if (Stats._bulletPlayer[Stats._turnPlayer] > 0)
                    {
                        Stats._goldPlayer[Stats._turnPlayer] += Card._card._gold + 3;
                        AnimationStats._goldAnim += Card._card._gold + 3;

                        Stats._bulletPlayer[Stats._turnPlayer] += Card._card._bullet;
                        AnimationStats._bulletAnim += Card._card._bullet;
                    }
                    else
                    {
                        Stats._goldPlayer[Stats._turnPlayer] += Card._card._gold;
                        AnimationStats._goldAnim += Card._card._gold;
                    }
                    _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
            }
            _card._uiChoice.SetActive(false);
            _choice = false;
        }
        if (_cardTrade)
        {
            ChoiceTrad1();
            _cardTrade = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
        
    }

    public void Choice3()
    {
        if (_cardTrade)
        {
            ChoiceTrad2();
            _cardTrade = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
    }

    public void ChoiceTrad1()
    {
        switch (Stats._turnPlayer) 
        {
            case 0:
                Stats._bulletPlayer[1] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[1] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                } break;
            case 1:
                Stats._bulletPlayer[0] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[0] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                } break;
            case 2:
                Stats._bulletPlayer[0] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[0] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                } break;
        }
    }
    public void Update()
    {
        if (_choice)
        {
            _textChoice1.text = "Choice 1";
            _textChoice2.text = "Choice 2";
        }
        if (Stats._turnPlayer == 2 && _cardTrade)
        {
            _textChoice1.text = "Choice 1";
            _textChoice2.text = "Joueur 1";
            _textChoice3.text = "Joueur 2";
        }
        else if (Stats._turnPlayer == 1 && _cardTrade)
        {
            _textChoice1.text = "Choice 1";
            _textChoice2.text = "Joueur 1";
            _textChoice3.text = "Joueur 3";
        }
        else if (Stats._turnPlayer == 0 && _cardTrade)
        {
            _textChoice1.text = "Choice 1";
            _textChoice2.text = "Joueur 2";
            _textChoice3.text = "Joueur 3";
        }
    }

    public void ChoiceTrad2()
    {
        switch (Stats._turnPlayer)
        {
            case 0:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                }
                break;
            case 1:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                }
                break;
            case 2:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                switch (Stats._zonePlayer[Stats._turnPlayer])
                {
                    case "Desert":
                        _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                    case "River":
                        _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                    case "Mountain":
                        _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
                }
                break;
        }
    }
}
