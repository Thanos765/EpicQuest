using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [Serializable]
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; } //if item is stackable or not
        [SerializeField]
        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1; //max stack of items

        [field: SerializeField]
        public string Name { get; set; } //item name

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; } //description of items

        [field: SerializeField]
        public Sprite ItemImage { get; set; } // item

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; } //default item parameters (for instance durability of item)

        
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
         public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}