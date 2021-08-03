using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    public SMGAmmo smgAmmo;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Add relevant ammo types here
            smgAmmo.smgCurrentBullets = 96;
            smgAmmo.StartCoroutine(smgAmmo.Reload());
            Destroy(gameObject);

            // Huge ammo

            // Power ammo etc.
        }
    }
}
