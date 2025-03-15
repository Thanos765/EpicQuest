using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;


public class AgentWeapon : MonoBehaviour
{

    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    private ItemSO itemSO;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

     private InventoryUIController inventoryUIController;

    SwordAttack swordAttack;

        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

    GameObject character;
    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }


    public void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                }; 
            }

        }
    }

}
