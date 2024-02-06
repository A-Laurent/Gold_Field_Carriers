using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static List<int> _goldPlayer = new();
    public static List<int> _bulletPlayer = new();
    public static List<int> _hpPlayer = new();

    public static int _turnPlayer = 0;
    public static int _nbPlayer = 3;

    private void Start()
    {
        for (int i = 0; i < _nbPlayer; i++)
        {
            _goldPlayer.Add(25);
            _bulletPlayer.Add(2);
            _hpPlayer.Add(3);
        }
    }
    void Update()
    {
        StatLimit();
    }
    public void StatLimit()
    {
        //HP
        if (_hpPlayer[_turnPlayer] > 3)
            _hpPlayer[_turnPlayer] = 3;
        if (_hpPlayer[_turnPlayer] == 0)
        {
            _goldPlayer[_turnPlayer] -= 8;
            _hpPlayer[_turnPlayer] = 1;
        }

        //Bullet
        if (_bulletPlayer[_turnPlayer] > 3)
            _bulletPlayer[_turnPlayer] = 3;
        if (_bulletPlayer[_turnPlayer] < 0)
            _bulletPlayer[_turnPlayer] = 0;

        //Gold
        if (_goldPlayer[_turnPlayer] < 0)
            _goldPlayer[_turnPlayer] = 0;
    }
}
