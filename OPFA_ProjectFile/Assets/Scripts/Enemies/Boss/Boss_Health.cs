using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    float maxHealth = 5000f;

    public float currentHealth;

    // Stage 2 varaibles
    public bool stage2Active = false;
    public bool stage3Active = false;
    [HideInInspector] public bool isProjectileRunning = false;
    public bool isSpreadProjectileRunning = false;
    public Boss_Pulse_Projectile pulseProjectile;

   void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (stage2Active == true && isProjectileRunning == false)
        {
            // Spawn medkit??

            pulseProjectile.StartCoroutine(GetComponent<Boss_Pulse_Projectile>().Pulse());
            //pulseProjectile.Pulse1();
            isProjectileRunning = true;

            StartCoroutine(Wait(5f));

            Debug.Log("Run pulse projectile");
        }
        
        if (stage3Active == true && isSpreadProjectileRunning == false)
        {
            // Spawn medkit??

            pulseProjectile.StartCoroutine(GetComponent<Boss_Pulse_Projectile>().RandomSpread());

            isSpreadProjectileRunning = true;
        }
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


        if (currentHealth <= 4000)
        {
            stage2Active = true;
        }
        
        if (currentHealth <= 2500)
        {
            stage3Active = true;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
