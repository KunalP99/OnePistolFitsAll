using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Small_Projectiles : MonoBehaviour
{
    private ParticleSystem particleEffect;
    private ParticleSystem smallParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        particleEffect = GameObject.FindGameObjectWithTag("Boss_Projectile_Particles").GetComponent<ParticleSystem>();
        smallParticleEffect = GameObject.FindGameObjectWithTag("Boss_Small_Projectile_Particles").GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall")
        {
            Instantiate(smallParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
