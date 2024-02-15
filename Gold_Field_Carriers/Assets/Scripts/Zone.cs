using UnityEngine;
public class Zone : MonoBehaviour
{
    public static bool _draw;
    public static int _turn = 1;
    public static Zone Instance;

    public static bool _animDesert;
    public static bool _animRiver;

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

        Sc_GameManager.Instance.RaiseDecoration(Sc_GameManager.Instance._desertDecoration);

        _animDesert = true;
        _animRiver = false;
    }

    public static void SetRiver()
    {
        Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "River";
        _draw = true;

        Sc_GameManager.Instance.RaiseDecoration(Sc_GameManager.Instance._riverDecoration);

        _animRiver = true;
        _animDesert= false;
    }

    public static void SetMountain()
    {
        Stats._zonePlayer[SC_PlayerTurn.Instance.turn] = "Mountain";
        _draw = true;

        _animRiver = false;
        _animDesert = false;
    }

    public static void AddTurn()
    {
        if (SC_PlayerTurn.Instance.turn == 0) 
            _turn+= 1;
        if (_turn > 2)
            Card.Instance._horde.SetActive(true);
    }
}
