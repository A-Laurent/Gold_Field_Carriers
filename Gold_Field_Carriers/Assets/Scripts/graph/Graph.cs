using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Graph : MonoBehaviour
{

    List<Sommets> _sommets = new List<Sommets>();
    List<Aretes> _aretes = new List<Aretes>();

    class Aretes
    {
        public char _origine;
        public List<char> _extremite;

        public Aretes(char origine, List<char> extremite)
        {
            _origine = origine;
            _extremite = extremite;
        }
    }

    class Sommets
    {
        public char _ID;
        public char _Zone;
        public bool _Occuped;

        public Sommets(char ID, char Zone, bool Occuped = false)
        {
            _ID = ID;
            _Zone = Zone;
            _Occuped = Occuped;
        }
    }

    private void AddSommet()
    {                         // 'ID 'Zone'
        _sommets.Add(new Sommets('A', 'D')); //Start town            //  D = départ
                                                                      
        _sommets.Add(new Sommets('B', 'M'));                         //  M = montagne
        _sommets.Add(new Sommets('C', 'R'));                         //  R = riviere
        _sommets.Add(new Sommets('D', 'D'));                         //  D = desert
                                            
        _sommets.Add(new Sommets('E', 'M'));                         //  F = Fin
        _sommets.Add(new Sommets('F', 'R'));
        _sommets.Add(new Sommets('G', 'D'));
                                            
        _sommets.Add(new Sommets('H', 'M'));
        _sommets.Add(new Sommets('I', 'R'));
        _sommets.Add(new Sommets('J', 'D'));
                                            
        _sommets.Add(new Sommets('K', 'M'));
        _sommets.Add(new Sommets('L', 'R'));
        _sommets.Add(new Sommets('M', 'D'));
                                            
        _sommets.Add(new Sommets('N', 'M'));
        _sommets.Add(new Sommets('O', 'R'));
        _sommets.Add(new Sommets('P', 'D'));
                                            
        _sommets.Add(new Sommets('Q', 'M'));
        _sommets.Add(new Sommets('R', 'R'));
        _sommets.Add(new Sommets('S', 'D'));
                                            
        _sommets.Add(new Sommets('T', 'M'));
        _sommets.Add(new Sommets('U', 'R'));
        _sommets.Add(new Sommets('V', 'D'));
                                            
        _sommets.Add(new Sommets('W', 'M'));
        _sommets.Add(new Sommets('X', 'R'));
        _sommets.Add(new Sommets('Y', 'D'));

        _sommets.Add(new Sommets('Z', 'F'));     //End town
    }

    void AddArete()
    {
        _aretes.Add(new Aretes('A', new List<char> {'B', 'C', 'D'}));       //Start town

        _aretes.Add(new Aretes('B', new List<char> { 'A', 'C', 'E'}));
        _aretes.Add(new Aretes('C', new List<char> { 'B', 'A', 'D', 'F' }));
        _aretes.Add(new Aretes('D', new List<char> { 'C', 'A', 'G' }));

        _aretes.Add(new Aretes('E', new List<char> { 'B', 'F', 'H' }));
        _aretes.Add(new Aretes('F', new List<char> { 'E', 'C', 'G', 'I' }));
        _aretes.Add(new Aretes('G', new List<char> { 'F', 'D', 'J' }));

        _aretes.Add(new Aretes('H', new List<char> { 'E', 'I', 'K' }));
        _aretes.Add(new Aretes('I', new List<char> { 'H', 'F', 'J', 'L' }));
        _aretes.Add(new Aretes('J', new List<char> { 'I', 'G', 'M' }));

        _aretes.Add(new Aretes('K', new List<char> { 'H', 'L', 'N' }));
        _aretes.Add(new Aretes('L', new List<char> { 'K', 'I', 'M', 'O' }));
        _aretes.Add(new Aretes('M', new List<char> { 'L', 'J', 'P' }));

        _aretes.Add(new Aretes('N', new List<char> { 'K', 'O', 'Q' }));
        _aretes.Add(new Aretes('O', new List<char> { 'N', 'L', 'P', 'R' }));
        _aretes.Add(new Aretes('P', new List<char> { 'O', 'M', 'S' }));

        _aretes.Add(new Aretes('Q', new List<char> { 'N', 'R', 'T' }));
        _aretes.Add(new Aretes('R', new List<char> { 'Q', 'O', 'S', 'U' }));
        _aretes.Add(new Aretes('S', new List<char> { 'R', 'P', 'V' }));

        _aretes.Add(new Aretes('T', new List<char> { 'Q', 'U', 'W' }));
        _aretes.Add(new Aretes('U', new List<char> { 'T', 'R', 'V', 'X' }));
        _aretes.Add(new Aretes('V', new List<char> { 'U', 'S', 'Y' }));

        _aretes.Add(new Aretes('W', new List<char> { 'T', 'X', 'Z' }));           // Path to the end town
        _aretes.Add(new Aretes('X', new List<char> { 'W', 'U', 'Y', 'Z' }));      // Path to the end town
        _aretes.Add(new Aretes('Y', new List<char> { 'X', 'V', 'Z' }));           // Path to the end town
    }
    private void Start()
    {
        AddSommet();
        AddArete();
    }
}
