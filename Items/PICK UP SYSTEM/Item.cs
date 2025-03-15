using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int ID;

    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;
    private ItemSO itemSO;

    [SerializeField]
    private float duration = 0.3f;
     public bool pickedUp = false;
    public int price;
    private int Coins;
 



    private void Start()
    {
         GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;

    }


//destroy item when picked up
    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());

    }



//animation when picking up items
    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = 
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }


//when player is inside item collider
private void OnTriggerEnter2D(Collider2D other)
{

    if (other.gameObject.CompareTag("Player")) // Check if the item hasn't been picked up yet
    {
        CanPickUp();
    }

}

//if enough coins pick up
    public void CanPickUp()
    {
        
    if(!pickedUp && Coins >= price && Coins!=0){
        PerformPickUp();
        pickedUp=true;
    }
    else if(Coins<0)
{
    CannotPickUp();
    Debug.Log("Cannot buy item");
}
    }



//cannot pick up items
 public void CannotPickUp()
    {
        
        pickedUp =false;
    }


//  pick up item
    public void PerformPickUp()
    {
            Coins -= price;
    CoinCount.coins = Coins; // Update the coin count
    DestroyItem();
    }




        }
       


