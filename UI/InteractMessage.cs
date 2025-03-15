using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractMessage : MonoBehaviour
{

    public bool playerdetected;
    public GameObject interactText;

     void  Awake()
    {
        interactText.gameObject.SetActive(false);
        
    }
    //dialogue starts with e and with space it continues
     private void Update()
    {
        if (playerdetected && Input.GetKeyDown(KeyCode.E))
        
           interactText.gameObject.SetActive(false);
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        
            playerdetected = true;  
            interactText.gameObject.SetActive(true);
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        
            
            playerdetected = false;
            interactText.gameObject.SetActive(false);
        
    }
}


    
