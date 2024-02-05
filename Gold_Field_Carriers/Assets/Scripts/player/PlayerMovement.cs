using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    float Speed = 5f;
    Vector2 Target;
    public bool playerTurn = true;

    private void Start()
    {
        Target = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && playerTurn == true)
        {
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Target);
            playerTurn = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }
}
