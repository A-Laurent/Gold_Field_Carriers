using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_getPlayerPosition : MonoBehaviour
{
    public GameObject _position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "P1" || other.name == "P2" || other.name == "P3")
            return;
        _position = other.gameObject;
        if (_position.name != "End")
            _position.tag = "Occuped";
    }

    private void OnTriggerExit(Collider other)
    {
        _position.tag = "Path";
    }
}
