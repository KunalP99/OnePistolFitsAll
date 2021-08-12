using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugePickup : MonoBehaviour
{
    public PlayerController player;
    public HugeAmmo huge;
    public GameObject hugePickup;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.nextSwitchTime = Time.time + 1.2f;

            player.smgPicked = true;
            player.pistolPicked = true;
            player.hugeFound = true;
            player.hugePicked = false;

            player.GetComponent<RegularAmmo>().enabled = false;
            player.GetComponent<SMGAmmo>().enabled = false;
            player.GetComponent<HugeAmmo>().enabled = true;

            huge.hugeMaxAmmo = 30;

            // Play particle effect for each change 
            player.switchHugeParticleEffect.Play();

            player.pistolUI.SetActive(false);
            player.smgUI.SetActive(false);
            player.hugeUI.SetActive(true);
            player.pistolSwitchUI.SetActive(false);
            player.smgSwitchUI.SetActive(false);
            player.hugeSwitchUI.SetActive(true);

            Destroy(gameObject);
        }
    }
}
