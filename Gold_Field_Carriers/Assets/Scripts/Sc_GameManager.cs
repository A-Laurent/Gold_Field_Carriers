using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _riverDecoration = new List<GameObject>();
    [SerializeField] private List<GameObject> _desertDecoration = new List<GameObject>();
    
    void Start()
    {
        RaiseDecoration(_riverDecoration);
        StartCoroutine(Raise());
    }
    
    void Update()
    {
        
    }

    public void RaiseDecoration(List<GameObject> zonedecoration)
    {
        foreach (var decoration in zonedecoration)
        {
            decoration.GetComponent<Animation>().Play("Raise");
        }
    }
    
    public void LowerDecoration(List<GameObject> zonedecoration)
    {
        foreach (var decoration in zonedecoration)
        {
            decoration.GetComponent<Animation>().Play("Lower");
        }
    }

    IEnumerator Raise()
    {
        yield return new WaitForSeconds(3f);
        LowerDecoration(_riverDecoration);
    }
}
