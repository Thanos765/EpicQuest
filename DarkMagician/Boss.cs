using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;





public class Boss : MonoBehaviour, IDamageable, ItriggerCheckable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public float invincibilityTime = 0.25f;
    public bool disableSimulation = false;
    private float invincibleTimeElapsed = 0f;
    public GameObject healthText;
    Animator animator;
    public bool canTurnInvincible = false;
    SpriteRenderer sr;
    [SerializeField] private BossHealthSO currentHealth;
    [SerializeField] private float maxHealth = 40;
    public bool targetable = true;
    public bool invincible = false;
     public Transform target;
       private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BossHealthUI healthBarUI;
    Animator anim;
   StaffAttack staffAttack;
  StaffAttack2 staffAttack2;
    public bool isDefeated = false;


    public bool isAggroed{get;set;}
public bool isInAttackDistance{get;set;}





#region StateMachine Variables
    public StateMachineManager stateMachineManager {get;set;}
    public AttackState attackState{set;get;}

    public IdleState idleState {set;get;}

    public RunState runState{set;get;}

#endregion




#region Idle Variables

    public bool isFacingRight{get;set;} = true;

    #endregion




    private void Awake()
    {
       
        stateMachineManager = new StateMachineManager();
        idleState = new IdleState(this, stateMachineManager);
        runState = new RunState(this, stateMachineManager);
        attackState = new AttackState(this, stateMachineManager);
    }


private void Start(){
     rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        currentHealth.Value = 1f;
        sr = GetComponent<SpriteRenderer>();
        stateMachineManager.Initialize(idleState);
        anim = GetComponent<Animator>();
        staffAttack =GetComponentInChildren<StaffAttack>();
        staffAttack2 =GetComponentInChildren<StaffAttack2>();
}



    public float Health {

        set
        {
            if (currentHealth.Value < maxHealth)
            {
                animator.SetTrigger("Damaged");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    textTransform.SetParent(canvas.transform);
                }
            }

            currentHealth.Value = value;

            if (currentHealth.Value < 0.01)
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


    public bool Targetable {
        get { return targetable; }
        set
        {

            targetable = value;
            if (disableSimulation)
            {
                rb.simulated = false;
            }
            physicsCollider.enabled = value;
        }


    }
    public bool Invincible {
        get
        {
            return invincible;

        }
        set
        {
            invincible = value;
            if (invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
            Debug.Log(Invincible);
        }


    }


    public void OnHit(float damage, Vector2 knockback)
    {
        Debug.Log("Reduce method called. Damage: " + damage);

        if (!Invincible)
        {
            if (maxHealth != 0)
            {
                Health -= damage / maxHealth;
            }
            currentHealth.Value = Health;
            currentHealth.Value = Mathf.Clamp01(currentHealth.Value);
            UpdateBossHealthBarUI();
            rb.AddForce(knockback, ForceMode2D.Impulse);



            if (canTurnInvincible)
            {
                // activate invincibility timer
                Invincible = true;
            }
            if (currentHealth.Value <= 0)
            {
                isDefeated = true;
                Debug.Log("dark defeated");
            }
        }
    }


    public void OnHit(float damage)
    {
    Debug.Log("Reduce method called. Damage: " + damage);

    if (!Invincible)
    {
        Health -= damage / maxHealth;
        currentHealth.Value = Health;
        currentHealth.Value = Mathf.Clamp01(currentHealth.Value);
        UpdateBossHealthBarUI();


        if (canTurnInvincible)
        {
            // activate invincibility timer
            Invincible = true;
        }

    }
}

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    private void UpdateBossHealthBarUI()
    {
        healthBarUI.SetValue(currentHealth.Value);
    }


public void MoveBoss(Vector2 velocity){
   rb.velocity = velocity;
   CheckForLeftOrRightFacing(velocity);
}



public void CheckForLeftOrRightFacing(Vector2 velocity){
    if(isFacingRight && velocity.x <0f){
        Vector3 rotator = new Vector3(transform.rotation.x , 180f, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        isFacingRight=false;
    }

else if(!isFacingRight && velocity.x>0f){
    Vector3 rotator = new Vector3 (transform.rotation.x , 0, transform.rotation.z);
    transform.rotation = Quaternion.Euler(rotator);
        isFacingRight=true;

    }
}



private void Update(){
    stateMachineManager.currentEnemyState.FrameUpdate();
       
    }

    private void FixedUpdate()
    {

        stateMachineManager.currentEnemyState.PhysicsUpdate();
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }

    }


public void SetAggroStatus(bool IsAggroed){
  isAggroed = IsAggroed;

}


public void SetAttackDistanceBool(bool isWithinAttackDistance){
   isInAttackDistance  = isWithinAttackDistance;
}

    public void StartAttack()
    {
        staffAttack.Attack();
    }

    public void StopAttack()
    {
        staffAttack.StopAttack();
        staffAttack.ResetCooldown();
        // Reset the delay timer between attacks
    }


 public void StartAttack2()
    {
        staffAttack2.Attack2();
    }

    public void StopAttack2()
    {
        staffAttack2.StopAttack2();
        staffAttack2.ResetCooldown2();
        // Reset the delay timer between attacks
    }


}


