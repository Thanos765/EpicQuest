using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;





public class Boss2 : MonoBehaviour, IDamageable, ItriggerCheckable
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

    [SerializeField]
    private BossHealthUI healthBarUI;
    Animator anim;
    FireAttack FireAttack;
    public bool isDefeated;

    public bool isAggroed { get; set; }
    public bool isInAttackDistance { get; set; }





    #region StateMachine Variables
    public StateMachineManager2 stateMachineManager2 { get; set; }
    public AttackState2 attackState2 { set; get; }

    public IdleState2 idleState2 { set; get; }

    public RunState2 runState2{ set; get; }

    #endregion




    #region Idle Variables

    public bool isFacingRight { get; set; } = true;

    #endregion




    private void Awake()
    {

        stateMachineManager2 = new StateMachineManager2();
        idleState2 = new IdleState2(this, stateMachineManager2);
        runState2 = new RunState2(this, stateMachineManager2);
        attackState2 = new AttackState2(this, stateMachineManager2);

    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        currentHealth.Value = 1f;
        sr = GetComponent<SpriteRenderer>();
        stateMachineManager2.Initialize(idleState2);
        anim = GetComponent<Animator>();
        FireAttack = GetComponentInChildren<FireAttack>();
    }



    public float Health
    {

        set
        {
            if (currentHealth.Value < maxHealth)
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


    public bool Targetable
    {
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
    public bool Invincible
    {
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
                Debug.Log("fire defeated");
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


    public void MoveBoss(Vector2 velocity)
    {
        rb.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }



    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
        }

        else if (!isFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;

        }
    }



    private void Update()
    {
        stateMachineManager2.currentEnemyState.FrameUpdate();

    }

    private void FixedUpdate()
    {

        stateMachineManager2.currentEnemyState.PhysicsUpdate();
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }

    }


    public void SetAggroStatus(bool IsAggroed)
    {
        isAggroed = IsAggroed;

    }


    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        isInAttackDistance = isWithinAttackDistance;
    }

    public void Boss2StartAttack()
    {
        FireAttack.Attack();
    }

    public void Boss2StopAttack()
    {
        FireAttack.StopAttack();
        FireAttack.ResetCooldown();
        // Reset the delay timer between attacks
    }



}
