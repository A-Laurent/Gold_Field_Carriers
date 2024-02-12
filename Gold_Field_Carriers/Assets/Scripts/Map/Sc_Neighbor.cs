using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Neighbor : MonoBehaviour
{
    public List<GameObject> _neighbor = new List<GameObject>();
    
    private float _rayDistance = 100f;
    RaycastHit hit;
    public bool CanMove = true;

    private void StartRaycast()
    {
        Vector2 _mousePosition = Input.mousePosition;

        Ray cursorToBallRay = Camera.main.ScreenPointToRay(_mousePosition);

        Debug.DrawRay(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance, Color.yellow);

        Physics.Raycast(cursorToBallRay.origin, cursorToBallRay.direction, out hit, _rayDistance);
    }
    
    // public void RecupPos(GameObject _player)
    // {
    //     UpdateSommetIsOcupped(_player);
    //     foreach (var arete in _aretes)
    //     {
    //         if ((arete.startSommets.StepPos == _player.transform.position |
    //              arete.endSommets.StepPos == _player.transform.position))
    //         {
    //             if (arete.startSommets.IsOccuped == false && arete.startSommets.Obj.tag == "Path")
    //             {
    //                 _neighborsSommetPos.Add(arete.startSommets.StepPos);
    //                 arete.startSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(253, 108, 158, 255);
    //             }
    //
    //             if (arete.endSommets.IsOccuped == false && arete.endSommets.Obj.tag == "Path")
    //             {
    //                 _neighborsSommetPos.Add(arete.endSommets.StepPos);
    //                 arete.endSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(253, 108, 158, 255);
    //             }
    //         }
    //     }
}
