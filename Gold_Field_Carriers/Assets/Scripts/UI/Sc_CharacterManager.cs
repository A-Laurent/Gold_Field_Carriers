using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sc_CharacterManager : MonoBehaviour
{
    public List<string> _characterSelectedNames = new List<string>();
    public List<Sprite> _characterSelectedSprites = new List<Sprite>();
    public static Sc_CharacterManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if(File.Exists(Application.dataPath + "/Saves/Characters.json"))
            Sc_SaveCharacters.Instance.LoadFromJson();       
    }
}
