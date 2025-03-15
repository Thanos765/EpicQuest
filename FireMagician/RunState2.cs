using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState2: State2
{
    private Transform playerTransform;
    public float moveSpeed = 6f;
    Animator anim;
    public RunState2(Boss2 boss2, StateMachineManager2 stateMachineManager2) : base(boss2, stateMachineManager2)
    {

    }


    public override void EnterState()
    {
        base.EnterState();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = boss2.GetComponent<Animator>();
        Debug.Log("in run state");

    }

    public override void ExitState()
    {
        base.ExitState();
    }


    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (playerTransform != null)
        {
            Vector2 moveDirection = (playerTransform.position - boss2.transform.position).normalized;
            boss2.MoveBoss(moveDirection * moveSpeed);

        }
        if (boss2.isInAttackDistance)
        {
            boss2.stateMachineManager2.ChangeBossState(boss2.attackState2);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}





