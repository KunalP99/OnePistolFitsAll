using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGAmmo : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    [SerializeField] private int smgAmmoAmount = 32;
    [SerializeField] private GameObject[] smgAmmo;
    [HideInInspector] public int smgMaxAmmo = 96;
    public int smgCurrentBullets;

    [HideInInspector] public bool isReloading = false;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        smgCurrentBullets = smgMaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (smgCurrentBullets > 0)
        {
            // *FIRE*
            if (Input.GetButton("Fire1") && smgAmmoAmount > 0 && Time.time >= nextTimeToFire)
            {
                Shoot();
                smgCurrentBullets--;
            }

            // *RELOAD*
            if (Input.GetKey(KeyCode.R) && smgAmmoAmount < 32)
            {
                StartCoroutine(Reload());
                return;
            }
            else if (smgAmmoAmount == 0 && smgCurrentBullets > 0)
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

        smgAmmoAmount -= 1;

        smgAmmo[smgAmmoAmount].gameObject.SetActive(false);

        // Removes the correct amount of bullets when current ammo is less than 32
        if (smgCurrentBullets < 32)
        {
            smgAmmo[smgCurrentBullets - 1].gameObject.SetActive(false);
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        anim.SetTrigger("reload");

        // Will wait for 1 second after animation is triggered, so player cannot shoot while reloading 
        yield return new WaitForSeconds(1f);

        // Displays UI images for amount of bullets player has
        smgAmmoAmount = 32;
        for (int i = 0; i <= 31; i++)
        {
            if (smgCurrentBullets >= 32)
            {
                smgAmmo[i].gameObject.SetActive(true);
            }
            else if (smgCurrentBullets < 32) // When player has less than 32 current ammo, it will show in the UI
            {
                for (int j = 0; j <= smgCurrentBullets - 1; j++)
                smgAmmo[j].gameObject.SetActive(true);           
            }
        }

        isReloading = false;
    }
}
