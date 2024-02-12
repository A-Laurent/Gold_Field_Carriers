using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_getPlayerPosition : MonoBehaviour
{
    public GameObject _position;

    private void OnTriggerEnter(Collider other)
    {
        _position = other.gameObject;
        _position.tag = "Occuped";
    }

    private void OnTriggerExit(Collider other)
    {
        _position.tag = "Path";
    }
}
