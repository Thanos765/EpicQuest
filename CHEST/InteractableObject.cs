using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class InteractableObject : MonoBehaviour, ISaveController
{

  
    public bool canInteract;
    private Open openAnim;
    private bool open = false;
    public int chestCoins;
     public GameObject interactText;
     [SerializeField] private AudioSource audioSource;
    [SerializeField] public string id;

    ChestManager chestManager;


    
    void Awake(){
         openAnim = GetComponentInParent<Open>();
         interactText.gameObject.SetActive(false);

           // Ensure that the Chest Manager is present in the scene.
       chestManager = ChestManager.Instance;
        if (chestManager == null)
        {
            Debug.LogError("Chest Manager not found in the scene!");
            return;
        }

    }
    public void Interact()
    {

        if (open==true)
        {
            openAnim.openChest();
            interactText.gameObject.SetActive(false);
            canInteract=false;
           
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && open==false)
        {
            interactText.gameObject.SetActive(true);
            canInteract = true;
            chestManager.SetCurrentChest(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other )
    {
        if (other.CompareTag("Player") && open==false)
        {
            interactText.gameObject.SetActive(false);
            canInteract = false;
            
        }

    }

    public void InteractButtonClicked()
    {
        if (canInteract)
        {
            if(!open)
            {
                open= true;
                Interact();
                audioSource.Play();
                CoinCount.coins += chestCoins;  
            }
           
        }
    }



 public void LoadGameData(SaveData data)
    {
       data.chestsOpened.TryGetValue(id, out open);
       
       if(open){
        Interact();
       }
       
    }

    public void SaveGameData(ref SaveData data)
    {
     if (data.chestsOpened.ContainsKey(id)) {
        data.chestsOpened.Remove(id);
     }
        data.chestsOpened.Add(id,open);
        
    }


}
