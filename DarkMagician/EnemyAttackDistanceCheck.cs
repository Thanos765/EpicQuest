using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDistanceCheck : MonoBehaviour
{
 Boss boss;
public string tagTarget = "Player";
public List<Collider2D> detectedObjs = new List<Collider2D>();


private void Awake(){

    boss = GetComponentInParent<Boss>();
}


//if player inside the boss' attack distance...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
           boss.SetAttackDistanceBool(true);
           detectedObjs.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
            boss.SetAttackDistanceBool(false);
            detectedObjs.Remove(collision);
        }
    }

}
