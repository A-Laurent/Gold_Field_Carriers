using TMPro;
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
    public bool _choicePlayer1;
    public bool _choicePlayer2;

    public Card _card;

    public TMP_Text _textChoice1;
    public TMP_Text _textChoice2;
    public TMP_Text _textChoice3;

    public static CardChoice instance;
    private SC_PlayerTurn _playerTurnInstance = SC_PlayerTurn.Instance;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
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
            switch (SC_PlayerTurn.Instance.turn)
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
        if (_cardDonation && !_choicePlayer1 && !_choicePlayer2)
        {
            switch (SC_PlayerTurn.Instance.turn)
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
        else if (_choicePlayer1 || _choicePlayer2)
        {
            switch (SC_PlayerTurn.Instance.turn)
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
            switch (Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
            {
                case "Desert":
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    AnimationStats._bulletAnim += Card._card._bullet;
                    _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                case "River":
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                    AnimationStats._hpAnim += Card._card._hp;
                    _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                case "Mountain":
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    AnimationStats._goldAnim += Card._card._gold;
                    _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
            }
            _card._uiChoice.SetActive(false);
            _choice = false;
            AnimationCard._timer = 15;
        }
        if (_cardTrade)
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
            AnimationStats._bulletAnim += Card._card._bullet;
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
            AnimationStats._hpAnim += Card._card._hp;
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _cardTrade = false;
            AnimationCard._timer = 15;
        }
        if (_cardDonation)
        {
            _cardDonation = false;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            AnimationCard._timer = 15;
            DestroyCard();
        }
        if (_changeZoneRiver)
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "Mountain" && Stats._zonePlayer[i] == Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
                    return;
            }
            Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "Mountain";
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _changeZoneRiver = false;
            AnimationCard._timer = 15;
        }
        if (_choiceDesert || _choiceMountain || _choiceRiver)
        {
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _medium = false;
            _choiceDesert = false;
            _choiceMountain = false;
            AnimationCard._timer = 15;
            _choiceRiver = false;
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
            switch (Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
            {
                case "Desert":
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet + 1;
                    AnimationStats._bulletAnim += Card._card._bullet + 1;

                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                    AnimationStats._hpAnim += Card._card._hp;
                    _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
                case "River":
                    if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                    {
                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp + 1;
                        AnimationStats._hpAnim += Card._card._hp + 1;

                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                        AnimationStats._goldAnim += Card._card._gold;
                    }
                    else
                    {
                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                        AnimationStats._hpAnim += Card._card._hp;
                    }
                    _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
                case "Mountain":
                    if (Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                    {
                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold + 3;
                        AnimationStats._goldAnim += Card._card._gold + 3;

                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                        AnimationStats._bulletAnim += Card._card._bullet;
                    }
                    else
                    {
                        Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                        AnimationStats._goldAnim += Card._card._gold;
                    }
                    _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
            }
            _card._uiChoice.SetActive(false);
            _choice = false;
            AnimationCard._timer = 15;
        }

        //CardTrade
        if (_cardTrade)
        {
            ChoiceTrade1();
            _cardTrade = false;
            AnimationCard._timer = 15;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
        if (_choicePlayer1)
        {
            ChoiceDonation1();
            _cardDonation = false;
            AnimationCard._timer = 15;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _choicePlayer1 = false;
        }
        else if (_cardDonation && !_choicePlayer1)
            _choicePlayer1 = true;

        //CardChangezone River
        if (_changeZoneRiver)
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (Stats._zonePlayer[i] == "Desert" && Stats._zonePlayer[i] == Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
                    return;
            }
            Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "Desert";
            DestroyCard();
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _changeZoneRiver = false;
            AnimationCard._timer = 15;
        }

        //Card Medium
        if (_choiceDesert)
        {
            if (_card._cardDataDesert.Count > 1)
            {
                (_card._cardDataDesert[1], _card._cardDataDesert[0]) = (_card._cardDataDesert[0], _card._cardDataDesert[1]);
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _card._uiChoice.SetActive(false);
            }
            _medium = false;
            _choiceDesert = false;
            AnimationCard._timer = 15;
            DestroyCard();
        }
        if (_choiceRiver)
        {
            if (_card._cardDataRiver.Count > 1)
            {
                (_card._cardDataRiver[1], _card._cardDataRiver[0]) = (_card._cardDataRiver[0], _card._cardDataRiver[1]);
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _card._uiChoice.SetActive(false);
            }
            _medium = false;
            _choiceRiver = false;
            AnimationCard._timer = 15;
            DestroyCard();
        }
        if (_choiceMountain)
        {
            if (_card._cardDataMountain.Count > 1)
            {
                (_card._cardDataMountain[2], _card._cardDataMountain[1]) = (_card._cardDataMountain[1], _card._cardDataMountain[2]);
                _card._uiChoice.SetActive(false);
            }
            else
            {
                _card._uiChoice.SetActive(false);
            }
            _medium = false;
            _choiceMountain = false;
            AnimationCard._timer = 15;
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
            AnimationCard._timer = 15;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
        }
        else if (_choicePlayer2)
        {
            ChoiceDonation2();
            _cardDonation = false;
            AnimationCard._timer = 15;
            _card._uiTrade.SetActive(false);
            _card._uiChoice.SetActive(false);
            _choicePlayer2 = false;
        }
        else if ( _cardDonation && !_choicePlayer2)
            _choicePlayer2 = true;

        else if (_medium)
        {
            _card._uiTrade.SetActive(false);
            _choiceDesert = true;
        }
    }

    public void ChoiceTrade1()
    {
        switch (SC_PlayerTurn.Instance.turn) 
        {
            case 0:
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard(); break;
            case 1:
                Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard(); break;
            case 2:
                Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard(); break;
        }
    }

    public void ChoiceTrade2()
    {
        switch (SC_PlayerTurn.Instance.turn)
        {
            case 0:
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard();
                break;
            case 1:
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard();
                break;
            case 2:
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentLife += Card._card._hp;
                Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= Card._card._hp;
                DestroyCard();
                break;
        }
    }

    public void DestroyCard()
    {
        switch (Stats._zonePlayer[SC_PlayerTurn.Instance.turn])
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
        switch (SC_PlayerTurn.Instance.turn)
        {
            case 0:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    //Stats._bulletPlayer[1] += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                    //Stats._bulletPlayer[Stats._turnPlayer] -= Card._card._bullet;
                }
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 1:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                }
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 2:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                }
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[0].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
        }

    }
    public void ChoiceDonation2()
    {
        switch (SC_PlayerTurn.Instance.turn)
        {
            case 0:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                }   
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 1:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                }
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[2].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
            case 2:
                if (_choicePlayer1 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount > 0)
                {
                    Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._currentAmmount += Card._card._bullet;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentAmmount -= Card._card._bullet;
                }
                else if (_choicePlayer2 && Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold > 4)
                {
                    Sc_CharacterManager.Instance._playerInfo[1].GetComponent<Sc_ScriptableReader>()._gold += Card._card._gold;
                    Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._gold -= Card._card._gold;
                }
                DestroyCard();
                break;
        }

    }
}
