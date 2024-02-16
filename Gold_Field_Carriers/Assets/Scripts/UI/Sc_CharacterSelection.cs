using System;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sc_CharacterSelection : MonoBehaviour
{
    [SerializeField] private Color32 _characterAlreadySelectedColor;
    [SerializeField] private Color32 _initColor;
    [SerializeField] private List<GameObject> _characters = new List<GameObject>();
    [SerializeField] private List<GameObject> resetButton = new List<GameObject>();
    private Sc_SpritesCharacters _spritesCharacters;
    [SerializeField] private int _player = 0;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _spritesCharacters = Sc_SpritesCharacters.Instance;
    }

    public void ResetCharacter()
    { 
        foreach (var characterselection in Sc_SpritesCharacters.Instance._charactersSelection)
        {
            if (characterselection.GetComponent<Image>().sprite ==
                EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite)
            {
                characterselection.GetComponent<Image>().color = Color.white;
                characterselection.GetComponent<Sc_CanSelect>()._canSelect = true;
            }
        }

        foreach (var character in _characters)
        {
            if (EventSystem.current.currentSelectedGameObject.gameObject == character)
            {
                character.transform.GetChild(0).gameObject.SetActive(false);
                character.GetComponent<Image>().sprite = null;
                character.GetComponent<Image>().color = _initColor;
            }
        }

        _player = 0;
        foreach (var character in _characters)
        {
            if (character.GetComponent<Image>().sprite == null)
                break;
            _player++;
        }

        _startButton.interactable = false;
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Button>().interactable = false;
    }

    public void SelectCharacters()
    {
        if (_spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect == true &&
            _player < _characters.Count)
        {
            _initColor = _characters[_player].GetComponent<Image>().color;
            _spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect = false;
            _spritesCharacters._characterToSelect.GetComponent<Image>().color = _characterAlreadySelectedColor;
            Sc_SpritesCharacters.Instance._selectedCharacters[_player] =
                Sc_SpritesCharacters.Instance._characterToSelect;
            _characters[_player].GetComponent<Image>().sprite =
                Sc_SpritesCharacters.Instance._characterToSelect.GetComponent<Image>().sprite;
            _characters[_player].GetComponent<Image>().color = Color.white;
            Sc_SpritesCharacters.Instance._ID[_player] =
                Sc_SpritesCharacters.Instance._characterToSelect.GetComponent<Sc_CanSelect>()._id;
            _characters[_player].GetComponent<RectTransform>().sizeDelta = new Vector2(430, 432);
            _player++;
        }

        bool _canStart = true;
        foreach (var character in _characters)
        {
            if (character.GetComponent<Image>().sprite == null)
            {
                _canStart = false;
                break;
            }
        }

        _startButton.interactable = _canStart;
    }


    public void ResetSizeButton1()
    {
        resetButton[0].GetComponent<RectTransform>().sizeDelta = new Vector2(230, 350);
    }
    public void ResetSizeButton2()
    {
        resetButton[1].GetComponent<RectTransform>().sizeDelta = new Vector2(230, 350);
    }
    public void ResetSizeButton3()
    {
        resetButton[2].GetComponent<RectTransform>().sizeDelta = new Vector2(230, 350);
    }
}