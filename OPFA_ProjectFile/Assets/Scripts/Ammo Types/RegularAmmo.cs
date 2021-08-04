using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularAmmo : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    [SerializeField] private int regularAmmoAmount = 8;
    [SerializeField] private GameObject[] regularAmmo;

    [HideInInspector] public bool isReloading = false;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        // *FIRE*
        if (Input.GetButtonDown("Fire1") && regularAmmoAmount > 0 && Time.time >= nextTimeToFire)
        {
            Shoot();
        }

        // *RELOAD*
        if (Input.GetKey(KeyCode.R) && regularAmmoAmount < 8)
        {
            StartCoroutine(Reload());
            return;
        }
        else if (regularAmmoAmount == 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    void Shoot()
    {
        // Adds a cooldown to firing
        nextTimeToFire = Time.time + 1f / fireRate;
        anim.SetTrigger("fire");

        CameraShake.Instance.ShakeCamera(1f, 0.1f);

        // Spawns the bullet prefab at firePoint position with firePoint rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        regularAmmoAmount -= 1;
        regularAmmo[regularAmmoAmount].gameObject.SetActive(false);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        anim.SetTrigger("reload");

        // Will wait for 1 second after animation is triggered, so player cannot shoot while reloading 
        yield return new WaitForSeconds(1f);

        // Displays UI images for amount of bullets player has
        regularAmmoAmount = 8;
        for (int i = 0; i <= 7; i++)
        {
            regularAmmo[i].gameObject.SetActive(true);
        }

        isReloading = false;
    }

}
