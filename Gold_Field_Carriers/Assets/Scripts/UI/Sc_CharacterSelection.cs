using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sc_CharacterSelection : MonoBehaviour
{
    [SerializeField] private Color32 _characterAlreadySelectedColor;
    public void SelectCharacters()
    {
        if (Sc_SpritesCharacters.Instance._selectedCharacter.GetComponent<Sc_CanSelect>()._canSelect == true)
        {
            this.GetComponent<Image>().sprite = Sc_SpritesCharacters.Instance._selectedCharacter.GetComponent<Image>().sprite;
            Sc_SpritesCharacters.Instance._selectedCharacter.GetComponent<Image>().color = _characterAlreadySelectedColor;
            Sc_SpritesCharacters.Instance._selectedCharacter.GetComponent<Sc_CanSelect>()._canSelect = false;

            if (Sc_SpritesCharacters.Instance._selectButton[0].interactable == true)
            {
                Sc_SpritesCharacters.Instance._selectButton[0].interactable = false;
                Sc_SpritesCharacters.Instance._selectButton[1].interactable = true;
                Sc_SpritesCharacters.Instance._selectButton[2].interactable = false;
            }
            else if (Sc_SpritesCharacters.Instance._selectButton[1].interactable == true)
            {
                Sc_SpritesCharacters.Instance._selectButton[0].interactable = false;
                Sc_SpritesCharacters.Instance._selectButton[1].interactable = false;
                Sc_SpritesCharacters.Instance._selectButton[2].interactable = true;   
            }
            else
            {
                Sc_SpritesCharacters.Instance._selectButton[0].interactable = false;
                Sc_SpritesCharacters.Instance._selectButton[1].interactable = false;
                Sc_SpritesCharacters.Instance._selectButton[2].interactable = false;  
            }
        }
    }
}
