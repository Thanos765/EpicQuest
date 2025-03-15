using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack2 : MonoBehaviour
{
       public float swordDamage2 = 2f;
    Collider2D swordCollider;
    public float knockBackForce2 = 600f;

    private bool canAttack2 = true;



public void Awake(){
    swordCollider = GetComponent<Collider2D>();
    swordCollider.enabled = false; // Ensure collider starts disabled
}


 public void Attack2()
    {
        if (canAttack2)
        {
            swordCollider.enabled = true;
            canAttack2 = false; // Disable attack until cooldown is finished
        }
    }

public void StopAttack2()
    {
        swordCollider.enabled = false;
    }

    public void ResetCooldown2()
    {
        canAttack2 = true; // Reset the attack cooldown
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
            Vector2 knockBack = direction * knockBackForce2;

            //other.SendMessage("onHit", swordDamage, knockBack);
            damageable.OnHit(swordDamage2,knockBack);
        }
    }
}
