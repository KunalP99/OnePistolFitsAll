using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    public Color low;
    public Color high;

    public Vector3 Offset;

    public void SetHealth(float health, float maxHealth)
    {
        // Health bar is only visible when enemy is not at full health
        slider.gameObject.SetActive(health < maxHealth);

        slider.value = health;
        slider.maxValue = maxHealth;

        // Set the colour depending on how hurt the enemy is
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the slider with the parent object
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
