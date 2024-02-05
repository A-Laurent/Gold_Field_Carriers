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
            if(_spritesCharacters._selectedCharacters[0] == null || _spritesCharacters._selectedCharacters[1] == null || _spritesCharacters._selectedCharacters[2] == null)
                _firstSelection = false;
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[1].interactable = false;
            _spritesCharacters._selectCharacterButton[2].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;

            _spritesCharacters._selectedCharacters[0].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[0].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[0] = null;

            this.GetComponent<Image>().color = new Color32(210, 132, 45, 255);
            
            if (_spritesCharacters._selectedCharacters[0] != null &&
                _spritesCharacters._selectedCharacters[1] != null &&
                _spritesCharacters._selectedCharacters[2] != null)
            {
                _spritesCharacters._startButton.interactable = true;
            }
            else
            {
                _spritesCharacters._startButton.interactable = false;     
            }
        }
    }

    public void UndoSecondCharacter()
    {
        if (_spritesCharacters._selectedCharacters[1] != null)
        {
            if(_spritesCharacters._selectedCharacters[0] == null || _spritesCharacters._selectedCharacters[1] == null || _spritesCharacters._selectedCharacters[2] == null)
                _secondSelection = false;
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[0].interactable = false;
            _spritesCharacters._selectCharacterButton[2].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;
            
            _spritesCharacters._selectedCharacters[1].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[1].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[1] = null;   
            
            this.GetComponent<Image>().color = new Color32(210, 132, 45, 255);
            
            if (_spritesCharacters._selectedCharacters[0] != null &&
                _spritesCharacters._selectedCharacters[1] != null &&
                _spritesCharacters._selectedCharacters[2] != null)
            {
                _spritesCharacters._startButton.interactable = true;
            }
            else
            {
                _spritesCharacters._startButton.interactable = false;     
            }
        }    
    }

    public void UndoThirdCharacter()
    {
        if(_spritesCharacters._selectedCharacters[2] != null)
        {
            if(_spritesCharacters._selectedCharacters[0] == null || _spritesCharacters._selectedCharacters[1] == null || _spritesCharacters._selectedCharacters[2] == null)
                _thirdSelection = false;
            this.GetComponentInChildren<Button>().interactable = true;
            _spritesCharacters._selectCharacterButton[0].interactable = false;
            _spritesCharacters._selectCharacterButton[1].interactable = false;
            this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = null;

            _spritesCharacters._selectedCharacters[2].GetComponent<Sc_CanSelect>()._canSelect = true;
            _spritesCharacters._selectedCharacters[2].GetComponent<Image>().color = Color.white;
            _spritesCharacters._selectedCharacters[2] = null;  
            
            this.GetComponent<Image>().color = new Color32(210, 132, 45, 255);
            
            if (_spritesCharacters._selectedCharacters[0] != null &&
                _spritesCharacters._selectedCharacters[1] != null &&
                _spritesCharacters._selectedCharacters[2] != null)
            {
                _spritesCharacters._startButton.interactable = true;
            }
            else
            {
                _spritesCharacters._startButton.interactable = false;     
            }
        }
    }

    public void SelectCharacters()
    {
        if (_spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect == true)
        {
            this.GetComponent<Image>().sprite = _spritesCharacters._characterToSelect.GetComponent<Image>().sprite;
            this.GetComponent<Image>().color = Color.white;
            _spritesCharacters._characterToSelect.GetComponent<Image>().color = _characterAlreadySelectedColor;
            _spritesCharacters._characterToSelect.GetComponent<Sc_CanSelect>()._canSelect = false;

            if (_spritesCharacters._selectCharacterButton[0].interactable == true)
            {
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[2].interactable = false;
                
                if(_spritesCharacters._selectedCharacters[1] == null)
                    _spritesCharacters._selectCharacterButton[1].interactable = true;
                else if(_spritesCharacters._selectedCharacters[2] == null)
                    _spritesCharacters._selectCharacterButton[2].interactable = true;
                
                _spritesCharacters._undoCharacterButton[0].interactable = true;
                
                _spritesCharacters._selectedCharacters[0] = _spritesCharacters._characterToSelect;
                
                if (_spritesCharacters._selectedCharacters[0] != null &&
                    _spritesCharacters._selectedCharacters[1] != null &&
                    _spritesCharacters._selectedCharacters[2] != null)
                {
                    _spritesCharacters._startButton.interactable = true;
                }
                else
                {
                    _spritesCharacters._startButton.interactable = false;     
                }
            }
            else if (_spritesCharacters._selectCharacterButton[1].interactable == true)
            {
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[1].interactable = false;
                
                if(_spritesCharacters._selectedCharacters[0] == null)
                    _spritesCharacters._selectCharacterButton[0].interactable = true;
                else if(_spritesCharacters._selectedCharacters[2] == null)
                    _spritesCharacters._selectCharacterButton[2].interactable = true;
                
                _spritesCharacters._undoCharacterButton[1].interactable = true;
                
                _spritesCharacters._selectedCharacters[1] = _spritesCharacters._characterToSelect;
                
                if (_spritesCharacters._selectedCharacters[0] != null &&
                    _spritesCharacters._selectedCharacters[1] != null &&
                    _spritesCharacters._selectedCharacters[2] != null)
                {
                    _spritesCharacters._startButton.interactable = true;
                }
                else
                {
                    _spritesCharacters._startButton.interactable = false;     
                }
            }
            else
            {
                _spritesCharacters._selectCharacterButton[2].interactable = false;
                _spritesCharacters._selectCharacterButton[0].interactable = false;
                _spritesCharacters._selectCharacterButton[1].interactable = false;
                
                if(_spritesCharacters._selectedCharacters[0] == null)
                    _spritesCharacters._selectCharacterButton[0].interactable = true;
                else if(_spritesCharacters._selectedCharacters[1] == null)
                    _spritesCharacters._selectCharacterButton[1].interactable = true;
                
                _spritesCharacters._undoCharacterButton[2].interactable = true;
                
                _spritesCharacters._selectedCharacters[2] = _spritesCharacters._characterToSelect;

                if (_spritesCharacters._selectedCharacters[0] != null &&
                    _spritesCharacters._selectedCharacters[1] != null &&
                    _spritesCharacters._selectedCharacters[2] != null)
                {
                    _spritesCharacters._startButton.interactable = true;
                }
                else
                {
                    _spritesCharacters._startButton.interactable = false;     
                }
            }
        }
    }
}
