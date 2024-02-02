using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_renderer : MonoBehaviour
{
    [SerializeField] private GameObject _circleStep;
    [SerializeField] private GameObject _startTown;
    [SerializeField] private GameObject _endTown;

    int maxStep = 8;
    int Branch = 3;

    int Ite;
    private void Start()
    {
        while (Ite < Branch)
        {
            for (int i = 0; i < Branch; i++)
            {
                for (int j = 0; j < maxStep; j++)
                {
                    GameObject newObject = Instantiate(_circleStep, new Vector2(j * 2, + i * 2), Quaternion.identity);
                }
                Ite++;
            }
        }
    }
}
