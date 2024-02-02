using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SpritesCharacters : MonoBehaviour
{
    [Header("Characters")]
    public List<GameObject> _charactersSelection = new List<GameObject>();
    public List<Sprite> _charactersSprites = new List<Sprite>();
    public GameObject _temporySprite;   
    
    public int _charactersSelectionIndex = 2;

    public static Sc_SpritesCharacters Instance;

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
        foreach (var sprite in _charactersSelection)
        {
            _charactersSprites.Add(sprite.GetComponent<Image>().sprite);       
        }    
    }

    public void RightArrow()
    {
        if (_charactersSelectionIndex < 6)
        {
            _charactersSelectionIndex++;

            _charactersSelection.Add(_charactersSelection[0]);
            _charactersSelection.Remove(_charactersSelection[0]);

            for (int i = 0; i < _charactersSprites.Count; i++)
            {
                if(i >= 0)
                    _charactersSelection[i].GetComponent<Image>().sprite = _charactersSprites[i];
            }   
        }
        
        if(_charactersSelectionIndex == 6)
            _charactersSelectionIndex = 0;
    }
    
    public void LeftArrow()
    {
        if (_charactersSelectionIndex > 0)
        {
            _charactersSelectionIndex--;
            
            _temporySprite = _charactersSelection[_charactersSelection.Count - 1];
            _charactersSelection.Remove(_charactersSelection[_charactersSelection.Count - 1]);
            _charactersSelection.Insert(0, _temporySprite);

            for (int i = 0; i < _charactersSprites.Count; i++)
            {
                if(i <= _charactersSprites.Count)
                    _charactersSelection[i].GetComponent<Image>().sprite = _charactersSprites[i];
            }   
        }
        
        if(_charactersSelectionIndex == 0)
            _charactersSelectionIndex = 6;
    }
}
