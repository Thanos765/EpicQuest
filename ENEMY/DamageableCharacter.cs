using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DamageableCharacter : MonoBehaviour, IDamageable, ISaveController
{
         public GameObject healthText;
        Animator animator;
        Rigidbody2D rb;
        Collider2D physicsCollider;
        public bool canTurnInvincible = false;
        public float invincibilityTime = 0.25f;
        public bool disableSimulation = false;
        SpriteRenderer sr;
        public int Coins;
        private float invincibleTimeElapsed = 0f;
        private bool dead= false;
 [SerializeField] public string enemiesID; // Serialized field for the ID (Optional, for debugging)

 

    //controlling slime's animation for getting hit and dying
    public float Health
    {
        set
        {
            if (value < health)
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

            health = value;

            if (health <= 0)
            {
                
                animator.SetTrigger("Death");
                Targetable = false;
                dead=true;
                CoinCount.coins += Coins;



            }
        }

        get
        {
            return health;
        }

    }

//targetable character
public bool Targetable{get { return targetable; }
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

    public float health = 3f;
    public  bool targetable = true;

    public bool invincible = false;

    SaveData data;


     // Start is called before the first frame update
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }


    //damage enemy

    //multiple
    public void OnHit(float damage, Vector2 knockback)
    {
        if(!Invincible){

         
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
        if(canTurnInvincible){
            //activate invincibility timer
            Invincible = true;
        }
        }
    }
    //single
    public void OnHit(float damage)
    {
        if(!Invincible){
        Health -= damage;

        if(canTurnInvincible){
            //activate invincibility timer
            Invincible = true;
        }
        }
    }
     //remove enemy after death
    public void RemoveObject()
    {
         gameObject.SetActive(false);
    }



public void LoadGameData(SaveData data)
{
    data.enemiesKilled.TryGetValue(enemiesID, out dead);
       
       if(dead){
            RemoveObject();
       }
}


public void SaveGameData(ref SaveData data){
    if (data.enemiesKilled.ContainsKey(enemiesID)) {
        data.enemiesKilled.Remove(enemiesID);
     }
    data.enemiesKilled.Add(enemiesID,dead);
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
