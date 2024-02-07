using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public int turn = 0;
    public int maxPlayer = 3;

    public bool endTurn = false;

    public List<GameObject> _player = new List<GameObject>();
    public GameObject currentPlayer;

    [SerializeField] Graph graph;

    public class Player
    {
        public bool CanMove;
        public GameObject PlayerBody;

        public Player(GameObject playerBody, bool canMove = true)
        {
            CanMove = canMove;
            PlayerBody = playerBody;
        }
    }
    public void FirstPlayerTurn()
    {
        if (_player.Count == maxPlayer)
        {
            switch (turn)
            {
                case 0:
                    currentPlayer = _player[0];
                    if (endTurn)
                    {
                        turn++;
                        graph.recupPos(_player[1]);
                        endTurn = false;
                    }
                    break;
                case 1:
                    currentPlayer = _player[1];
                    if (endTurn)
                    {
                        turn++;
                        graph.recupPos(_player[2]);
                        endTurn = false;
                    }
                    break;
                case 2:
                    currentPlayer = _player[2];
                    if (endTurn)
                    {
                        turn = 0;
                        graph.recupPos(_player[0]);
                        endTurn = false;
                    }
                    break;
                default:
                    currentPlayer = null;
                    break;
            }

        }
    }



    private void Update()
    {
        FirstPlayerTurn();
    }

}
