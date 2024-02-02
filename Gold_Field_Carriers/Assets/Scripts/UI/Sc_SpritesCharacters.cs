using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SpritesCharacters : MonoBehaviour
{
    [Header("Characters")]
    public List<GameObject> _charactersSelection = new List<GameObject>();
    public GameObject _selectedCharacter;

    [Header("Button")] 
    public List<Button> _selectButton = new List<Button>();
    
    [SerializeField] private int _characterIndex = 2;
    
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
        _selectedCharacter = _charactersSelection[_characterIndex];
        _selectButton[1].interactable = false;
        _selectButton[2].interactable = false;
    }

    public void RightArrow()
    {
        Vector3 pos = _charactersSelection[0].GetComponent<RectTransform>().position;
        Vector2 size = _charactersSelection[0].GetComponent<RectTransform>().sizeDelta;
        
        for (var i = _charactersSelection.Count -1; i >= 0; i--)
        {
            Vector3 pos2 = _charactersSelection[i].GetComponent<RectTransform>().position;
            Vector2 size2 = _charactersSelection[i].GetComponent<RectTransform>().sizeDelta;
            _charactersSelection[i].GetComponent<RectTransform>().position = pos;
            _charactersSelection[i].GetComponent<RectTransform>().sizeDelta = size;
            pos = pos2;
            size = size2;
        }

        _characterIndex--;

        if (_characterIndex == -1)
            _characterIndex = 5;
        
        _selectedCharacter = _charactersSelection[_characterIndex];
    }

    public void LeftArrow()
    {
        Vector3 pos = _charactersSelection[_charactersSelection.Count - 1].GetComponent<RectTransform>().position;
        Vector2 size = _charactersSelection[_charactersSelection.Count - 1].GetComponent<RectTransform>().sizeDelta;
        
        for (var i = 0; i < _charactersSelection.Count; i++)
        {
            Vector3 pos2 = _charactersSelection[i].GetComponent<RectTransform>().position;
            Vector2 size2 = _charactersSelection[i].GetComponent<RectTransform>().sizeDelta;
            _charactersSelection[i].GetComponent<RectTransform>().position = pos;
            _charactersSelection[i].GetComponent<RectTransform>().sizeDelta = size;
            pos = pos2;
            size = size2;
        }
        _characterIndex++;

        if (_characterIndex == 6)
            _characterIndex = 0;
        
        _selectedCharacter = _charactersSelection[_characterIndex];
        
    }
}
