using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_CameraMovement : MonoBehaviour
{
    [Header("Clamping Position")]
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;
    
    [Header("Velocity")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    
    private float _currentSpeed;
    private float _targetSpeed;
    
    [HideInInspector] public float _cameraDirection;
    
    [HideInInspector] public bool _rightDirection = false;
    [HideInInspector] public bool _leftDirection = false;


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
        CameraMovement();
    }

    public void CameraMovement()
    {
        Vector3 movement = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        //Calculation of target speed as a function of camera direction
        if(_rightDirection)
            _targetSpeed = _cameraDirection * _maxSpeed;
        else if(_leftDirection)
            _targetSpeed = (_cameraDirection * -1) * _maxSpeed;

        //Accelaration or Deceleration
        if (_cameraDirection != 0)
        {
            if (_currentSpeed < _targetSpeed)
            {
                _currentSpeed += _acceleration * Time.deltaTime;
            }
            else if (_currentSpeed > _targetSpeed)
            {
                _currentSpeed -= _deceleration * Time.deltaTime;
            }
        }
        else
        {
            _currentSpeed -= _deceleration * Time.deltaTime;
        }
        
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
        
        //Add acceleration to movement
        if (_rightDirection)
        {
            movement = new Vector3(_currentSpeed * Time.deltaTime, 0, 0);
            transform.position += movement;
        }
        else if (_leftDirection)
        {
            movement = new Vector3(-_currentSpeed * Time.deltaTime, 0, 0);
            transform.position += movement;
        }
        
        //Reset Bool
        if (_currentSpeed == 0 || transform.position.x == _maxX || transform.position.x == _minX)
        {
            _rightDirection = false;
            _leftDirection = false;
            _currentSpeed = 0;
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, transform.position.z);
    }
}