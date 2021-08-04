using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public GameObject metalParticleEffect;
    public ParticleSystem switchPistolParticleEffect;
    public ParticleSystem switchSmgParticleEffect;
    public ParticleSystem dashParticleEffect;

    Animator anim;

    Vector2 movement;

    [HideInInspector] public KeyCode lastKeyHit;

    // Health variables
    public int maxHealth = 100;
    public int currentHealth;
    public Health healthBar;

    // Mouse rotation variables
    Vector2 dir;
    private float angle;

    // Enemy damage variables
    private int batDamage = 20;
    private int pistolDamage = 30;

    // Dash variables
    private bool isDashButtonDown;
    private float cooldownTime = 2f;
    private float nextDashTime = 0;

    BoxCollider2D playerCollider;

    // Ammo switching variables
    public GameObject pistolUI;
    public GameObject smgUI;
    public GameObject pistolSwitchUI;
    public GameObject smgSwitchUI;
    public SpriteRenderer sr;
    [HideInInspector] public float nextSwitchTime = 0;
    [HideInInspector] public bool pistolPicked = false;
    [HideInInspector] public bool smgPicked = true;
    [HideInInspector] public bool smgFound = false;

    bool isReloadingAnim = false;

    // Timer variables
    public DashCoutdownTimer dashTimer;
    public SmgSwitchTimer smgSwitchTimer;

    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame, noy good when dealing with physics to use Update(), as framerate can constantly change, so use FixedUpdate() instead
    void Update()
    {
        // *INPUT*
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // *MOUSE ROTATION*
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Atan2 - Return value is the angle in radians, between x-axis and 2D vector, starting at 0 and terminating at x and Y. Multiply by Rad2Deg - Converts radians to degrees
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Creates a rotation, that rotates the angle degrees around the axis 
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        // *DASH* + cooldown
        if (Time.time > nextDashTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                nextDashTime = Time.time + cooldownTime;

                isDashButtonDown = true;
                dashTimer.gameObject.SetActive(true);

                dashParticleEffect.Play();

                StartCoroutine(dashTimer.DashTimerEnabled());
                StartCoroutine(DashInvicibility(0.1f));
            }
        }

        // *AMMO SWITCHING* 
        if (Time.time > nextSwitchTime)
        {
            // Pistol switch
            if (Input.GetKeyDown(KeyCode.Alpha1) && pistolPicked == true)
            {
                nextSwitchTime = Time.time + 9f;

                // Enabling and disabling ammo scripts on player object
                GetComponent<RegularAmmo>().enabled = true;
                GetComponent<SMGAmmo>().enabled = false;

                // Play particle effect for each change 
                switchPistolParticleEffect.Play();

                // Checks to see whether the last key pressed is 2, which will then show the smg cooldown timer
                if (lastKeyHit == KeyCode.Alpha2)
                {
                    smgSwitchTimer.gameObject.SetActive(true);
                    StartCoroutine(smgSwitchTimer.SmgTimerEnabled());
                }

                pistolPicked = false;
                smgPicked = true;

                // Sets all the UI for icons
                pistolUI.SetActive(true);
                smgUI.SetActive(false);
                pistolSwitchUI.SetActive(true);
                smgSwitchUI.SetActive(false);
            }
        }

        // SMG switch
        if (Time.time > nextSwitchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && smgPicked == true && smgFound == true)
            {
                nextSwitchTime = Time.time + 0.1f;

                lastKeyHit = KeyCode.Alpha2;

                GetComponent<RegularAmmo>().enabled = false;
                GetComponent<SMGAmmo>().enabled = true;

                // Play particle effect for each change 
                switchSmgParticleEffect.Play();

                pistolPicked = true;
                smgPicked = false;

                pistolUI.SetActive(false);
                smgUI.SetActive(true);
                pistolSwitchUI.SetActive(false);
                smgSwitchUI.SetActive(true);
            }
        }

        // DEATH
        Death();
    }

    // Executed on a fixed timer, by default, FixedUpdate() will be called 50 times per minute
    void FixedUpdate()
    {
        // Moves the player to a new position at a set speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // *DASH*
        if (isDashButtonDown)
        {
            float dashAmount = 3f;
            rb.MovePosition(rb.position + movement * dashAmount);
            isDashButtonDown = false;
        }
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }

    public IEnumerator DashInvicibility(float invicibilityTime)
    {
        playerCollider.enabled = false;

        yield return new WaitForSeconds(invicibilityTime);

        playerCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bat")
        {
            currentHealth -= batDamage;
            healthBar.SetHealth(currentHealth);

            CameraShake.Instance.ShakeCamera(10f, 0.1f);
        }

        if (other.gameObject.tag == "Pistol_Enemy")
        {
            currentHealth -= pistolDamage;
            healthBar.SetHealth(currentHealth);

            CameraShake.Instance.ShakeCamera(10f, 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pistol_Bullet")
        {
            currentHealth -= pistolDamage;
            healthBar.SetHealth(currentHealth);

            CameraShake.Instance.ShakeCamera(10f, 0.1f);
        }
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            // Play death animation
            Instantiate(metalParticleEffect, transform.position, Quaternion.identity);

            // Give player option to restart level or quit

            // Pause game

            gameObject.SetActive(false);
        }
    }
}

