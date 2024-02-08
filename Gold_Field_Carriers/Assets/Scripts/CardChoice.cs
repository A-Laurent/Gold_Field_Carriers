using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    public static bool _choice;
    public static bool _cardTrade;
    public static bool _cardDonation;
    public static bool _changeZoneRiver;
    public static bool _medium;

    public bool _choiceDesert;
    public bool _choiceMountain;
    public bool _choiceRiver;
    public bool _choiceBullet;
    public bool _choiceGold;

    public Card _card;

    public Text _textChoice1;
    public Text _textChoice2;
    public Text _textChoice3;

    public void Update()
    {
        if (_medium)
        {
            if (_choiceDesert && _card._cardDataDesert.Count > 1)
            {
                _textChoice1.text = "First : " + _card._cardDataDesert[0]._description;
                _textChoice2.text = "First : " + _card._cardDataDesert[1]._description;
            }
            else if (_choiceRiver && _card._cardDataRiver.Count > 1)
            {
                _textChoice1.text = "First : " + _card._cardDataRiver[0]._description;
                _textChoice2.text = "First : " + _card._cardDataRiver[1]._description;
            }
            else if (_choiceMountain && _card._cardDataMountain.Count > 1)
            {
                _textChoice1.text = "First : " + _card._cardDataMountain[1]._description;
                _textChoice2.text = "First : " + _card._cardDataMountain[2]._description;
            }
            else
            {
                _textChoice1.text = "Mountain";
                _textChoice2.text = "River";
                _textChoice3.text = "Desert";
            }       
        }
        if (_changeZoneRiver)
        {
            _textChoice1.text = "Mountain";
            _textChoice2.text = "Desert";
        }
        if (_choice)
        {
            _textChoice1.text = "Choice 1";
            _textChoice2.text = "Choice 2";
        }
        if (_cardTrade)
        {
            switch (Stats._turnPlayer)
            {
                case 0:
                    _textChoice1.text = "Choice 1";
                    _textChoice2.text = "Joueur 2";
                    _textChoice3.text = "Joueur 3"; break;

                case 1:
                    _textChoice1.text = "Choice 1";
                    _textChoice2.text = "Joueur 1";
                    _textChoice3.text = "Joueur 3"; break;

                case 2:
                    _textChoice1.text = "Choice 1";
                    _textChoice2.text = "Joueur 1";
                    _textChoice3.text = "Joueur 2"; break;
            }
        }
        if (_cardDonation && !_choiceBullet && !_choiceGold)
        {
            switch (Stats._turnPlayer)
            {
                case 0:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "Joueur 2";
                    _textChoice3.text = "Joueur 3"; break;

                case 1:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "Joueur 1";
                    _textChoice3.text = "Joueur 3"; break;

                case 2:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "Joueur 1";
                    _textChoice3.text = "Joueur 2"; break;
            }
        }
        else if (_choiceBullet || _choiceGold)
        {
            switch (Stats._turnPlayer)
            {
                case 0:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "1 Bullet";
                    _textChoice3.text = "5 Gold"; break;

                case 1:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "1 Bullet";
                    _textChoice3.text = "5 Gold"; break;

                case 2:
                    _textChoice1.text = "No donation";
                    _textChoice2.text = "1 Bullet";
                    _textChoice3.text = "5 Gold"; break;
            }
        }
    }
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
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _cardTrade = false;
        }
        if (_cardDonation)
        {
            _cardDonation = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            DestroyCard();
        }
        if (_changeZoneRiver)
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "Mountain" && Stats._zonePlayer[i] == Stats._zonePlayer[Stats._turnPlayer])
                    return;
            }
            Stats._zonePlayer[Stats._turnPlayer] = "Mountain";
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _changeZoneRiver = false;
        }
        if (_choiceDesert || _choiceMountain || _choiceRiver)
        {
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _changeZoneRiver = false;
            DestroyCard();
        }
        if (_medium)
        {
            _card._uiTrade.SetActive(false);
            _choiceMountain = true;
        }
    }
    public void Choice2() 
    {
        //Card Choice
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

        //CardTrade
        if (_cardTrade)
        {
            ChoiceTrade1();
            _cardTrade = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
        if (_choiceBullet)
        {
            ChoiceDonation1();
            _cardDonation = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _choiceBullet = false;
        }
        else if (_cardDonation && !_choiceBullet)
            _choiceBullet = true;

        //CardChangezone River
        if (_changeZoneRiver)
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "Desert" && Stats._zonePlayer[i] == Stats._zonePlayer[Stats._turnPlayer])
                    return;
            }
            Stats._zonePlayer[Stats._turnPlayer] = "Desert";
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _changeZoneRiver = false;
        }

        //Card Medium
        if (_choiceDesert)
        {
            if (_card._cardDataDesert.Count > 1)
            {
                (_card._cardDataDesert[1], _card._cardDataDesert[0]) = (_card._cardDataDesert[0], _card._cardDataDesert[1]);
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            _choiceDesert = false;
            DestroyCard();
        }
        if (_choiceRiver)
        {
            if (_card._cardDataRiver.Count > 1)
            {
                (_card._cardDataRiver[1], _card._cardDataRiver[0]) = (_card._cardDataRiver[0], _card._cardDataRiver[1]);
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            _choiceRiver = false;
            DestroyCard();
        }
        if (_choiceMountain)
        {
            if (_card._cardDataMountain.Count > 1)
            {
                (_card._cardDataMountain[2], _card._cardDataMountain[1]) = (_card._cardDataMountain[1], _card._cardDataMountain[2]);
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _medium = false;
                _card._uiChoice.SetActive(false);
            }
            _choiceMountain = false;
            DestroyCard();
        }

        if (_medium)
        {
            _card._uiTrade.SetActive(false);
            _choiceRiver = true;
        }
    }

    public void Choice3()
    {
        if (_cardTrade)
        {
            ChoiceTrade2();
            _cardTrade = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
        if (_choiceGold)
        {
            ChoiceDonation2();
            _cardDonation = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _choiceGold = false;
        }
        else if ( _cardDonation && !_choiceGold)
            _choiceGold = true;

        if (_medium)
        {
            _card._uiTrade.SetActive(false);
            _choiceDesert = true;
        }
    }

    public void ChoiceTrade1()
    {
        switch (Stats._turnPlayer) 
        {
            case 0:
                Stats._bulletPlayer[1] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[1] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard(); break;
            case 1:
                Stats._bulletPlayer[0] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[0] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard(); break;
            case 2:
                Stats._bulletPlayer[0] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[0] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard(); break;
        }
    }

    public void ChoiceTrade2()
    {
        switch (Stats._turnPlayer)
        {
            case 0:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard();
                break;
            case 1:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard();
                break;
            case 2:
                Stats._bulletPlayer[2] += Card._card._bullet;
                Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                Stats._hpPlayer[2] += Card._card._hp;
                Stats._hpPlayer[Stats._turnPlayer] -= Card._card._hp;
                DestroyCard();
                break;
        }
    }

    public void DestroyCard()
    {
        switch (Stats._zonePlayer[Stats._turnPlayer])
        {
            case "Desert":
                _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
            case "River":
                _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
            case "Mountain":
                _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
        }
    }

    public void ChoiceDonation1()
    {
        switch (Stats._turnPlayer)
        {
            case 0:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[1] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[1] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 1:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[0] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[0] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 2:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[0] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[0] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
        }

    }
    public void ChoiceDonation2()
    {
        switch (Stats._turnPlayer)
        {
            case 0:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[2] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }   
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[2] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 1:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[2] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[2] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 2:
                if (_choiceBullet && Stats._bulletPlayer[Stats._turnPlayer] > 0)
                {
                    Stats._bulletPlayer[1] += Card._card._bullet;
                    Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choiceGold && Stats._goldPlayer[Stats._turnPlayer] > 4)
                {
                    Stats._goldPlayer[1] += Card._card._gold;
                    Stats._goldPlayer[Stats._turnPlayer] -= Card._card._gold;
                }
                DestroyCard();
                break;
        }

    }
}
