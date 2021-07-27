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

        hp = maxHp;
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        // Plays the death animation, waits 2 seconds, fades the sprite out, waits 3 seconds and destroys game object (performance)
        collider.enabled = false;
        anim.SetTrigger("death");

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(player.Knockback(0.5f, 300f, this.transform));
        }

        if (other.gameObject.tag == "Bullet")
        {
            TakeHit(50f);
            CameraShake.Instance.ShakeCamera(6f, 0.1f);
        }
    }

}
