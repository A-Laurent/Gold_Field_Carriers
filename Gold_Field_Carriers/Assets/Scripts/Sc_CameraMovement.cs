using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_CameraMovement : MonoBehaviour
{
    [SerializeField ]private int _speed;
    [HideInInspector] public float _cameraDirection;
    
    public static Sc_CameraMovement Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.Log("There is two CameraMovement Scripts");
        }
    }

    void Update()
    {
        Vector3 movement = new Vector3(_cameraDirection * _speed * Time.deltaTime, 0, 0);
        transform.position += movement;
    }
}