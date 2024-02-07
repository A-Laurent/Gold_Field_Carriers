using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sc_ResetCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Button>().interactable = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);   
    }
}