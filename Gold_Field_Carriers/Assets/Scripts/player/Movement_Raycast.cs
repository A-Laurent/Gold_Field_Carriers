using UnityEngine;

public class Movement_Raycast : MonoBehaviour
{
    float _rayDistance = 100f;
    [SerializeField] Camera _camera;
    [SerializeField] MovePlayer move;
    RaycastHit2D ballRaycastHit2D;

    [SerializeField] private Graph graph;

    public bool CanMove = true;

    void startraycast()
    {
        Vector2 _mousePosition = Input.mousePosition;

        Ray cursorToBallRay = _camera.ScreenPointToRay(_mousePosition);
        Debug.DrawRay(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance, Color.yellow);
        ballRaycastHit2D = Physics2D.Raycast(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance);
    }


    void CheckRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ballRaycastHit2D.collider != null && ballRaycastHit2D.collider.tag == "Path")
            {
                if (ballRaycastHit2D.transform.position == graph.neighborsSommetPos[graph.neighborsSommetPos.Count - 1])
                {
                    Debug.Log("monstre");
                }
            }
        }
    }

    private void Update()
    {
        startraycast();
        CheckRay();
    }
}

