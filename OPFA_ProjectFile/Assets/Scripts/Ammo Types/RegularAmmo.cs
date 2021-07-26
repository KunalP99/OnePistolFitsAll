using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularAmmo : MonoBehaviour
{
    // Bullet variables
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    [SerializeField] private int regularAmmoAmount = 8;
    [SerializeField] private GameObject[] regularAmmo;

    [HideInInspector] public bool isReloading;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // *FIRE*
        if (Input.GetButtonDown("Fire1") && regularAmmoAmount > 0)
        {
            anim.SetTrigger("fire");
            Shoot();
            regularAmmoAmount -= 1;
            regularAmmo[regularAmmoAmount].gameObject.SetActive(false);
        }

        // *RELOAD*
        if (Input.GetKey(KeyCode.R) && regularAmmoAmount < 8)
        {

            anim.SetTrigger("reload");

            // Displays UI images 
            regularAmmoAmount = 8;
            for (int i = 0; i <=7; i++)
            {
                regularAmmo[i].gameObject.SetActive(true);
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
