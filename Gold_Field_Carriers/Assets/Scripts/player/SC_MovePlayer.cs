using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_MovePlayer : MonoBehaviour
{
    [SerializeField] SC_Graph graph;
     GameObject PlayerBody;
    [SerializeField] SC_PlayerTurn pTurn;


    public bool canMove;
    private Vector3 start_pos, end_pos;

    private IEnumerator CharacterMove(float total_time)
    {
        foreach (var a in graph._sommets)
        {
            if (a.Obj.transform.position == pTurn.currentPlayer.transform.position)
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
            PlayerBody.transform.position = Vector3.Lerp(start_pos, end_pos,  time / total_time);

            yield return null;
        }

        graph._neighborsSommetPos.Clear();

        if (PlayerBody.transform.position == end_pos)
        {
            graph.CheckOccupedPath();
            graph.EndTownCase(pTurn.currentPlayer);
            graph.DrawCard(pTurn.currentPlayer);
            canMove = true;
        }
    }

    public void StartMoving()
    {
        StartCoroutine(CharacterMove(1f));
    }

    
}
