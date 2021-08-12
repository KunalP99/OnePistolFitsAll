using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pistol : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public GameObject metalParticleEffect;
    public Transform player;
    public Transform firePoint;
    public Animator anim;

    [SerializeField] private int ammoAmount = 8;

    private bool isReloading = false;

    public float hp;
    public float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // When enemy is reloading, allows that code to run before executing the rest of the code
        if (isReloading)
        {
            return;
        }

        RotateTowards(player.position);

        // Checks how far player is from the pistol enemy 
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            // Moves closer to target
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            // Stays in position as the player is at certain distance
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            // Moves away from the player as they are too close to the enemy (-speed variable)
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        // Handles projectile shooting from enemy
        if (timeBtwShots <= 0 && ammoAmount > 0)
        {
            ammoAmount -= 1;
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            anim.SetTrigger("fire");
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (timeBtwShots <= 0 && ammoAmount == 0)
        {
            StartCoroutine(Reload());
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeHit(25f);
            CameraShake.Instance.ShakeCamera(6f, 0.1f);
        }

        if (other.gameObject.tag == "Huge_Bullet")
        {
            TakeHit(50f);
            CameraShake.Instance.ShakeCamera(7f, 0.1f);
        }
    }

    private void RotateTowards(Vector2 target)
    {
        // Rotation towards the player, so object always faces the player no matter where they move
        var offset = 0f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetTrigger("reload");

        yield return new WaitForSeconds(1.2f);

        ammoAmount = 8;
        isReloading = false;
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Instantiate(metalParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    } 
}
