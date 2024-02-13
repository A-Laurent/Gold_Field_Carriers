using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SC_MovePlayer : MonoBehaviour
{
    [FormerlySerializedAs("graph")] [SerializeField] Sc_PlayerMovement playerMovement;
     GameObject PlayerBody;
    public SC_PlayerTurn pTurn;


    public bool canMove;
    private Vector3 start_pos, end_pos;

    public static SC_MovePlayer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private IEnumerator CharacterMove(float total_time)
    {
        PlayerBody = pTurn.currentPlayer;
        canMove = false;
        float time = 0f;
        end_pos = playerMovement.SetEndPos();
        start_pos = PlayerBody.transform.position;

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            PlayerBody.transform.position = Vector3.Lerp(start_pos, end_pos,  time / total_time);

            yield return null;
        }
        
        if (PlayerBody.transform.position == end_pos)
        {
            canMove = true;
            pTurn.endTurn = true;//a 

            switch (SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.transform.parent.name)
            {
                case "River" :
                    Zone.SetRiver();
                    Debug.Log("River");
                    break;
                case "Desert" :
                    Zone.SetDesert();
                    Debug.Log("Desert");
                    break;
                case "Mountain" :
                    Zone.SetMountain();
                    Debug.Log("Mountain");
                    break;
                default:
                    Debug.LogError("Failed for find parent by name");
                    break;
            }
        }
    }

    public void StartMoving()
    {
        if(canMove && !AnimationCard._animation)
            StartCoroutine(CharacterMove(1f));
    }
}
