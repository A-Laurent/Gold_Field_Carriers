using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoice : MonoBehaviour
{
    public static bool choice1;
    public static bool choice2;
   public void Choice1()
    {
        choice1 = true;
    }
    public void Choice2() 
    {
        choice2 = true;
    }
}
