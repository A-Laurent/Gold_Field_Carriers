using UnityEngine;
public class Zone : MonoBehaviour
{
    public static bool _draw;
    public static int _turn = 1;
    public static Zone Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
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

    public static void AddTurn()
    {
        if (SC_PlayerTurn.Instance.turn == 0) 
            _turn+= 1;
        if (_turn > 2)
            Card.Instance._horde.SetActive(true);
    }
}
