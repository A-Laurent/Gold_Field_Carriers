using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public static bool _draw;
    public static int _turn = 1;
    public void SetDesert()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            if (Card._zone == "Desert")
                _turn += 1;
            Card._zone = "Desert";
            _draw = true;
        }
        
    }

    public void SetRiver()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            if (Card._zone == "River")
                _turn += 1;
            Card._zone = "River";
            _draw = true;
        }
    }

    public void SetMountain()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            if (Card._zone == "Mountain")
                _turn += 1;
            Card._zone = "Mountain";
            _draw = true;
        }
    }
}
