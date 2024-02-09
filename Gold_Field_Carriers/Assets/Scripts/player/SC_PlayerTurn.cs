using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SC_PlayerTurn : MonoBehaviour
{
    public int turn = 0;
    public int maxPlayer = 3;

    public bool endTurn = false;
    public bool recupPos = true;

    private bool _mountains = false;
    private bool _desert = false;

    public List<GameObject> _player = new List<GameObject>();
    public List<bool> _canMove = new List<bool>() { true, true, true };
    public GameObject currentPlayer;

    public GameObject ButtonChoice;

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

        for (int i = 0; i < _canMove.Count; i++)
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
        print(turn);
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

    public void ZoneChanged(GameObject _player)
    {
        foreach (var sommet in graph._sommets)
        {
            if (_player.transform.position == sommet.Obj.transform.position)
            {
                switch (sommet.zone)
                {
                    case 0:

                        break;
                    case 1:
                        if (_mountains)
                        {
                            if (graph._sommets[sommet.id + 8].Obj.tag == "Path")
                            {
                                _player.transform.position = graph._sommets[sommet.id + 8].Obj.transform.position;
                                sommet.Obj.tag = "Path";
                                graph._sommets[sommet.id + 8].Obj.tag = "Occuped";
                            }
                        }
                        else if (_desert)
                        {
                            if (graph._sommets[sommet.id - 8].Obj.tag == "Path")
                            {
                                _player.transform.position = graph._sommets[sommet.id - 8].Obj.transform.position;
                                sommet.Obj.tag = "Path";
                                graph._sommets[sommet.id - 8].Obj.tag = "Occuped";
                            }
                        }

                        // else
                        //     Debug.Log("dont move");
                        break;
                    case 2:

                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void SetChoice1()
    {
        if (Card._card._name == "Change of zone")
        {
            _mountains = true;
            ZoneChanged(currentPlayer);
            Debug.Log("Mountains");
            endTurn = true;//a 
            ButtonChoice.SetActive(false);
            AnimationCard._timer = 15;
            CardChoice.instance.DestroyCard();
        }
        else if (Card._card._name == "Medium")
        {
            if (CardChoice.instance._choiceRiver || CardChoice.instance._choiceDesert || CardChoice.instance._choiceMountain)
            {
                AnimationCard._timer = 15;
                endTurn = true;//a 
            }
            CardChoice.instance.Choice1();
        }
        else if (Card._card._name == "Donation")
        {
            CardChoice.instance.Choice1();
            AnimationCard._timer = 15;
            endTurn = true;
        }
        else if (Card._card._name == "Choice")
        {
            CardChoice.instance.Choice1();
            AnimationCard._timer = 15;
            endTurn = true;
        }
        else if (Card._card._name == "TradePlayer")
        {
            CardChoice.instance.Choice1();
            AnimationCard._timer = 15;
            endTurn = true;
        }
    }

    public void SetChoice2()
    {
        if (Card._card._name == "Change of zone")
        {
            _desert = true;
            ZoneChanged(currentPlayer);
            Debug.Log("Desert");
            endTurn = true;//a 
            ButtonChoice.SetActive(false);
            AnimationCard._timer = 15;
            CardChoice.instance.DestroyCard();
        }
        else if (Card._card._name == "Medium")
        {
            if (CardChoice.instance._choiceRiver || CardChoice.instance._choiceDesert || CardChoice.instance._choiceMountain)
            {
                AnimationCard._timer = 15;
                endTurn = true;//a 
            }
            CardChoice.instance.Choice2();
        }
        else if (Card._card._name == "Donation")
        {
            if (CardChoice.instance._choicePlayer1 || CardChoice.instance._choicePlayer2)
            {
                AnimationCard._timer = 15;
                endTurn = true;
            }
            CardChoice.instance.Choice2();
        }
        else if (Card._card._name == "Choice")
        {
            CardChoice.instance.Choice2();
            AnimationCard._timer = 15;
            endTurn = true;
        }
        else if (Card._card._name == "TradePlayer")
        {
            CardChoice.instance.Choice2();
            AnimationCard._timer = 15;
            endTurn = true;
        }
    }

    public void SetChoice3()
    {
        if (Card._card._name == "Medium")
        {
            if (CardChoice.instance._choiceRiver || CardChoice.instance._choiceDesert || CardChoice.instance._choiceMountain)
            {
                AnimationCard._timer = 15;
                endTurn = true;//a 
            }
            CardChoice.instance.Choice3();
        }
        else if (Card._card._name == "Donation")
        {
            if (CardChoice.instance._choicePlayer1 || CardChoice.instance._choicePlayer2)
            {
                AnimationCard._timer = 15;
                endTurn = true;
            }
            CardChoice.instance.Choice3();              
        }
        else if (Card._card._name == "TradePlayer")
        {
            CardChoice.instance.Choice3();
            AnimationCard._timer = 15;
            endTurn = true;
        }
    }
}