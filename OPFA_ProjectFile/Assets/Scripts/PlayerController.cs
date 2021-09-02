using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private GameObject crosshair;

    // Particles
    public GameObject metalParticleEffect;
    public ParticleSystem switchPistolParticleEffect;
    public ParticleSystem switchSmgParticleEffect;
    public ParticleSystem switchHugeParticleEffect;
    public ParticleSystem dashParticleEffect;

    Animator anim;

    Vector2 movement;

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
    public GameObject hugeUI;
    public GameObject pistolSwitchUI;
    public GameObject smgSwitchUI;
    public GameObject hugeSwitchUI;
    public SpriteRenderer sr;
    [HideInInspector] public float nextSwitchTime = 0;
    [HideInInspector] public bool pistolPicked = false;
    [HideInInspector] public bool smgPicked = true;
    [HideInInspector] public bool smgFound = false;
    [HideInInspector] public bool hugePicked = true;
    [HideInInspector] public bool hugeFound = false;

    bool isReloadingAnim = false;

    // Timer variables
    public DashCoutdownTimer dashTimer;

    public GameObject deathScreen;
    public GameObject afterDeathObject;
    [HideInInspector] public bool isDead = false; 

    void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");

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
        // Pistol switch
        if (Input.GetKeyDown(KeyCode.Alpha1) && pistolPicked == true)
        {
            // Enabling and disabling ammo scripts on player object
            GetComponent<RegularAmmo>().enabled = true;
            GetComponent<SMGAmmo>().enabled = false;
            GetComponent<HugeAmmo>().enabled = false;

            // Play particle effect for each change 
            switchPistolParticleEffect.Play();

            pistolPicked = false;
            smgPicked = true;
            hugePicked = true;

            // Sets all the UI for icons
            pistolUI.SetActive(true);
            smgUI.SetActive(false);
            hugeUI.SetActive(false);
            pistolSwitchUI.SetActive(true);
            smgSwitchUI.SetActive(false);
            hugeSwitchUI.SetActive(false);
        }

        // SMG switch
        if (Input.GetKeyDown(KeyCode.Alpha2) && smgPicked == true && smgFound == true)
        {
            GetComponent<RegularAmmo>().enabled = false;
            GetComponent<SMGAmmo>().enabled = true;
            GetComponent<HugeAmmo>().enabled = false;

            // Play particle effect for each change 
            switchSmgParticleEffect.Play();

            pistolPicked = true;
            smgPicked = false;
            hugePicked = true;

            pistolUI.SetActive(false);
            smgUI.SetActive(true);
            hugeUI.SetActive(false);
            pistolSwitchUI.SetActive(false);
            smgSwitchUI.SetActive(true);
            hugeSwitchUI.SetActive(false);
        }

        // Huge switch
        if (Input.GetKeyDown(KeyCode.Alpha3) && hugePicked == true && hugeFound == true)
        {
            GetComponent<RegularAmmo>().enabled = false;
            GetComponent<SMGAmmo>().enabled = false;
            GetComponent<HugeAmmo>().enabled = true;

            //PARTICLE EFFECT
            switchHugeParticleEffect.Play();

            pistolPicked = true;
            smgPicked = true;
            hugePicked = false;

            pistolUI.SetActive(false);
            smgUI.SetActive(false);
            hugeUI.SetActive(true);
            pistolSwitchUI.SetActive(false);
            smgSwitchUI.SetActive(false);
            hugeSwitchUI.SetActive(true);
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
        if (other.gameObject.tag == "Pistol_Enemy_Bullet")
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
            isDead = true;

            afterDeathObject.SetActive(true);
            // Play death animation
            Instantiate(metalParticleEffect, transform.position, Quaternion.identity);

            // Give player option to restart level or quit
            crosshair.SetActive(false);
            Cursor.visible = true;
            deathScreen.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}

