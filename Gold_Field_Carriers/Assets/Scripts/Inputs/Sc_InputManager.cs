using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public void CameraMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sc_CameraMovement.Instance._cameraDirection = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            Sc_CameraMovement.Instance._cameraDirection = 0;
        }
    }
}
