using System.Collections.Generic;
using UnityEngine;

public class SC_Graph : MonoBehaviour
{

    [SerializeField] GameObject step;
    [SerializeField] GameObject StartTown;
    [SerializeField] GameObject EndTown;
    [SerializeField] GameObject Line;

    [SerializeField] SC_PlayerTurn pTurn;
    [SerializeField] SC_MovePlayer move;

    public List<Sommets> _sommets = new List<Sommets>();
    public List<Vector3> _neighborsSommetPos = new List<Vector3>();
    private List<Aretes> _aretes = new List<Aretes>();

    private GameObject endTown;

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
                newStep.gameObject.tag = "Path";
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
        GameObject startTown = Instantiate(StartTown, new Vector3(-2, 2, 0), Quaternion.identity);
        Sommets entrySommet = new Sommets(-1, 5, startTown.transform.position,startTown);
        _sommets.Add(entrySommet);
        for(int i = 0; i <  3; i++)
        {
            Aretes entryArete = new Aretes(entrySommet, _sommets[i * 8]);
            _aretes.Add(entryArete);
        }

        //create Endpoint
        endTown = Instantiate(EndTown, new Vector3(16, 2, 0), Quaternion.identity);
        Sommets exitSommet = new Sommets(3 * 8, 5, endTown.transform.position, endTown);
        _sommets.Add(exitSommet);
        for (int i = 0; i < 3; i++)
        {
            Aretes exitArete = new Aretes(_sommets[7 + 8 * i], exitSommet);
            _aretes.Add(exitArete);
        }
        AddNeighbors();
    }

    public void RecupPos(GameObject _player)
    {
        UpdateSommetIsOcupped(_player);
        foreach (var arete in _aretes)
        {
            if ((arete.startSommets.StepPos == _player.transform.position | arete.endSommets.StepPos == _player.transform.position))
            {
                if (arete.startSommets.IsOccuped == false && arete.startSommets.Obj.tag == "Path")
                {
                    _neighborsSommetPos.Add(arete.startSommets.StepPos);
                    arete.startSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(253, 108, 158, 255);
                }
            
                if (arete.endSommets.IsOccuped == false && arete.endSommets.Obj.tag == "Path")
                {
                    _neighborsSommetPos.Add(arete.endSommets.StepPos);
                    arete.endSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(253, 108, 158, 255);
                }
            }
        }
    }

    public void ResetColor()
    {
        foreach (var arete in _aretes)
        {
            arete.endSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            arete.startSommets.Obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }

    private void UpdateSommetIsOcupped(GameObject _player)
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
            if (sommet.Obj.transform.position == pTurn.currentPlayer.transform.position && sommet.Obj.transform.position != _sommets[_sommets.Count - 1].Obj.transform.position)
            {
                sommet.Obj.tag = "Occuped";
            }
        }
    }

    public void EndTownCase(GameObject _Player)
    {
        if (_sommets[_sommets.Count - 1].Obj.transform.position == _Player.transform.position)
        {
            pTurn.endTurn = true;
            pTurn._canMove[pTurn.turn] = false;
        }
    }


    private void AddNeighbors()
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
        RecupPos(pTurn.currentPlayer);
    }

    private void Start()
    {
        InitializeGraph();
    }


    private float _rayDistance = 100f;
    [SerializeField] Camera _camera;
    RaycastHit2D ballRaycastHit2D;
    public bool CanMove = true;

    private void StartRaycast()
    {
        Vector2 _mousePosition = Input.mousePosition;

        Ray cursorToBallRay = _camera.ScreenPointToRay(_mousePosition);

        Debug.DrawRay(cursorToBallRay.origin, cursorToBallRay.direction * _rayDistance, Color.yellow);

        ballRaycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }


    private void CheckRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ballRaycastHit2D.collider != null)
            {
                if (ballRaycastHit2D.collider.tag == "Path")
                {
                    for (int i = 0; i < _neighborsSommetPos.Count; i++)
                    {
                        if (ballRaycastHit2D.transform.position == _neighborsSommetPos[i])
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
}
