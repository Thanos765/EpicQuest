using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState2: State2
{
    public float moveSpeed = 0f;
 private Animator anim;
    private Transform playerTransform;
    public IdleState2(Boss2 boss2, StateMachineManager2 stateMachineManager2) : base(boss2, stateMachineManager2)
    {

    }


    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("in idle state");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = boss2.GetComponent<Animator>();
    }

    public override void ExitState()
    {
        base.ExitState();

    }


    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (boss2.isAggroed)
        {
            if (boss2.isInAttackDistance)
            {
                boss2.stateMachineManager2.ChangeBossState(boss2.attackState2);
            }
            else
            {
                boss2.stateMachineManager2.ChangeBossState(boss2.runState2);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}