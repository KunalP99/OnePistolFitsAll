using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator anim;

    //When the mouse hovers over the button, then exectute method
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("Hover", true);
    }

    //When mouse is not on button, execute method
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("Hover", false);
    }
}
