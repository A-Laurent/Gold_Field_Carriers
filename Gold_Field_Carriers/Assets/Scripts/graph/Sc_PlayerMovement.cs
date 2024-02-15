using UnityEngine;

public class Sc_PlayerMovement : MonoBehaviour
{
    private float _rayDistance = 100f;
    RaycastHit hit;
    public static bool _end;
    
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
                    Debug.Log(neighbor);
                    if (neighbor == hit.collider.gameObject)
                    {
                        _end = false;
                        if (hit.collider.gameObject.name == "End")
                            _end = true;
                        SC_MovePlayer.Instance.StartMoving();
                        //if(hit.collider.transform.parent.name != SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.transform.parent.name)
                        //GetZonePlayer(hit.collider.gameObject);
                        SC_PlayerTurn.Instance.ClearColor();
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
    
    private void GetZonePlayer(GameObject collider)
    {
        switch (collider.transform.parent.name)
        {
            case "River" :
                Sc_GameManager.Instance.RaiseDecoration(Sc_GameManager.Instance._riverDecoration);
                break;
            case "Desert" :
                Sc_GameManager.Instance.RaiseDecoration(Sc_GameManager.Instance._desertDecoration);
                break;
            case "Mountain" :
                break;
            default:

                break;
        }
    }
}