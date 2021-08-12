using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PistolAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float retreatDistance;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public GameObject metalParticleEffect;
    public Transform firePoint;
    public Animator anim;

    [SerializeField] private int ammoAmount = 8;

    private bool isReloading = false;

    public float hp;
    public float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        RotateTowards(target.position);

        if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = this.transform.position;
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
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        // If current way point is greater than the total amount of waypoints (Count) then the end of the path has been reached
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }

    void OnPathComplete(Path p)
    {
        // Making sure their are no errors
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            // Generate a path for the enemy to take
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

        // Changes the direction in which the enemy is facing depending on the velocity of the x value
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetTrigger("reload");

        yield return new WaitForSeconds(1.2f);

        ammoAmount = 8;
        isReloading = false;
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
