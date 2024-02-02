using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int _goldPlayer = 25;
    public static int _bulletPlayer = 3;
    public static int _hpPlayer = 3;

    void Update()
    {
        StatLimit();
    }
    public void StatLimit()
    {
        //HP
        if (_hpPlayer > 3)
            _hpPlayer = 3;
        if (_hpPlayer == 0)
        {
            _goldPlayer -= 8;
            _hpPlayer = 1;
        }

        //Bullet
        if (_bulletPlayer > 3)
            _bulletPlayer = 3;
        if (_bulletPlayer < 0)
            _bulletPlayer = 0;

        //Gold
        if (_goldPlayer < 0)
            _goldPlayer = 0;
    }
}
