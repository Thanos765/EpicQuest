using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IDamageable
{
    public float Health { set; get; }


    //for multiple objects
    public void OnHit (float damage, Vector2 knockback);


    //for single
    public void OnHit(float damage);


//Remove Object
    public void RemoveObject();


//interface for targetability
    public bool Targetable{ get; set; }


    //interface for invincibility
    public bool Invincible { get; set; }
}
