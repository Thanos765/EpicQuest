using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Inventory.Model;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage; //item image
        [SerializeField]
        private TMP_Text quantityTxt; //quantity of items

        [SerializeField]
        private Image borderImage; // border of item icon when clicked

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;    // dragging and clicking items in inventory

        private bool empty = true;
        private float lastClickTime;
        private const float doubleClickTimeThreshold = 0.3f; // Adjust as needed for your game.

        public void Awake()
        {
            ResetData();
            Deselect();


        }



//item goes back to its slot when not switched with another slot
      public void ResetData()
        {
            if (itemImage != null)
            {
                itemImage.sprite = null; // Clear the sprite to avoid accessing a destroyed object
                itemImage.gameObject.SetActive(false);
            }
            empty = true;
        }


//deselect item when clicking elsewhere
        public void Deselect()
        {
            if (borderImage != null)
            {
                borderImage.enabled = false;
            }
        }



    //set data of item
        public void SetData(Sprite sprite, int quantity)
        {
            if (itemImage != null)
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = sprite;
                quantityTxt.text = quantity + "";
                empty = false;
            }
        }


//select item
        public void Select()
        {
            borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {

             if (empty)
                    return;
            if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // Double-tap detected
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            lastClickTime = Time.time;
            OnItemClicked?.Invoke(this);
        }
        }


//end of dragging
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

//start of dragging
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }


//dropping item
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }


        public void OnDrag(PointerEventData eventData)
        {

        }


    }
}