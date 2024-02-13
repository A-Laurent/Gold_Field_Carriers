using UnityEngine;

[CreateAssetMenu]
public class SC_PlayerClass : ScriptableObject
{
    //main Information
    public string _name;
    public string _desc;
    public Sprite _sprite;

    public bool _isAlive = true;
    public bool _isInTheCity = false;

    //stat
    public int _currentLife;
    public int _maxLife = 3;
    public int _minLife = 0;

    public int _currentAmmount;
    public int _maxAmmount;
    public int _minAmmount = 0;

    public int _gold;

    public int _id;

}
