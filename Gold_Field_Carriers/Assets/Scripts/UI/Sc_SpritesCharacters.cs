using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SpritesCharacters : MonoBehaviour
{
    [Header("Characters")]
    public List<GameObject> _charactersSelection = new List<GameObject>();
    public List<GameObject> _selectedCharacters = new List<GameObject>(3);
    public GameObject _characterToSelect;

    [Header("Button")] 
    public Button _startButton;

    [Header("ID")] 
    public List<int> _ID = new List<int>();
    
    private int _characterIndex = 2;
    
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
        _characterToSelect = _charactersSelection[_characterIndex];
    }

    public void LeftArrow()
    {
        Vector3 pos = _charactersSelection[0].GetComponent<RectTransform>().position;
        Vector2 size = _charactersSelection[0].GetComponent<RectTransform>().sizeDelta;

        Vector2 textsize = _charactersSelection[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        Vector3 textpos = _charactersSelection[0].transform.GetChild(0).GetComponent<RectTransform>().position;
        float fontsize = _charactersSelection[0].transform.GetChild(0).GetComponent<TMP_Text>().fontSize;
        
        for (var i = _charactersSelection.Count -1; i >= 0; i--)
        {
            Vector3 pos2 = _charactersSelection[i].GetComponent<RectTransform>().position;
            Vector2 size2 = _charactersSelection[i].GetComponent<RectTransform>().sizeDelta;
            
            Vector2 textsize2 = _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
            Vector3 textpos2 = _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().position;
            float fontsize2 = _charactersSelection[i].transform.GetChild(0).GetComponent<TMP_Text>().fontSize;
            
            _charactersSelection[i].GetComponent<RectTransform>().position = pos;
            _charactersSelection[i].GetComponent<RectTransform>().sizeDelta = size;
            
            _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = textsize;
            _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().position = textpos;
            _charactersSelection[i].transform.GetChild(0).GetComponent<TMP_Text>().fontSize = fontsize;
            
            
            pos = pos2;
            size = size2;
            
            textsize = textsize2;
            textpos = textpos2;
            fontsize = fontsize2;
        }

        _characterIndex--;

        if (_characterIndex == -1)
            _characterIndex = 5;
        
        _characterToSelect = _charactersSelection[_characterIndex];
        
        foreach (var character in _charactersSelection)
        {
            if(character == _characterToSelect)
                character.GetComponent<Button>().interactable = true;
            else
                character.GetComponent<Button>().interactable = false; 
        }
    }

    public void RightArrow()
    {
        Vector3 pos = _charactersSelection[_charactersSelection.Count - 1].GetComponent<RectTransform>().position;
        Vector2 size = _charactersSelection[_charactersSelection.Count - 1].GetComponent<RectTransform>().sizeDelta;
        
        Vector2 textsize = _charactersSelection[_charactersSelection.Count - 1].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        Vector3 textpos = _charactersSelection[_charactersSelection.Count - 1].transform.GetChild(0).GetComponent<RectTransform>().position;
        float fontsize = _charactersSelection[_charactersSelection.Count - 1].transform.GetChild(0).GetComponent<TMP_Text>().fontSize;
        
        for (var i = 0; i < _charactersSelection.Count; i++)
        {
            Vector3 pos2 = _charactersSelection[i].GetComponent<RectTransform>().position;
            Vector2 size2 = _charactersSelection[i].GetComponent<RectTransform>().sizeDelta;
            
            Vector2 textsize2 = _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
            Vector3 textpos2 = _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().position;
            float fontsize2 = _charactersSelection[i].transform.GetChild(0).GetComponent<TMP_Text>().fontSize;
            
            _charactersSelection[i].GetComponent<RectTransform>().position = pos;
            _charactersSelection[i].GetComponent<RectTransform>().sizeDelta = size;
            
            _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = textsize;
            _charactersSelection[i].transform.GetChild(0).GetComponent<RectTransform>().position = textpos;
            _charactersSelection[i].transform.GetChild(0).GetComponent<TMP_Text>().fontSize = fontsize;
            
            pos = pos2;
            size = size2;
            
            textsize = textsize2;
            textpos = textpos2;
            fontsize = fontsize2;
        }
        _characterIndex++;

        if (_characterIndex == 6)
            _characterIndex = 0;
        
        _characterToSelect = _charactersSelection[_characterIndex];
        
        foreach (var character in _charactersSelection)
        {
            if(character == _characterToSelect)
                character.GetComponent<Button>().interactable = true;
            else
                character.GetComponent<Button>().interactable = false; 
        }
    }
}
