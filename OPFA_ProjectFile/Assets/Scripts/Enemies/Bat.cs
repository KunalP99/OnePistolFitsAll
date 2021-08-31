using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Bat : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public GameObject bloodEffect;

    Animator anim;
    PlayerController player;

    BoxCollider2D bCollider;
    Rigidbody2D rb;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        bCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        GameObject pistol = GameObject.Find("Pistol");
        player = pistol.GetComponent<PlayerController>();

        hp = maxHp;
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            gameObject.GetComponent<BatAI>().speed = 0;

            // Changes the body type of the rigidbody to static so it enemy doesn't move back when hit
            rb.bodyType = RigidbodyType2D.Static;

            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        // Plays the death animation, waits 2 seconds, fades the sprite out, waits 3 seconds and destroys game object (performance)
        bCollider.enabled = false;
        anim.SetTrigger("death");

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

        yield return new WaitForSeconds(5f);

        Destroy(bloodEffect);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(player.Knockback(0.5f, 1000f, this.transform));
        }

        if (other.gameObject.tag == "Bullet")
        {
            TakeHit(50f);
            CameraShake.Instance.ShakeCamera(6f, 0.1f);
        }

        if (other.gameObject.tag == "Huge_Bullet")
        {
            TakeHit(100f);
            CameraShake.Instance.ShakeCamera(7f, 0.1f);
        }
    }
        

}
