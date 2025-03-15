using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem , IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();


        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? 
                    DefaultParametersList : itemState);

                foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
                Debug.Log($"Applied modifier: {data.value} to {character.name}");

            }
            return true;
            }
            return false;
        }
    }

}