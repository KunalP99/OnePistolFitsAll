using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    float maxHealth = 5000f;

    public float currentHealth;
    public GameObject smoke;

   void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
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

    public void TakeHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }

        if (currentHealth <= 2500)
        {
            smoke.SetActive(true);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
