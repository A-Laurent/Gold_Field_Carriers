using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoice : MonoBehaviour
{
    public static bool _choice;
    public Card _card;
   public void Choice1()
    {
        switch (Card._zone)
        {
            case "Desert":
                Stats._bulletPlayer += _card._cardDataDesert[_card._cardIndex]._bullet;
                _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
            case "River":
                Stats._hpPlayer += _card._cardDataRiver[_card._cardIndex]._hp;
                _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
            case "Mountain":
                Stats._goldPlayer += _card._cardDataMountain[_card._cardIndex]._gold;
                _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
        }
        _card._uiChoice.SetActive(false);
        _choice = false;
    }
    public void Choice2() 
    {
        switch (Card._zone)
        {
            case "Desert":
                Stats._bulletPlayer += _card._cardDataDesert[_card._cardIndex]._bullet + 1;
                AnimationStats._goldAnim += _card._cardDataDesert[_card._cardIndex]._bullet + 1;

                Stats._hpPlayer += _card._cardDataDesert[_card._cardIndex]._hp;
                AnimationStats._goldAnim += _card._cardDataDesert[_card._cardIndex]._hp;
                _card._cardDataDesert.RemoveAt(_card._cardIndex); break;
            case "River":
                if (Stats._goldPlayer > 4)
                {
                    Stats._hpPlayer += _card._cardDataRiver[_card._cardIndex]._hp + 1;
                    AnimationStats._goldAnim += _card._cardDataRiver[_card._cardIndex]._hp + 1;

                    Stats._goldPlayer += _card._cardDataRiver[_card._cardIndex]._gold;
                    AnimationStats._goldAnim += _card._cardDataRiver[_card._cardIndex]._gold;
                }
                else
                {
                    Stats._hpPlayer += _card._cardDataRiver[_card._cardIndex]._hp;
                    AnimationStats._goldAnim += _card._cardDataRiver[_card._cardIndex]._hp;
                }
                _card._cardDataRiver.RemoveAt(_card._cardIndex); break;
            case "Mountain":
                if (Stats._bulletPlayer > 0)
                {
                    Stats._goldPlayer += _card._cardDataMountain[_card._cardIndex]._gold + 3;
                    AnimationStats._goldAnim += _card._cardDataMountain[_card._cardIndex]._gold + 3;

                    Stats._bulletPlayer += _card._cardDataMountain[_card._cardIndex]._bullet;
                    AnimationStats._goldAnim += _card._cardDataMountain[_card._cardIndex]._bullet;
                }
                else
                {
                    Stats._goldPlayer += _card._cardDataMountain[_card._cardIndex]._gold;
                    AnimationStats._goldAnim += _card._cardDataMountain[_card._cardIndex]._gold;
                }
                _card._cardDataMountain.RemoveAt(_card._cardIndex); break;
        }
        _card._uiChoice.SetActive(false);
        _choice = false;
    }
}
