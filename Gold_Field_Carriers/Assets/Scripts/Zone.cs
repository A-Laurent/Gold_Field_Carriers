using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public static bool _draw;
    public void SetDesert()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            Card._zone = "Desert";
            _draw = true;
        }
        
    }

    public void SetRiver()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            Card._zone = "River";
            _draw = true;
        }
    }

    public void SetMountain()
    {
        if (!CardChoice._choice && !AnimationCard._animation)
        {
            Card._zone = "Mountain";
            _draw = true;
        }
    }
}
