using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] Graph graph;
     GameObject PlayerBody;
    [SerializeField] PlayerTurn pTurn;


    public bool canMove;
    private Vector3 start_pos, end_pos;

    private IEnumerator CharacterMove(float total_time)
    {
        foreach(var a in graph._sommets)
        {
            if(a.Obj.transform.position == pTurn.currentPlayer.transform.position)
            {
                a.Obj.tag = "Path";
            }
        }
        PlayerBody = pTurn.currentPlayer;
        canMove = false;
        float time = 0f;
        end_pos = graph.SetEndPos();
        start_pos = PlayerBody.transform.position;

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            PlayerBody.transform.position = Vector3.Lerp(start_pos, end_pos, time / total_time);

            yield return null;
        }
        graph.neighborsSommetPos.Clear();

        if (PlayerBody.transform.position == end_pos)
        {
            graph.CheckOccupedPath();
            canMove = true;
            pTurn.endTurn = true;       //a retirer
        }
    }

    public void StartMoving()
    {
        StartCoroutine(CharacterMove(1f));
    }
}
