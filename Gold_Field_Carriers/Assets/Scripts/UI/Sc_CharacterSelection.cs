using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CharacterSelection : MonoBehaviour
{
    [SerializeField] private Color32 _characterAlreadySelectedColor;
    private Sc_SpritesCharacters _spritesCharacters;

    private bool _firstSelection = false;
    private bool _secondSelection = false;
    private bool _thirdSelection = false;

    private void Start()
    {
        _spritesCharacters = Sc_SpritesCharacters.Instance;
    }

    public void UndoFirstCharacter()
    {
        if (_spritesCharacters._selectedCharacters[0] != null)
        {
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[1].interactable = false;
            _spritesCharacters._selectCharacterButton[2].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;

            _spritesCharacters._selectedCharacters[0].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[0].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[0] = null;
        }
    }

    public void UndoSecondCharacter()
    {
        if (_spritesCharacters._selectedCharacters[1] != null)
        {
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[0].interactable = false;
            _spritesCharacters._selectCharacterButton[2].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;
            
            _spritesCharacters._selectedCharacters[1].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[1].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[1] = null;    
        }    
    }

    public void UndoThirdCharacter()
    {
        if(_spritesCharacters._selectedCharacters[2] != null)
        {
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[0].interactable = false;
            _spritesCharacters._selectCharacterButton[1].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;

            _spritesCharacters._selectedCharacters[2].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[2].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[2] = null;     
        }
    }

    public void SelectCharacters()
    {
        if (_spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect == true)
        {
            this.GetComponent<Image>().sprite = _spritesCharacters._characterToSelect.GetComponent<Image>().sprite;
            _spritesCharacters._characterToSelect.GetComponent<Image>().color = _characterAlreadySelectedColor;
            _spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect = false;

            if (_spritesCharacters._selectCharacterButton[0].interactable == true)
            {
                if (_firstSelection == false)
                {
                    _spritesCharacters._selectCharacterButton[1].interactable = true;
                    _firstSelection = true;
                }
                
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[2].interactable = false;
                
                _spritesCharacters._undoCharacterButton[0].interactable = true;
                
                _spritesCharacters._selectedCharacters[0] = _spritesCharacters._characterToSelect;
            }
            else if (_spritesCharacters._selectCharacterButton[1].interactable == true)
            {
                if (_secondSelection == false)
                {
                    _spritesCharacters._selectCharacterButton[2].interactable = true;
                    _secondSelection = true;
                }
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[1].interactable = false;
                
                _spritesCharacters._undoCharacterButton[1].interactable = true;
                
                _spritesCharacters._selectedCharacters[1] = _spritesCharacters._characterToSelect;
            }
            else
            {
                if (_thirdSelection == false)
                {
                    _thirdSelection = true;
                }
                _spritesCharacters._selectCharacterButton[2].interactable = false;
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[1].interactable = false;
                
                _spritesCharacters._undoCharacterButton[2].interactable = true;
                
                _spritesCharacters._selectedCharacters[2] = _spritesCharacters._characterToSelect;
            }
        }
    }
}
