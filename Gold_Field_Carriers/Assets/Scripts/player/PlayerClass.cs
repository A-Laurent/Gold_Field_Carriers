using UnityEngine;

[CreateAssetMenu]
public class PlayerClass : ScriptableObject
{
    //main Information
    public string PlayerName;
    public string Description;
    public Sprite Image;

    public bool IsAlive = true;

    //stat
    public int CurrentLife;
    public int MaxLife = 3;
    private int _minLife = 0;

    public int CurrentAmmount;
    public int MaxAmmount;
    private int _minAmmount = 0;

    public int Gold;

}
