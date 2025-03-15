using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class skeleton : MonoBehaviour
{
  public float damage= 1;
    public float knockBackForce = 300f;
    public Detectionzone detectionZone;
    public float movespeed = 500f;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
     void FixedUpdate()
    {
        //if enemy detects player
        if (damageableCharacter.Targetable && detectionZone.detectedObjs.Count> 0)
        {

        //calculate direction to target object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
            anim.SetBool("isMoving",true);
            //move towards detected target
            rb.AddForce(direction * movespeed * Time.deltaTime);
                if(direction.x>0){
                        spriteRenderer.flipX = true;
                }
                 else if(direction.x<0){
                spriteRenderer.flipX = false;
        } 
        }
        else {
            anim.SetBool("isMoving",false);
            AudioSource.Stop();
        }
    }


//if any character collides with player... damage
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player"){
        Collider2D collider = col.collider;
       IDamageable damageable = col.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector3 parentPosition = transform.position;
            Vector2 direction = (Vector2)(col.gameObject.transform.position - transform.position).normalized;
            Vector2 knockBack = direction * knockBackForce;
            damageable.OnHit(damage, knockBack);
        }
    }

}

}
