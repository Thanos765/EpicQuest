using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItriggerCheckable 
{
    bool isAggroed{get;set;}
    bool isInAttackDistance{get;set;}

    void SetAttackDistanceBool(bool isInAttackDistance);
    void SetAggroStatus(bool isAggroed);






}
