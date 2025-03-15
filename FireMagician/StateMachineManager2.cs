using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineManager2 
{
    public State2 currentEnemyState { get; set; }


    public void Initialize(State2 startingState2)
    {

        currentEnemyState = startingState2;
        currentEnemyState.EnterState();

    }


    public void ChangeBossState(State2 newState2)
    {


        currentEnemyState.ExitState();
        currentEnemyState = newState2;
        currentEnemyState.EnterState();

    }

}
