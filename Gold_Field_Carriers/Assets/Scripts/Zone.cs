using System.Collections.Generic;
using UnityEngine;
public class Zone : MonoBehaviour
{
    public static bool _draw;
    public static List<int> _line = new();
    public static int _turn;
    public static Zone Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < SC_PlayerTurn.Instance.turn; i++)
        {
            _line.Add(0);
        }
    }
    public static void SetDesert()
    {
        Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "Desert";
        _draw = true;
    }

    public static void SetRiver()
    {
        Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "River";
        _draw = true;
    }

    public static void SetMountain()
    {
        Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "Mountain";
        _draw = true;
    }
}
