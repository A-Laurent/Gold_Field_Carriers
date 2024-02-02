using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "My Game/CardData")]
public class CardData : ScriptableObject
{
    public int _gold;
    public int _hp;
    public int _bullet;
    public string _description;
    public Sprite _cardImage;
    public string _name;
    public int _zone;
}
