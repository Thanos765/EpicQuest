using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Inventory.Model
{
public class PlayerHealth : MonoBehaviour, IDamageable
{
    
Rigidbody2D rb;
 Collider2D physicsCollider;
public float invincibilityTime = 0.25f;
public bool disableSimulation = false;
private float invincibleTimeElapsed = 0f;


//control hit and death animation while taking damage
 public float Health
    {
        set
        {
            if (currentHealth.Value< maxHealth)
            {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    textTransform.SetParent(canvas.transform);
                }
            }

            currentHealth.Value = value;

            if (currentHealth.Value<= 0)
            {
                
                animator.SetTrigger("Death");
                Targetable = false;
                
            }
        }

        get
        {
            return currentHealth.Value;
        }

    }

//targetable player
public bool Targetable{
    get { return targetable; }
    set {

     targetable = value;
    if(disableSimulation){
        rb.simulated=false;
    }
    physicsCollider.enabled = value;
    }

}


//invincibility frames
    public bool Invincible { 
        get {
            return invincible;

    } set{
        invincible = value;
        if(invincible == true){
            invincibleTimeElapsed = 0f;
        }
        Debug.Log(Invincible);
      }

    }



public GameObject healthText;
Animator animator;
public bool canTurnInvincible = false;
 SpriteRenderer sr;
[SerializeField] public FloatValueSO currentHealth;
 
[SerializeField] public int maxHealth = 4;
public bool targetable = true;
public bool invincible = false;

[SerializeField]
private  PlayerHealthUI healthBarUI;




     // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        currentHealth.Value=1f;
        
    }


    //damage player
    public void OnHit(float damage,Vector2 knockback)
{



    if (!Invincible)
    {
        Health -= damage / maxHealth;
        currentHealth.Value = Health;
        currentHealth.Value = Mathf.Clamp01(currentHealth.Value);
        UpdateHealthBarUI();
        rb.AddForce(knockback, ForceMode2D.Impulse);



        if (canTurnInvincible)
        {
            // activate invincibility timer
            Invincible = true;
        }
    }
}


public void OnHit(float damage){



    if (!Invincible)
    {
        Health -= damage / maxHealth;
        currentHealth.Value = Health;
        currentHealth.Value = Mathf.Clamp01(currentHealth.Value);
        UpdateHealthBarUI();


        if (canTurnInvincible)
        {
            // activate invincibility timer
            Invincible = true;
        }

    }

}
 private void UpdateHealthBarUI()
        {
            healthBarUI.SetValue(currentHealth.Value);
        }




     public void AddHealth(int healthBoost)
    {
        int health = Mathf.RoundToInt(currentHealth.Value * maxHealth);
        int val = health + healthBoost;
        currentHealth.Value = (val > maxHealth ? maxHealth : (float)val / maxHealth);
    }
    


     public void RemoveObject()
    {
        Destroy(gameObject);
    }
 
// short invincibility after getting hit
public void FixedUpdate(){
    if(Invincible){
        invincibleTimeElapsed += Time.deltaTime;
        if(invincibleTimeElapsed > invincibilityTime){
            Invincible = false;
        }
    }
}


}

}

