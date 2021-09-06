using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBigProjectile : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private PlayerController player;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        GameObject pistol = GameObject.Find("Pistol");
        player = pistol.GetComponent<PlayerController>();

        // Destroys object after 20 seconds have passed (for performance)
        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(player.Knockback(0.5f, 1500f, this.transform));
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(20);

        Destroy(gameObject);
    }
}
