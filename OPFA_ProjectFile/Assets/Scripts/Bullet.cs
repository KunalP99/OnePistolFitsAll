using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;

    private Bat bat;

    // Start is called before the first frame update
    void Start()
    {
        bat = GameObject.FindGameObjectWithTag("Bat").GetComponent<Bat>();

        // Moves bullet in a direction
        rb.velocity = transform.right * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bat")
        {
            bat.TakeHit(50f);
            Destroy(gameObject);
        }
    }
}
