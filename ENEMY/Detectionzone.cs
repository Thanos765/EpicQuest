using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectionzone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
  
   

   
    // detect when object enters range
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == tagTarget)

        detectedObjs.Add(collider);
    }

    // detect when object leaves range
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == tagTarget)
        detectedObjs.Remove(collider);
    }
}
