using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public  class State 
{
    public Boss boss;
public StateMachineManager stateMachineManager;


public State(Boss boss , StateMachineManager stateMachineManager){
    this.boss = boss;
    this.stateMachineManager = stateMachineManager;
}
    
public virtual void EnterState(){}

public virtual void ExitState(){}


public virtual void FrameUpdate(){}

public virtual void PhysicsUpdate(){}

}


