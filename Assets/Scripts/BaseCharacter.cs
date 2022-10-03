using Assets.Scripts.Passive;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Base values
    [Header("Base Values")]
    public const float b_movementSpeed = 7f;
    public const int b_maxDashNumber = 1;
    public const bool b_canDoubleJump = false;
  
    public const int b_damage = 2;
    public const int b_heavyDamageMultiplicator = 2;
    public const int b_armorPoint = 2;
    public const float b_range = 4;
    public const float b_areaOfEffectSize = 0;
    public const float b_criticalChance = 0.1f;

    // Passives list
    List<Passive> passivePool;
    Passive actualPassive;

    // UpdatedValues
    [NonSerialized] public float movementSpeed = b_movementSpeed;
    [NonSerialized] public bool canDoubleJump = b_canDoubleJump;
    [NonSerialized] public int maxDashNumber = b_maxDashNumber;
    [NonSerialized] public int armorPoint = b_armorPoint;


    [NonSerialized] public int damage = b_damage;
    [NonSerialized] public int heavyDamageMultiplicator = b_heavyDamageMultiplicator;
    [NonSerialized] public float range = b_range;
    [NonSerialized] public float areaOfEffectSize = b_areaOfEffectSize;
    [NonSerialized] public float criticalChance = b_criticalChance;

    // Attack
    [Header("Attack")]
    [SerializeField] private float m_HitRadius = .4f;
    [SerializeField] private Transform m_EnemyHitCheck;
    [SerializeField] private LayerMask m_WhatIsEnemy;
    [SerializeField] private float m_cooldown = 0.5f;
    [SerializeField] private float m_chargedAttackTimer = 1f;

    private float nextAttackTimer;
    private float beginCharged;
    private bool isCharging;
    private bool isDashing;
    private bool notifyEndCharge = false;

    // Dash 

    [SerializeField] private float m_dashCooldown = 1f;
    [SerializeField] private float m_dashForce = 50f;

    private bool canDash = true;
    private float startedDash;
    private int actualDashNumber;

    // Others
    private Animator m_Animator;
    private Assets.Scripts.CharacterController m_Controller;
    private AudioSource m_source;

    [NonSerialized] public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        passivePool = new List<Passive>()
        {
            new DoubleJumpPassive(), new SpeedPassive()
        };

        actualPassive = passivePool.First();

        actualDashNumber = maxDashNumber;

        m_Animator = GetComponent<Animator>();
        m_Controller = GetComponent<Assets.Scripts.CharacterController>();
        m_source = GetComponents<AudioSource>()[1];

        UpdateVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_Animator.ResetTrigger("Notification");
                m_Animator.ResetTrigger("Attack");
                m_Animator.ResetTrigger("HeavyAttack");
                if (Time.time >= nextAttackTimer)
                {
                    notifyEndCharge = true;
                    isCharging = true;
                    beginCharged = Time.time;

                    m_Animator.SetTrigger("HoldAttack");
                }
                else if (!isCharging)
                {
                    beginCharged = Time.time + 99999999f;
                }
            }

            if (Input.GetButtonUp("Fire1") && Time.time >= nextAttackTimer && isCharging)
            {
                notifyEndCharge = false;
                isCharging = false;
                nextAttackTimer = Time.time + m_cooldown;

                if (Time.time > beginCharged + m_chargedAttackTimer)
                {
                    Attack(true);
                }
                else
                {
                    Attack();
                }
            }

            if (Time.time >= beginCharged + m_chargedAttackTimer && notifyEndCharge)
            {
                m_Animator.SetTrigger("Notification");
            }

            // Dash
            if (Input.GetButtonDown("Fire2") && actualDashNumber > 0 && canDash)
            {
                canDash = false;
                actualDashNumber--;
                startedDash = Time.time;

                StartCoroutine(Dash());
            }

            if (Time.time > startedDash + m_dashCooldown)
            {
                canDash = true;
            }

            // if out of bound, die
            if (transform.position.y < -10f)
            {
                isDead = true;
            }
        }
    }

    private IEnumerator Dash()
    {
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        movementSpeed *= m_dashForce;
        col.enabled = false;
        rb.gravityScale = 0.1f;

        rb.velocity = new Vector2(rb.velocity.x, 0);

        yield return new WaitForSeconds(0.25f);

        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        col.enabled = true;
        rb.gravityScale = 4f;
    }

    // Update statistic with the passive values
    void UpdateVariables()
    {
        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        maxDashNumber = actualPassive.MaxDashNumber ?? b_maxDashNumber;
        canDoubleJump = actualPassive.CanDoubleJump ?? b_canDoubleJump;

        damage = actualPassive.Damage ?? b_damage;
        heavyDamageMultiplicator = actualPassive.HeavyDamageMultiplicator ?? b_heavyDamageMultiplicator;
        range = actualPassive.Range ?? b_range;
        areaOfEffectSize = actualPassive.AreaOfEffectSize ?? b_areaOfEffectSize;
        criticalChance = actualPassive.CriticalChance ?? b_criticalChance;
    }

    public void ChangePassive()
    {
        actualDashNumber = maxDashNumber;

        int r = UnityEngine.Random.Range(0, passivePool.Count);

        actualPassive = passivePool[r];

        UpdateVariables();

    }

    public Sprite GetPassiveSprite()
    {
        return actualPassive.GetSprite();
    }

    void Attack(bool isHeavy = false)
    {
        // Animator
        m_Animator.ResetTrigger("Notification");
        m_Animator.ResetTrigger("HoldAttack");
        m_source.Play();

        if (isHeavy)
            m_Animator.SetTrigger("HeavyAttack");
        else
            m_Animator.SetTrigger("Attack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_EnemyHitCheck.position, m_HitRadius, m_WhatIsEnemy);

        foreach (Collider2D enemy in colliders)
        {
            enemy.GetComponent<BaseEnemy>()?.TakeDamage((int)damage * (isHeavy ? 2 : 1));
        }
    }

    public void Damage()
    {
        if (!isDashing)
        {
            if (armorPoint > 0)
            {
                armorPoint--;
                m_Animator.SetTrigger("Damage");
            }
            else
            {
                m_Animator.SetBool("isDead", true);
                isDead = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!m_EnemyHitCheck)
            return;

        Gizmos.DrawWireSphere(m_EnemyHitCheck.position, m_HitRadius);
    }

}
