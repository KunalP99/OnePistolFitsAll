using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeAmmo : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    [SerializeField] private int hugeAmmoAmount = 5;
    [SerializeField] private GameObject[] hugeAmmo;
    [HideInInspector] public int hugeMaxAmmo = 20;

    [HideInInspector] public bool isReloading = false;

    public float fireRate = 10f;
    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
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

        if (hugeMaxAmmo > 0)
        {
            // *FIRE*
            if (Input.GetButton("Fire1") && hugeAmmoAmount > 0 && Time.time >= nextTimeToFire)
            {
                Shoot();
            }
        }
       

        // *RELOAD*
        if (Input.GetKey(KeyCode.R) && hugeAmmoAmount < 5)
        {
            StartCoroutine(Reload());
            return;
        }
        else if (hugeAmmoAmount == 0)
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

        // Spawns the bullet prefab at firePoint position with firePoint rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        hugeAmmoAmount -= 1;
        hugeAmmo[hugeAmmoAmount].gameObject.SetActive(false);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        anim.SetTrigger("reload");

        // Will wait for 1 second after animation is triggered, so player cannot shoot while reloading 
        yield return new WaitForSeconds(1f);

        // Displays UI images for amount of bullets player has
        hugeAmmoAmount = 32;
        for (int i = 0; i <= 31; i++)
        {
            hugeAmmo[i].gameObject.SetActive(true);
        }

        isReloading = false;
    }
}
