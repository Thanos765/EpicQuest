using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using Unity.VisualScripting;

namespace Inventory.Model{
public class PlayerController : MonoBehaviour, ISaveController
{

    //bool value for moving or not
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }
    Vector2 movementInput = Vector2.zero;
    Rigidbody2D Rb;
    public AudioSource AudioSource;
    public float moveSpeed = 50f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public Animator animator;
    public SwordAttack swordAttack;
    public SpriteRenderer spriteRenderer;
    public InventorySO Playerinventory;
    public PlayerController playerController;
  


        bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
            Application.targetFrameRate = 60;
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (EventSystem.current.IsPointerOverGameObject())
        // {
        //  return;
        // }
        if (movementInput != Vector2.zero)
        {

            // Check for potential collisions
            Rb.AddForce(movementInput * moveSpeed * Time.deltaTime);
            if (Rb.velocity.magnitude > maxSpeed)
            {
                float limitedspeed = Mathf.Lerp(Rb.velocity.magnitude, maxSpeed, idleFriction);
                Rb.velocity = Rb.velocity.normalized * limitedspeed;
            }


            //footsteps sound plays
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }

            //flip sprite left or right depending where it moves
            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }


            IsMoving = true;

        }
        else
        {
            Rb.velocity = Vector2.Lerp(Rb.velocity, Vector2.zero, idleFriction);//speed of player
            IsMoving = false;


            //footsteps sound stops
            AudioSource.Stop();
        }
    }


   public void LoadGameData(SaveData data) 
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveGameData(ref SaveData data) 
    {
        data.playerPosition = this.transform.position;
    }



    //initialize movement Input
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }


   public  void OnAttackClicked()
    {
        animator.SetTrigger("attack");
    }


    //direction of attack
    public void AttackStart()
    {
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    //end of attack
    public void AttackEnd()
    {

        swordAttack.StopAttack();
    }


}

}
    
  
