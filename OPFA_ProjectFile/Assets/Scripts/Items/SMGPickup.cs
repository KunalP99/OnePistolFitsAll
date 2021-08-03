using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGPickup : MonoBehaviour
{
    public PlayerController player;
    public SMGAmmo smg;
    public GameObject smgPickup;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.nextSwitchTime = Time.time + 1.2f; 

            player.smgFound = true;
            player.smgPicked = true;
            player.pistolPicked = true;

            player.GetComponent<RegularAmmo>().enabled = false;
            player.GetComponent<SMGAmmo>().enabled = true;

            smg.smgMaxAmmo = 96;

            // Play particle effect for each change 
            player.switchSmgParticleEffect.Play();

            player.pistolUI.SetActive(false);
            player.smgUI.SetActive(true);
            player.pistolSwitchUI.SetActive(false);
            player.smgSwitchUI.SetActive(true);

            Destroy(gameObject);
        }
    }
}
