using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Mouse rotation variables
    Vector2 dir;
    private float angle;

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
    }
    
    // Executed on a fixed timer, by default, FixedUpdate() will be called 50 times per minute
    void FixedUpdate()
    {
        // Moves the object to a new position at a set speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
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

}
