using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Inventory.Model;
public class ShopKeeperText : MonoBehaviour
{

    public Collider2D other;
    public bool playerdetected;
    public GameObject dialoguetemplate;
    public string[] dialogue;
    private int index = 0;
    public TMP_Text dialogueText;
    public float wordSpeed;
    public GameObject interactText;
    private bool isTyping = false;
    private bool isDialogueActive = false;
    PlayerController pl;

    // sign
    public void Awake()
    {

        dialogueText.text = "";
        interactText.gameObject.SetActive(false);
        dialoguetemplate.SetActive(false);
    }
    
    //dialogue starts with e and with space it continues
   public void InteractButtonClicked()
    {
        if (playerdetected && !isTyping && !isDialogueActive)
        {
           dialoguetemplate.SetActive(true);
           interactText.gameObject.SetActive(false);
            isDialogueActive=true;
            StartCoroutine(Typing());
            
        }
        if (playerdetected && !isTyping && isDialogueActive)
        {
            NextLine();
        }
    }



    IEnumerator Typing()
    {
        isTyping = true; // Set the flag when the dialogue starts typing
        if (index < dialogue.Length)
        {
            foreach (char letter in dialogue[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else
        {
            RemoveText();
        }
        isTyping = false; // Reset the flag when the dialogue is done typing
    }
    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
                RemoveText();
                index = 0;
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguetemplate.SetActive(false);
        isDialogueActive = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Player"))
        {
            playerdetected = true;  
            interactText.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            
            playerdetected = false;
            dialoguetemplate.SetActive(false);
            interactText.gameObject.SetActive(false);
        }
    }
}

    
