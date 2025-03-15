using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.VisualScripting;
using Inventory.UI;
using UnityEngine.SceneManagement; 

namespace Inventory.Model
{
    [CreateAssetMenu(fileName ="New Inventory", menuName ="Inventory System/Inventory")]
    [Serializable]
    public class InventorySO : ScriptableObject 
    {
        [SerializeField]
        public List<InventoryItem> inventoryItems = new List<InventoryItem>(); //struct of inventory items
        [field: SerializeField]
        public int Size =20; //size of struct
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; //when inventory gets updated
        

private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Make sure the InventorySO persists between scenes
        DontDestroyOnLoad(this);
    }

        public int AddItem( ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item == null)
             {
                Debug.LogError("Tried to add a null item to the inventory.");
                return quantity;
            }
        if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
                }
                return quantity;
            }
            quantity = AddStackableItem(item , quantity);
            InformAboutChange();
            return quantity;
        }



        private int AddItemToFirstFreeSlot(ItemSO item, int quantity , List<ItemParameter> itemState = null)
        {
            
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }




        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;




        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot( item, newQuantity);
            }
            return quantity;

        }




        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);

                InformAboutChange();
            }
        }



        public void AddItem(InventoryItem item)
        {
            AddItem( item.item, item.quantity);
        }




        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }



        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            if (itemIndex_1 < 0 || itemIndex_1 >= inventoryItems.Count ||
                itemIndex_2 < 0 || itemIndex_2 >= inventoryItems.Count)
            {
                // Invalid indices, return without swapping
                Debug.Log("Invalid indices for swapping.");
                return;
            }
            Debug.Log("Swapping items at indices: " + itemIndex_1 + " and " + itemIndex_2);
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            Debug.Log("Swap successful.");
            InformAboutChange();
        }



        private void InformAboutChange()
        {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
         Debug.Log("Informing about inventory change");
        }
        
    }



    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;

        public InventoryItem(int ID, ItemSO item, int quantity, List<ItemParameter> itemState) : this()
        {
            this.item = item;
            this.quantity = quantity;
            this.itemState = new List<ItemParameter>(itemState);
        }

        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
                itemState = new List<ItemParameter>()
            };
    }
}

