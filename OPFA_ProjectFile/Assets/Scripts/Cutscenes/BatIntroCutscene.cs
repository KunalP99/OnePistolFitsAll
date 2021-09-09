using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIntroCutscene : MonoBehaviour
{
    private Rigidbody2D rb;

    float speed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * speed;
    }
}
