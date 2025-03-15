using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    private ItemSO itemSO;
   private Item item;
    private CoinCount coinCount;
    private int coin;
    bool flag;

    int ID;
    
//pick up item when in collision zone
  private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Item"))
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null && !item.pickedUp)
        {
            if (CoinCount.coins >= item.price) // Check if player has enough coins
            {
                CoinCount.coins -= item.price; // Subtract the item's price from coins
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
            else
            {
                // Player doesn't have enough coins to pick up the item
                Debug.Log("Not enough coins to pick up the item.");
            }
        }
    }
    }
  
}