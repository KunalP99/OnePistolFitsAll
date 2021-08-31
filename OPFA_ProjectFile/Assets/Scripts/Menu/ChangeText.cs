using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    public Animator anim;

    public void OnMouseOver()
    {
        anim.SetBool("Hover", true);
    }

    public void OnMouseExit()
    {
        anim.SetBool("Hover", false);
    }
}
