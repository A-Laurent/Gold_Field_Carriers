using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_testing : MonoBehaviour
{
    [SerializeField] private Transform[] point;
    [SerializeField] private lr_LineController line;


    private void Start()
    {
        line.SetUpLine(point);
    }
}
