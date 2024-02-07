using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{
    public static bool _draw;
    public static List<int> _line = new();
    public static int _turn;

    private void Start()
    {
        for (int i = 0; i < Stats._nbPlayer; i++)
        {
            _line.Add(0);
        }
    }
    public void SetDesert()
    {
        if (!CardChoice._choice && !AnimationCard._animation && Stats._zonePlayer[Stats._turnPlayer] != "Mountain")
        {
            for (int i = 0; i < Stats._nbPlayer;i++)
            {
                if (i != Stats._turnPlayer)
                {
                    if (_line[i] == _line[Stats._turnPlayer] + 1 && Stats._zonePlayer[i] == "Desert" && Stats._zonePlayer[Stats._turnPlayer] == "Desert")
                        return;
                    if ((_line[i] == _line[Stats._turnPlayer] || Stats._zonePlayer[Stats._turnPlayer] == "Start") && Stats._zonePlayer[i] == "Desert")
                        return;
                }
            }
            if (Stats._zonePlayer[Stats._turnPlayer] == "Desert" || Stats._zonePlayer[Stats._turnPlayer] == "Start")
                _line[Stats._turnPlayer] += 1;
            Stats._zonePlayer[Stats._turnPlayer] = "Desert";
            _draw = true;
            if (Stats._turnPlayer == 0)
                _turn += 1;
        }
        
    }

    public void SetRiver()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (i != Stats._turnPlayer)
                {
                    if (_line[i] == _line[Stats._turnPlayer] + 1 && Stats._zonePlayer[i] == "River" && Stats._zonePlayer[Stats._turnPlayer] == "River")
                        return;
                    if ((_line[i] == _line[Stats._turnPlayer] || Stats._zonePlayer[Stats._turnPlayer] == "Start") && Stats._zonePlayer[i] == "River")
                        return;
                }        
            }
            if (Stats._zonePlayer[Stats._turnPlayer] == "River" || Stats._zonePlayer[Stats._turnPlayer] == "Start")
                _line[Stats._turnPlayer] += 1;
            Stats._zonePlayer[Stats._turnPlayer] = "River";
            _draw = true;
            if (Stats._turnPlayer == 0)
                _turn += 1;
        }
    }

    public void SetMountain()
    {
        if (!CardChoice._choice && !AnimationCard._animation && Stats._zonePlayer[Stats._turnPlayer] != "Desert")
        {
            for (int i = 0; i < Stats._nbPlayer; i++)
            {
                if (i != Stats._turnPlayer)
                {
                    if (_line[i] == _line[Stats._turnPlayer] + 1 && Stats._zonePlayer[i] == "Mountain" && Stats._zonePlayer[Stats._turnPlayer] == "Mountain")
                        return;
                    if ((_line[i] == _line[Stats._turnPlayer] || Stats._zonePlayer[Stats._turnPlayer] == "Start") && Stats._zonePlayer[i] == "Mountain")
                        return;
                }
                    
            }
            if (Stats._zonePlayer[Stats._turnPlayer] == "Mountain" || Stats._zonePlayer[Stats._turnPlayer] == "Start")
                _line[Stats._turnPlayer] += 1;
            Stats._zonePlayer[Stats._turnPlayer] = "Mountain";
            _draw = true;
            if (Stats._turnPlayer == 0)
                _turn += 1;
        }
    }
}
