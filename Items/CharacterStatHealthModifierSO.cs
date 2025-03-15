using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{

    //heal player
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerHealth health = character.GetComponent<PlayerHealth>();
       if (health != null){
            health.AddHealth((int)val);
       }
    }
}

}