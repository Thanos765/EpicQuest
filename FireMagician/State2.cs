using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State2 
{
    public Boss2 boss2;
    public StateMachineManager2 stateMachineManager2;


    public State2(Boss2 boss2, StateMachineManager2 stateMachineManager2)
    {
        this.boss2 = boss2;
        this.stateMachineManager2 = stateMachineManager2;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }


    public virtual void FrameUpdate() { }

    public virtual void PhysicsUpdate() { }
}
