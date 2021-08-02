using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public PlayerController player;
    public Health healthBar;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.currentHealth = 100;
            healthBar.SetHealth(player.currentHealth);

            Destroy(gameObject);
            // Possible animation OR particle effect
        }
    }
}
