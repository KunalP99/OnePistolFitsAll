using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spreadProjectile;
    public GameObject firePoint;

    public AudioSource projectileFire;
    public AudioSource smallProjectileFire;

    float randomSpeed;

    // Projectile spread
    Vector2 startPoint;
    float radius = 10f;
    float spreadProjectileSpeed = 7f;


    void Update()
    {

    }

    void ShootProjectile()
    {
        projectileFire.Play();
        Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
    }

    public void SpawnProjectile(int numberOfProjectiles)
    {
        smallProjectileFire.Play();

        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * spreadProjectileSpeed;

            var proj = Instantiate(spreadProjectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    public void RandomSpreadProjectile(int numberOfProjectiles)
    {
        smallProjectileFire.Play();

        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            randomSpeed = Random.Range(1f, 10f);

            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * randomSpeed;

            var proj = Instantiate(spreadProjectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
}
