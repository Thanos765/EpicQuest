using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class StateMachineManager 
{

public State currentEnemyState {get;set;}


public void Initialize(State startingState){

    currentEnemyState = startingState;
    currentEnemyState.EnterState();

}


public void ChangeBossState(State newState){


currentEnemyState.ExitState();
currentEnemyState = newState;
currentEnemyState.EnterState();

}

}
