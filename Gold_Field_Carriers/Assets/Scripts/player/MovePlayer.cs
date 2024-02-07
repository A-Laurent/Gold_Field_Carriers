using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] Graph graph;
    [SerializeField] GameObject Player;
    [SerializeField] Movement_Raycast move;

    private Vector3 start_pos, end_pos;

    public bool canMove = true;
    private IEnumerator CharacterMove(float total_time)
    {
        canMove = false;
        float time = 0f;
        end_pos = graph.SetEndPos();
        start_pos = Player.transform.position;

        Debug.Log(" start : " + start_pos + " to end : " + end_pos);

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            Player.transform.position = Vector3.Lerp(start_pos, end_pos, time / total_time);

            yield return null;
        }

        graph.neighborsSommetPos.Clear();

        if (Player.transform.position == end_pos)
        {
            graph.recupPos();
            canMove = true;
        }
    }

    public void StartMoving()
    {
        StartCoroutine(CharacterMove(1f));
    }
}
