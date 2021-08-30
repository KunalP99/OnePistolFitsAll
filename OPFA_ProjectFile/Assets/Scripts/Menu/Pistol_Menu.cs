using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Menu : MonoBehaviour
{
    Vector2 dir;

    private float angle;

    public Animator anim;

    public void Update()
    {
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("fire");
        }
    }
}
