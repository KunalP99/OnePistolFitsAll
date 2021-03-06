using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Moves bullet in a direction
        rb.velocity = transform.right * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bat")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Pistol_Enemy")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Big_Projectile")
        {
            Destroy(gameObject);
        }
    }
}
