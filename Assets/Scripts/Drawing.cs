using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] FillTheForm _form;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _form.canDraw = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _form.canDraw = false;
    }

    
}
