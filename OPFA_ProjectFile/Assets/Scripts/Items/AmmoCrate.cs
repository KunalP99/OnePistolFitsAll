using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    PlayerController player;

    public SMGAmmo smgAmmo;
    public HugeAmmo hugeAmmo;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Add relevant ammo types here
            // SMG ammo
            if (player.smgFound == true)
            {
                smgAmmo.smgCurrentBullets = 96;
                smgAmmo.StartCoroutine(smgAmmo.Reload());
                Destroy(gameObject);
            }

            // Huge ammo
            if (player.hugeFound == true)
            {
                hugeAmmo.hugeCurrentBullets = 30;
                hugeAmmo.StartCoroutine(hugeAmmo.Reload());
            }

            // Power ammo etc.
        }
    }
}
