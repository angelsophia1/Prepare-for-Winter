using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverShowText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject infoText;
    private void Start()
    {
        infoText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.SetActive(false);
    }
}
