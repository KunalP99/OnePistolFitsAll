using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBigProjectile : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        // Rotates the object 90 degrees to make it horizontal 
        transform.eulerAngles = Vector3.forward * 90;

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
