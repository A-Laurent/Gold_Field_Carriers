using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public void SetDesert()
    {
        Card._zone = "Desert";
    }

    public void SetRiver()
    {
        Card._zone = "River";
    }

    public void SetMountain()
    {
        Card._zone = "Mountain";
    }
}
