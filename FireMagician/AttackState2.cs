using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState2 : State2
{
    private Transform playerTransform;
    //time between attacks
    private float timeBetweenAttacks = 1.5f; // second delay before attacking


    //time duration of an attack
    private float attackDuration = 0.4f;

    //time after player exits attack range
    private float exitTimer;


    //time after player exits attack range and boss exits attack state
    private float timeTillExit = 0.2f;

    //distance until player is out of attack range
    public float distanceToCountExit = 1f;


    private FireAttack StaffAttack;

    private Animator anim;


    //flag for attack
    public bool isAttacking;

    //time when attack starts
    public float attackStartTime;

    //time when player enters attack range
    private float timeSincePlayerEnteredRange;


    public AttackState2(Boss2 boss2, StateMachineManager2 stateMachineManager2) : base(boss2, stateMachineManager2)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("attack state");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StaffAttack = boss2.GetComponentInChildren<FireAttack>();
        anim = boss2.GetComponent<Animator>();
        isAttacking = false;
        attackStartTime = -timeBetweenAttacks;
        timeSincePlayerEnteredRange = 0f;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (playerTransform !=null) { 
        if (Vector2.Distance(playerTransform.position, boss2.transform.position) <= distanceToCountExit)
        {
            Vector2 moveDirection = (playerTransform.position - boss2.transform.position).normalized;
            boss2.CheckForLeftOrRightFacing(moveDirection);
            anim.SetBool("isInAggro",false);


            if (!isAttacking && timeSincePlayerEnteredRange >= timeBetweenAttacks)
            {
                Debug.Log("Starting Attack");
                anim.SetBool("isAttacking", true);
                isAttacking = true;
                attackStartTime = Time.time;
            }

            if (!isAttacking)
            {
                timeSincePlayerEnteredRange += Time.deltaTime;
            }

            if (isAttacking && Time.time >= attackStartTime + attackDuration)
            {
                Debug.Log("Stopping Attack");
                anim.SetBool("isAttacking", false);
                isAttacking = false;
                timeSincePlayerEnteredRange = 0f;
                boss2.stateMachineManager2.ChangeBossState(boss2.idleState2);
            }
            exitTimer = 0f;
        }
        else
        {
            if (isAttacking)
            {
                Debug.Log("Stopping Attack (out of range)");
                anim.SetBool("isAttacking", false);
                boss2.Boss2StopAttack();
                timeSincePlayerEnteredRange = 0f; // Reset the timer when the attack is stopped
            }

            exitTimer += Time.deltaTime;

            if (exitTimer > timeTillExit)
            {
                Debug.Log("Transitioning to RunState");
                anim.SetBool("isInAggro", true);
                boss2.stateMachineManager2.ChangeBossState(boss2.runState2); // Transition to RunState
            }
        }

        }

    }
}
