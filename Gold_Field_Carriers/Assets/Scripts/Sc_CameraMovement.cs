using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_CameraMovement : MonoBehaviour
{
    [Header("Velocity")]
    [SerializeField] private float _speed;
    
    [Header("Clamping")]
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;
    
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

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(_cameraDirection * _speed * Time.deltaTime, 0, 0);
        transform.position += movement;
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, transform.position.z);
    }
}