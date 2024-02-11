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
    [SerializeField] private float _accelerationRate = 1f;
    [SerializeField] private float _decelerationRate = 2f;
    
    [Header("Clamping")]
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;
    
    [HideInInspector] public float _cameraDirection;
    private float _currentSpeed;

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
        _currentSpeed += _cameraDirection * _accelerationRate * Time.deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, -_speed, _speed);
        
        Vector3 movement = new Vector3(_currentSpeed * Time.deltaTime, 0, 0);
        transform.position += movement;
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), transform.position.y, transform.position.z);
        
        if ((_cameraDirection == 0) && (Mathf.Abs(transform.position.x - _minX) < 0.01f || Mathf.Abs(transform.position.x - _maxX) < 0.01f))
        {
            _currentSpeed = 0f;
        }
        else if (_cameraDirection == 0)
        {
            float deceleration = _decelerationRate * Time.deltaTime;
            if (_currentSpeed > 0)
                _currentSpeed = Mathf.Max(0, _currentSpeed - deceleration);
            else if (_currentSpeed < 0)
                _currentSpeed = Mathf.Min(0, _currentSpeed + deceleration);
        }
    }
}