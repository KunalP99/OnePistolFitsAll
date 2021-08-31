using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && player.GetComponent<PlayerController>().smgFound == true)
        {
            player.GetComponent<SMGAmmo>().smgCurrentBullets = 96;
            player.GetComponent<SMGAmmo>().StartCoroutine(player.GetComponent<SMGAmmo>().Reload());
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && player.GetComponent<PlayerController>().smgFound == true && player.GetComponent<PlayerController>().hugeFound == true)
        {
            player.GetComponent<SMGAmmo>().smgCurrentBullets = 96;
            player.GetComponent<SMGAmmo>().StartCoroutine(player.GetComponent<SMGAmmo>().Reload());
            Destroy(gameObject);

            player.GetComponent<HugeAmmo>().hugeCurrentBullets = 30;
            player.GetComponent<HugeAmmo>().GetComponent<HugeAmmo>().StartCoroutine(player.GetComponent<HugeAmmo>().Reload());
        }

        // Power ammo etc.
    }
}
