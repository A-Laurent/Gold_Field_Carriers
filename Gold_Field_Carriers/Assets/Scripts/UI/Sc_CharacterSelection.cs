using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CharacterSelection : MonoBehaviour
{
    [SerializeField] private Color32 _characterAlreadySelectedColor;
    public void SelectCharacters()
    {
        if (Sc_SpritesCharacters.Instance._charactersSelection[Sc_SpritesCharacters.Instance._charactersSelectionIndex].GetComponent<Sc_CanSelect>()._canSelect == true)
        {
            this.GetComponent<Image>().sprite = Sc_SpritesCharacters.Instance._charactersSelection[2].GetComponent<Image>().sprite;
            Sc_SpritesCharacters.Instance._charactersSelection[2].GetComponent<Image>().color = _characterAlreadySelectedColor;
            Sc_SpritesCharacters.Instance._charactersSelection[2].GetComponent<Sc_CanSelect>()._canSelect = false;
        }
    }
}
