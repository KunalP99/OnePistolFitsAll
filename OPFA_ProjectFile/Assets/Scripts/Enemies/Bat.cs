using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public PlayerController player;

    Animator anim;

    private BoxCollider2D collider;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            StartCoroutine(DeathAnimation());
            return;
        }
    }

    IEnumerator DeathAnimation()
    {
        // Plays the death animation, waits 2 seconds, fades the sprite out, waits 3 seconds and destroys game object (performance)
        collider.enabled = false;
        anim.SetTrigger("death");

        yield return new WaitForSeconds(2f);

        anim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(player.Knockback(0.5f, 300f, this.transform));
        }
    }

}
