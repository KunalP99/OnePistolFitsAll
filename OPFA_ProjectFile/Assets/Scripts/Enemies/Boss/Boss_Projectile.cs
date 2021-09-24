using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;
    private Boss_Shoot bossProjectile;
    [HideInInspector] public ParticleSystem particles;

    public float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(20, 30);

        rb = gameObject.GetComponent<Rigidbody2D>();

        GameObject pistol = GameObject.Find("Pistol");
        player = pistol.GetComponent<PlayerController>();

        GameObject boss = GameObject.Find("Boss");
        bossProjectile = boss.GetComponent<Boss_Shoot>();

        particles = GameObject.FindGameObjectWithTag("Boss_Projectile_Particles").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * randomSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Play particle effect
            Instantiate(particles, transform.position, Quaternion.identity);

            StartCoroutine(player.Knockback(0.5f, 1500f, this.transform));
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Instantiate(particles, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }


}
