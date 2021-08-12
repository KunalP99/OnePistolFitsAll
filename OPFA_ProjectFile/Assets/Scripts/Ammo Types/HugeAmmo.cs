using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeAmmo : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    [SerializeField] private int hugeAmmoAmount = 10;
    [SerializeField] private GameObject[] hugeAmmo;
    [HideInInspector] public int hugeMaxAmmo = 30;
    public int hugeCurrentBullets;

    [HideInInspector] public bool isReloading = false;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        hugeCurrentBullets = hugeMaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (hugeCurrentBullets > 0)
        {
            // *FIRE*
            if (Input.GetButton("Fire1") && hugeAmmoAmount > 0 && Time.time >= nextTimeToFire)
            {
                Shoot();
                hugeCurrentBullets--;
            }

            // *RELOAD*
            if (Input.GetKey(KeyCode.R) && hugeAmmoAmount < 10)
            {
                StartCoroutine(Reload());
                return;
            }
            else if (hugeAmmoAmount == 0 && hugeCurrentBullets > 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
    }

    void Shoot()
    {
        // Adds a cooldown to firing
        nextTimeToFire = Time.time + 1f / fireRate;
        anim.SetTrigger("fire");

        // Spawns the bullet prefab at firePoint position with firePoint rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        CameraShake.Instance.ShakeCamera(1f, 0.1f);

        hugeAmmoAmount -= 1;

        hugeAmmo[hugeAmmoAmount].gameObject.SetActive(false);

        // Removes the correct amount of bullets when current ammo is less than 32
        if (hugeCurrentBullets < 10)
        {
            hugeAmmo[hugeCurrentBullets - 1].gameObject.SetActive(false);
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        anim.SetTrigger("reload");

        // Will wait for 1 second after animation is triggered, so player cannot shoot while reloading 
        yield return new WaitForSeconds(1f);

        // Displays UI images for amount of bullets player has
        hugeAmmoAmount = 10;
        for (int i = 0; i <= 9; i++)
        {
            if (hugeCurrentBullets >= 10)
            {
                hugeAmmo[i].gameObject.SetActive(true);
            }
            else if (hugeCurrentBullets < 10) // When player has less than 32 current ammo, it will show in the UI
            {
                for (int j = 0; j <= hugeCurrentBullets - 1; j++)
                    hugeAmmo[j].gameObject.SetActive(true);
            }
        }

        isReloading = false;
    }
}
