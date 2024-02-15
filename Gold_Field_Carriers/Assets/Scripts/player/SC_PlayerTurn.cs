using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;

public class SC_PlayerTurn : MonoBehaviour
{
    public int turn = 0;
    public int maxPlayer = 3;

    public bool endTurn = false;
    public bool recupPos = true;


    public List<GameObject> _player = new List<GameObject>();
    public List<GameObject> _stepNeighbor = new List<GameObject>();
    public List<bool> _canMove = new List<bool>() {true, true, true};
    public GameObject currentPlayer;

    [FormerlySerializedAs("graph")] [SerializeField] Sc_PlayerMovement playerMovement;

    public static SC_PlayerTurn Instance;

    [SerializeField] private Material currentMat;
    [SerializeField] private Material newMat;

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
                            StartCoroutine(PLayerBlocked());
                            ChangeColorNeighbors();
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            recupPos = true;
                            endTurn = false;
                        }
                    }
                    
                    break;
                case 1:
                    if (_canMove[turn] == true)
                    {
                        currentPlayer = _player[1];
                        if (recupPos == true)
                        {
                            StartCoroutine(PLayerBlocked());
                            ChangeColorNeighbors();
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            recupPos = true;
                            endTurn = false;
                        }
                    }
                    break;
                case 2:
                    if (_canMove[turn] == true)
                    {
                        currentPlayer = _player[2];
                        if (recupPos == true)
                        {
                            StartCoroutine(PLayerBlocked());
                            ChangeColorNeighbors();
                            recupPos = false;
                        }
                        if (endTurn)
                        {
                            recupPos = true;
                            endTurn = false;
                        }
                    }
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
                Sc_VictoryDefeat._endGame = true;
            }
        }
    }
    private void Update()
    {
        PlayerTurnLogic();
    }

    public IEnumerator MovePlayer(float total_time, Vector3 endpos, GameObject player)
    {
        Debug.Log("Move");
        float time = 0f;
        Vector3 start_pos = player.transform.position;

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(start_pos, endpos,  time / total_time);
            
            yield return null;
        }
    }
    
    
    public void OverFlow()
    {
        foreach (var player in _player)
        {
            if (player.GetComponent<Sc_getPlayerPosition>()._position.transform.parent.name == "River")
            {
                int RandNumber = Random.Range(0, 2);
                int turn = Int32.Parse(player.name[1].ToString());
                Debug.Log("Turn : " + turn);
                Debug.Log("Random Number : " + RandNumber);
                switch (RandNumber)
                {
                    case 0:
                        if (!player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[0].gameObject.CompareTag("Occuped"))
                        {
                            StartCoroutine(MovePlayer(1f, player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[0].transform.position, player));
                        }
                        else
                            Sc_CharacterManager.Instance._playerInfo[turn - 1].GetComponent<Sc_ScriptableReader>()._currentLife -= 1;
                        break;
                    case 1 :
                        if (!player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[2].gameObject.CompareTag("Occuped"))
                        {
                            StartCoroutine(MovePlayer(1f, player.GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[2].transform.position, player));
                        }
                        else
                        {
                            Sc_CharacterManager.Instance._playerInfo[turn - 1].GetComponent<Sc_ScriptableReader>()._currentLife -= 1;    
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private IEnumerator PLayerBlocked()
    {
        if (SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[0].gameObject.tag == "Occuped"
         && SC_PlayerTurn.Instance._player[SC_PlayerTurn.Instance.turn].GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor[1].gameObject.tag == "Occuped")
        {
            Sc_CharacterManager.Instance._playerInfo[SC_PlayerTurn.Instance.turn].GetComponent<Sc_ScriptableReader>()._currentLife -= 1;
            yield return new WaitForSeconds(0.5f);
            Card._skipTurn[turn] = true;
            AnimationCard.Instance.SkipTurn();
            Sc_CharacterManager.Instance.ChangePlayer();
        }
    }


    private void ChangeColorNeighbors()
    {
        ClearColor();
        _stepNeighbor.Clear();
        foreach (var neighbor in _player[turn].GetComponent<Sc_getPlayerPosition>()._position.GetComponent<Sc_Neighbor>()._neighbor)
        {
            if (neighbor.gameObject.tag == "Path")
            {
                _stepNeighbor.Add(neighbor);
                foreach (var stepNeighbor in _stepNeighbor)
                {
                    stepNeighbor.gameObject.GetComponent<MeshRenderer>().material = newMat;
                    stepNeighbor.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255, 255);
                }
            }
        }
    }

    void ClearColor()
    {
        foreach (var stepNeighbor in _stepNeighbor)
        {
            stepNeighbor.gameObject.GetComponent<MeshRenderer>().material = currentMat;
            stepNeighbor.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0);
        }
    }
}
