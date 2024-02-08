using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_PlayerTurn : MonoBehaviour
{
    public int turn = 0;
    public int maxPlayer = 3;

    public bool endTurn = false;
    public bool recupPos = true;

    public List<GameObject> _player = new List<GameObject>();
    public List<bool> _canMove = new List<bool>() {true, true, true};
    public GameObject currentPlayer;

    [SerializeField] SC_Graph graph;

    public static SC_PlayerTurn Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerTurnLogic()
    {
        if (_player.Count == maxPlayer)
        {
            switch (turn)
            {
                case 0:
                    if (_canMove[turn] == true)
                    {
                        currentPlayer = _player[0];
                        if (recupPos == true)
                        {
                            graph.RecupPos(_player[turn]);
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            graph.ResetColor();
                            turn++;
                            recupPos = true;
                            endTurn = false;
                        }
                    }
                    else
                        turn++;
                    break;
                case 1:
                    if (_canMove[turn] == true)
                    {
                        currentPlayer = _player[1];
                        if (recupPos == true)
                        {
                            graph.RecupPos(_player[turn]);
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            graph.ResetColor();
                            turn++;
                            recupPos = true;
                            endTurn = false;
                        }
                    }
                    else
                        turn++;
                    break;
                case 2:
                    if (_canMove[turn] == true)
                    {
                        currentPlayer = _player[2];
                        if (recupPos == true)
                        {
                            graph.RecupPos(_player[turn]);
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            graph.ResetColor();
                            turn = 0;
                            recupPos = true;
                            endTurn = false;
                        }
                    }
                    else
                        turn = 0;
                    break;
                default:
                    currentPlayer = null;
                    break;
            }
        }
        for(int i = 0; i < _canMove.Count; i++)
        {
            if (_canMove[0] == false && _canMove[1] == false && _canMove[2] == false)
            {
                turn = 5;
                graph.ResetColor();
            }
        }
    }
    private void Update()
    {
        PlayerTurnLogic();
    }
    public void OverFlow()
    {
        foreach (var player in _player)
        {
            foreach (var sommet in graph._sommets)
            {
                if (player.transform.position == sommet.Obj.transform.position)
                {
                    if (sommet.zone == 1)
                    {
                        int RandNumber = Random.Range(0, 2);

                        if (RandNumber == 0 && sommet.Obj.tag == "Path")
                        {
                            player.transform.position = graph._sommets[sommet.id + 8].Obj.transform.position;
                            sommet.Obj.tag = "Path";
                        }
                        else if (sommet.Obj.tag == "Path")
                        {
                            player.transform.position = graph._sommets[sommet.id - 8].Obj.transform.position;
                            sommet.Obj.tag = "Path";

                        }
                        else
                            Debug.Log("dont move"); 
                    }
                }
            }
        }
    }
}
