using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Awake()
    {
        // Hide the normal original cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Position of mouse cursor
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }
}
