using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
[CreateAssetMenu]
public class CharacterStatDamageModifier : CharacterStatModifierSO
{
     
    public override void AffectCharacter(GameObject character, float val)
    {
        SwordAttack swordAttack = character.GetComponentInChildren<SwordAttack>();
       if (swordAttack != null){
           swordAttack.SetDamage(val);
       }
    }
}
}