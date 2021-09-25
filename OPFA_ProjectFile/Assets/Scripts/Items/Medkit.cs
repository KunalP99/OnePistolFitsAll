using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    GameObject player;
    GameObject healthBar;

    public AudioClip healthItemAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.FindGameObjectWithTag("Health");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(healthItemAudio, this.gameObject.transform.position);
            player.GetComponent<PlayerController>().currentHealth = 500;
            healthBar.GetComponent<Health>().SetHealth(player.GetComponent<PlayerController>().currentHealth);

            Destroy(gameObject);
            // Possible animation OR particle effect
        }
    }
}
