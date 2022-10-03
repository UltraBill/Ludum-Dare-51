using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Passive;

public class BaseBoss : MonoBehaviour
{
    [Header("Base Attribute")]
    [SerializeField] private int maxLifePoint = 10;
    private int actualLifePoint;

    [Header("Player Detection")]
    [SerializeField] private float m_TargetDetectionRadius;
    [SerializeField] private LayerMask m_WhatIsTarget;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;

    [Header("Attack System")]
    [SerializeField] private float m_CanAttackRadius;
    [SerializeField] private float m_attackChargeTime = 0.5f;
    [SerializeField] private Transform m_HitPoint;
    [SerializeField] private float m_HitRadius;

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

    [NonSerialized] public float movementSpeed = b_movementSpeed;
    [NonSerialized] public bool canDoubleJump = b_canDoubleJump;
    [NonSerialized] public int maxDashNumber = b_maxDashNumber;
    [NonSerialized] public int armorPoint = b_armorPoint;


    [NonSerialized] public int damage = b_damage;
    [NonSerialized] public int heavyDamageMultiplicator = b_heavyDamageMultiplicator;
    [NonSerialized] public float range = b_range;
    [NonSerialized] public float areaOfEffectSize = b_areaOfEffectSize;
    [NonSerialized] public float criticalChance = b_criticalChance;

    private bool isDead;
    private bool isAttacking;
    private bool flipped;
    private Vector3 direction;
    private Animator animator;
    private Rigidbody2D m_Rigidbody2D;
    private int jumpcount;

    // Passives list
    List<Passive> passivePool;
    Passive actualPassive;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        actualLifePoint = maxLifePoint;

        passivePool = new List<Passive>()
        {
            new SpeedPassive(), new DoubleJumpPassive(),
        };

        actualPassive = passivePool.First();
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Damage");
        actualLifePoint -= damage;

        if (actualLifePoint <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        
        animator.SetBool("isDead", true);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void FixedUpdate()
    {
        Vector3 dest = Vector3.zero;

        if (!isDead)
        {
            float speed = 0f;

            Collider2D[] seePlayer = Physics2D.OverlapCircleAll(transform.position, m_TargetDetectionRadius, m_WhatIsTarget);
            Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(transform.position, m_CanAttackRadius, m_WhatIsTarget);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, .2f, m_WhatIsGround);

            if (colliders.Any())
            {
                jumpcount = canDoubleJump ? 2 : 1;
            }

            if (seePlayer.Any())
            {
                var player = seePlayer.First();

                direction = (player.transform.position - transform.position).normalized;            

                speed = movementSpeed * (isAttacking ? 0 : 1);

                // dest = transform.position + direction * speed;

                // transform.position = dest;

                m_Rigidbody2D.velocity = new Vector2(direction.x * speed, m_Rigidbody2D.velocity.y);

                if (Mathf.Abs(direction.y) >  0.3f && jumpcount > 0)
                {
                    m_Rigidbody2D.AddForce(Vector2.up * 30f, ForceMode2D.Impulse);
                    jumpcount--;
                }
            }
            
            if ( attackPlayer.Any() && !isAttacking)
            {
                isAttacking = true;
                Invoke("Attack", m_attackChargeTime);
            }

            // Turn the enemy in the right direction
            if (direction.x < 0.1 && !flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                flipped = true;
            }
            else if (direction.x > 0.1 && flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                flipped = false;
            }

            // Animator
            animator.SetFloat("Speed", speed);
            animator.SetBool("Jumping", (canDoubleJump && jumpcount < 2) || jumpcount == 0);
        }
    }

    private void Attack()
    {
        animator.ResetTrigger("Notification");
        animator.ResetTrigger("HoldAttack");
        animator.SetTrigger("Attack");

        Collider2D[] attackedPlayer = Physics2D.OverlapCircleAll(m_HitPoint.position, m_HitRadius, m_WhatIsTarget);

        if (attackedPlayer.Any())
        {
            var player = attackedPlayer.First();
            player.GetComponent<BaseCharacter>().Damage();
        }

        isAttacking = false;
    }

    void UpdateVariables()
    {
        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        canDoubleJump = actualPassive.CanDoubleJump ?? b_canDoubleJump;

        damage = actualPassive.Damage ?? b_damage;
        heavyDamageMultiplicator = actualPassive.HeavyDamageMultiplicator ?? b_heavyDamageMultiplicator;
        range = actualPassive.Range ?? b_range;
        areaOfEffectSize = actualPassive.AreaOfEffectSize ?? b_areaOfEffectSize;
        criticalChance = actualPassive.CriticalChance ?? b_criticalChance;
    }

    public void ChangePassive()
    {
        int r = UnityEngine.Random.Range(0, passivePool.Count);

        actualPassive = passivePool[r];

        UpdateVariables();

    }

    private void OnDrawGizmos()
    {
        // Draw detection circle
        Gizmos.DrawWireSphere(transform.position, m_TargetDetectionRadius);
        Gizmos.DrawWireSphere(transform.position, m_CanAttackRadius);

        // Draw attack range
        if (!m_HitPoint)
            return;

        Gizmos.DrawWireSphere(m_HitPoint.position, m_HitRadius);

    }

}
