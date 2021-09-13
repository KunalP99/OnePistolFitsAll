using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    // Fire mechanics

    private int ammoAmount = 1;
    private float timeBtwShots;
    private float startTimeBtwShots = 3f;

    public int randNum;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rotate(player.position);

        if (timeBtwShots <= 0 && ammoAmount > 0)
        {
            timeBtwShots = startTimeBtwShots;
            animator.SetTrigger("Fire");
            ammoAmount--;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
        if (ammoAmount <= 0)
        {
            animator.SetTrigger("Reload");
            ammoAmount++;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fire");
    }

    private void Rotate(Vector2 target)
    {
        // Rotation towards the player, so object always faces the player no matter where they move
        var offset = 0f;
        Vector2 direction = target - (Vector2)rb.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
