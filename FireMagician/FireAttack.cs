using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class FireAttack : MonoBehaviour
{
    public float fireDamage = 1f;
    Collider2D swordCollider;
    public float knockBackForce = 500f;

    private bool canAttack = true;



    public void Awake()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false; // Ensure collider starts disabled
    }


    public void Attack()
    {
        if (canAttack)
        {
            swordCollider.enabled = true;
            canAttack = false; // Disable attack until cooldown is finished
        }
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    public void ResetCooldown()
    {
        canAttack = true; // Reset the attack cooldown
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnHit(fireDamage);
        }
    }
}
