using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spreadProjectile;
    public GameObject firePoint;

    // Projectile spread
    Vector2 startPoint;
    float radius = 5f;
    float spreadProjectileSpeed = 10f;


    void Update()
    {

    }

    void ShootProjectile()
    {
        Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
    }

    public void SpawnProjectile(int numberOfProjectiles)
    {
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
}
