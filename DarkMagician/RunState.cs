using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RunState : State
{

    private Transform playerTransform;
    public float moveSpeed = 3f;
     Animator anim;
    public RunState(Boss boss, StateMachineManager stateMachineManager) : base(boss, stateMachineManager)
    {
         
    }


    public override void EnterState(){
        base.EnterState();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
         anim = boss.GetComponent<Animator>();
         Debug.Log("in run state");
        
    }

public override void ExitState(){
    base.ExitState();
}


public override void FrameUpdate(){
base.FrameUpdate();
        if (playerTransform != null)
        {
            Vector2 moveDirection = (playerTransform.position - boss.transform.position).normalized;
            boss.MoveBoss(moveDirection * moveSpeed);
        }
        else
        {
            boss.stateMachineManager.ChangeBossState(boss.idleState);
        }
if(boss.isInAttackDistance){
    boss.stateMachineManager.ChangeBossState(boss.attackState);
    
}


}

public override void PhysicsUpdate(){
base.PhysicsUpdate();
}


}

