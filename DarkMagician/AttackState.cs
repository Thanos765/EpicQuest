using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State 
{
    private Transform playerTransform;
    //time between attacks
    private float timeBetweenAttacks = 1.5f; // second delay before attacking


    //time duration of an attack
    private float attackDuration = 0.1f;

    //time after player exits attack range
    private float exitTimer;


    //time after player exits attack range and boss exits attack state
    private float timeTillExit = 0.2f;

    //distance until player is out of attack range
    public float distanceToCountExit = 1.2f;


    private FireAttack StaffAttack;

    private Animator anim;


//flag for attack
    public bool isAttacking;

    //time when attack starts
    public float attackStartTime;
    
    //time when player enters attack range
    private float timeSincePlayerEnteredRange;

    public int rand;

    public AttackState(Boss boss, StateMachineManager stateMachineManager) : base(boss, stateMachineManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("attack state");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StaffAttack = boss.GetComponentInChildren<FireAttack>();
        anim = boss.GetComponent<Animator>();
        isAttacking = false;
        attackStartTime = -timeBetweenAttacks;
        timeSincePlayerEnteredRange = 0f;
    }

    public override void FrameUpdate()
{
    base.FrameUpdate();
    if(playerTransform!=null){
    if (Vector2.Distance(playerTransform.position, boss.transform.position) <= distanceToCountExit)
    {
        anim.SetBool("PlayerInAttackRange",true);
        Vector2 moveDirection = (playerTransform.position - boss.transform.position).normalized;
            boss.CheckForLeftOrRightFacing(moveDirection);

        if (!isAttacking && timeSincePlayerEnteredRange >= timeBetweenAttacks)
        {
            rand=Random.Range(0,2);
              if(rand==0){
            Debug.Log("Starting Attack1");
            anim.SetBool("isAttacking", true);
             isAttacking = true;
             attackStartTime = Time.time;
             }
             if(rand==1){
            Debug.Log("Starting Attack2");
            anim.SetBool("isAttacking2", true);
             isAttacking = true;
             attackStartTime = Time.time;
             }
            
        }

        if (!isAttacking)
        {
            timeSincePlayerEnteredRange += Time.deltaTime;
        }

        if (isAttacking && Time.time >= attackStartTime + attackDuration)
        {
             if(rand==0){
            Debug.Log("Stopping Attack1");
            anim.SetBool("isAttacking", false);
            isAttacking = false;
            timeSincePlayerEnteredRange = 0f;
            boss.stateMachineManager.ChangeBossState(boss.idleState);
             }
             if(rand==1){
            Debug.Log("Stopping Attack2");
            anim.SetBool("isAttacking2", false);
            isAttacking = false;
            timeSincePlayerEnteredRange = 0f;
            boss.stateMachineManager.ChangeBossState(boss.idleState);
             }
        }

        exitTimer = 0f;
    }
    
    else    	
    {
        if (isAttacking)
        {

            Debug.Log("Stopping Attack (out of range)");
            if(rand==0){
                boss.StopAttack(); 
            }
           if(rand==1){
                boss.StopAttack2(); 
            }
            timeSincePlayerEnteredRange = 0f; // Reset the timer when the attack is stopped

        }

        exitTimer += Time.deltaTime;
        if (exitTimer > timeTillExit)
        {
            Debug.Log("Transitioning to RunState");
            anim.SetBool("PlayerInAttackRange",false);
            boss.stateMachineManager.ChangeBossState(boss.runState); // Transition to RunState
        }
    }

}


}
}
