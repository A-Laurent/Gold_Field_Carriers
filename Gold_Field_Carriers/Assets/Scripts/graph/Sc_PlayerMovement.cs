using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerMovement : MonoBehaviour
{
    private float _rayDistance = 100f;
    RaycastHit hit;
    
    private void StartRaycast()
    {
        Vector2 _mousePosition = Input.mousePosition;

        Ray cursorToBallRay = Camera.main.ScreenPointToRay(_mousePosition);

        Debug.DrawRay(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance, Color.yellow);

        Physics.Raycast(cursorToBallRay.origin, cursorToBallRay.direction, out hit, _rayDistance);
    }


    private void CheckRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Path")
            {
                //Debug.Log(hit.collider.name);
                foreach (var neighbor in SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor)
                {
                    if (neighbor == hit.collider.gameObject)
                    {
                        SC_MovePlayer.Instance.StartMoving();
                        break;
                    }
                }
            }
        }
    }

    public Vector3 SetEndPos()
    {
        return hit.transform.position;
    }

    private void Update()
    {
        StartRaycast();
        CheckRay();
    }
}