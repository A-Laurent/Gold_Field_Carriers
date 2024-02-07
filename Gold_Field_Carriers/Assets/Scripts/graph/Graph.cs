using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField] GameObject step;
    [SerializeField] GameObject StartTown;
    [SerializeField] GameObject EndTown;
    [SerializeField] GameObject Line;

    [SerializeField] GameObject GridParent;

    [SerializeField] GameObject Player;

   public List<Sommets> _sommets = new List<Sommets>();
    List<Aretes> _aretes = new List<Aretes>();
    public List<Vector3> neighborsSommetPos = new List<Vector3>();

    [SerializeField] PlayerTurn pTurn;

    public class Sommets
    {
        public int id;
        public int zone;
        public List<Aretes> neighbors;
        public Vector3 StepPos;
        public bool IsOccuped = false;
        public GameObject Obj;

        public Sommets(int id, int zone, Vector3 stepPos, GameObject obj, bool isOccuped = false)
        {
            this.id = id;
            this.zone = zone;
            neighbors = new List<Aretes>();
            StepPos = stepPos;
            IsOccuped = isOccuped;
            Obj = obj;
        }
    }

    public class Aretes
    {
        public Sommets startSommets;
        public Sommets endSommets;

        public Aretes(Sommets start, Sommets end)
        {
            startSommets = start;
            endSommets = end;
        }
    }


    private void InitializeGraph()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject newStep = Instantiate(step, new Vector3(j * 2, i * 2, 0), Quaternion.identity);
                Sommets sommet = new Sommets(i * 8 + j, i, newStep.transform.position, newStep) ;
                _sommets.Add(sommet);

                if (j > 0)
                {
                    GameObject newLine = Instantiate(Line, new Vector3(j * 2 - 1, i * 2, 0), Quaternion.Euler(0, 0, 90));

                    Aretes arete1 = new Aretes(sommet, _sommets[sommet.id - 1]);
                    _aretes.Add(arete1);
                }
                if (i > 0)
                {
                    GameObject newLine = Instantiate(Line, new Vector3(j * 2, i * 2 - 1, 0), Quaternion.identity);

                    if (sommet.id - 8 >= 0)
                    {
                        Aretes arete2 = new Aretes(sommet, _sommets[sommet.id - 8]);
                        _aretes.Add(arete2);
                    }
                }
            }

        }

        //create SpawnPoint
        GameObject _startTown = Instantiate(StartTown, new Vector3(-2, 2, 0), Quaternion.identity);
        Sommets entrySommet = new Sommets(-1, 5, _startTown.transform.position,_startTown);
        _sommets.Add(entrySommet);

        //create Endpoint
        GameObject _endTown = Instantiate(EndTown, new Vector3(16, 2, 0), Quaternion.identity);
        Sommets exitSommet = new Sommets(3 * 8, 5, _endTown.transform.position, _endTown);
        _sommets.Add(exitSommet);

        for(int i = 0; i <  3; i++)
        {
            GameObject newLine = Instantiate(Line,new Vector3(0 - 1,i * 2,0),Quaternion.Euler(0, 0, 90));
            Aretes entryArete = new Aretes(entrySommet, _sommets[i * 8]);
            _aretes.Add(entryArete);
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject newLine = Instantiate(Line, new Vector3(0 + 15, i * 2, 0), Quaternion.Euler(0,0,90));
            Aretes exitArete = new Aretes(_sommets[7 + 8 * i], exitSommet);
            _aretes.Add(exitArete);
        }
        AddNeighbors();
    }

    public void recupPos(GameObject _player)
    {
        UpdateSommetIsOcupped(_player);
        foreach (var arete in _aretes)
        {
            if (arete.endSommets.StepPos == _player.transform.position || arete.startSommets.StepPos == _player.transform.position)
            {
                if (arete.startSommets.IsOccuped == false)
                {
                    neighborsSommetPos.Add(arete.startSommets.StepPos);
                }
                if (arete.endSommets.IsOccuped == false)
                {
                    neighborsSommetPos.Add(arete.endSommets.StepPos);
                }
            }
        }
    }

    public void UpdateSommetIsOcupped(GameObject _player)
    {
        foreach (var arete in _aretes)
        {
            if (arete.startSommets.StepPos == pTurn.currentPlayer.transform.position)
            {
                arete.startSommets.IsOccuped = true;
            }
            else
            {
                arete.startSommets.IsOccuped = false;
            }
        }
    }

    public void CheckOccupedPath()
    {
        foreach (var sommet in _sommets)
        {
            if (sommet.Obj.transform.position == pTurn.currentPlayer.transform.position)
            {
                sommet.Obj.tag = "Occuped";
            }
        }

    }
    void AddNeighbors()
    {
        foreach (var sommet in _sommets)
        {
            foreach (var arete in _aretes)
            {
                if (arete.startSommets == sommet || arete.endSommets == sommet)
                {
                    sommet.neighbors.Add(arete);
                }
            }
        }
        recupPos(pTurn.currentPlayer);
    }

    private void Start()
    {
        InitializeGraph();
        foreach(var sommet in _sommets)
        {
            sommet.Obj.tag = "Path";
        }
    }






    float _rayDistance = 100f;
    [SerializeField] Camera _camera;
    [SerializeField] MovePlayer move;
    RaycastHit2D ballRaycastHit2D;
    public bool CanMove = true;

    void StartRaycast()
    {
        Vector2 _mousePosition = Input.mousePosition;

        Ray cursorToBallRay = _camera.ScreenPointToRay(_mousePosition);

        Debug.DrawRay(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance, Color.yellow);

        ballRaycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }


    void CheckRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ballRaycastHit2D.collider != null)
            {
                if (ballRaycastHit2D.collider.tag == "Path")
                {
                    for (int i = 0; i < neighborsSommetPos.Count; i++)
                    {
                        if (ballRaycastHit2D.transform.position == neighborsSommetPos[i])
                        {
                            if (move.canMove == true)
                            {
                                move.StartMoving();
                            }
                        }
                    }
                }
            }
        }
    }
    public Vector3 SetEndPos()
    {
        return ballRaycastHit2D.transform.position;
    }

    private void Update()
    {
        StartRaycast();
        CheckRay();
    }


    public void TurnPass()
    {
        pTurn.turn++;
    }

}
