using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class IdleState : State
{
 public float moveSpeed = 0f;
 public int rand;

private Transform playerTransform;
    public IdleState(Boss boss, StateMachineManager stateMachineManager) : base(boss, stateMachineManager)
    {

    }


public override void EnterState(){
    base.EnterState();
Debug.Log("in idle state");
 playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

public override void ExitState(){
    base.ExitState();
  
}


public override void FrameUpdate(){
    base.FrameUpdate();

if(boss.isAggroed)
{
    if(boss.isInAttackDistance)
    {   
        boss.stateMachineManager.ChangeBossState(boss.attackState);
    }
    else
    {
        boss.stateMachineManager.ChangeBossState(boss.runState);
    }

}
}


public override void PhysicsUpdate(){
    base.PhysicsUpdate();
}



}
