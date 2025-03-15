using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{
        public float swordDamage = 1f;
    Collider2D swordCollider;
    public float knockBackForce = 500f;

    private bool canAttack = true;



public void Awake(){
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
        if (damageable!=null)
        {
            // Deal damage to the enemy
            
            // calculate direction between character and Boss
            // Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockBack = direction * knockBackForce;

            //other.SendMessage("onHit", swordDamage, knockBack);
            damageable.OnHit(swordDamage,knockBack);
        }
    }
}
