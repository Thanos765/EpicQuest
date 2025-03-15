using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class SwordAttack : MonoBehaviour 
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
  Vector2 rightAttackOffset;
    Animator animator;
    public float knockBackForce = 500f;
    private AgentWeapon equippedWeapon;
    public Transform playerTransform;
    public Vector3 swordOffset;
    public HealthText healthText;
    // Set the additional damage from the equipped weapon
    
      public void Awake(){
        rightAttackOffset = transform.position;
        playerTransform = transform.parent; // Assuming the player's transform is the parent
        transform.position = playerTransform.position + swordOffset;
    }

 public void SetDamage(float damage)
    {
        swordDamage = damage;
         Debug.Log($"Updated damage to: {damage}");
    }
    
//attack right
    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;

        
    }


//attack left
    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }



//disable collider at the end of attack
    public void StopAttack()
    {
        swordCollider.enabled = false;
    }


//when weapon collider hits enemy... damage enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            healthText.SetPlayerDamageText(swordDamage);
            Debug.Log($"Attacking with damage: {swordDamage}");
            // Calculate direction between character and slime
            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockBack = direction * knockBackForce;

            damageable.OnHit(swordDamage, knockBack);
        }
    }

 
  

    }
























