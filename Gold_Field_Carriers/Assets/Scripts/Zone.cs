using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public static bool draw;
    public void SetDesert()
    {
        Card._zone = "Desert";
        draw = true;
    }

    public void SetRiver()
    {
        Card._zone = "River";
        draw = true;
    }

    public void SetMountain()
    {
        Card._zone = "Mountain";
        draw = true;
    }
}
