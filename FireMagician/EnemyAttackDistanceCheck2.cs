using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDistanceCheck2 : MonoBehaviour
{

    public string tagTarget = "Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>();


     Boss2 boss2;




    private void Awake()
    {
        boss2 = GetComponentInParent<Boss2>();
    
    }


    //if player inside the boss' attack distance...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {

            boss2.SetAttackDistanceBool(true);
             detectedObjs.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
            boss2.SetAttackDistanceBool(false);
            detectedObjs.Remove(collision);
        }
    }

}

