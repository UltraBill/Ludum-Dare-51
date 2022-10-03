using System;
using System.Linq;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Base Attribute")]
    [SerializeField] private int maxLifePoint = 10;
    [SerializeField] private float m_MoveSpeed = 5;
    private int actualLifePoint;

    [Header("Player Detection")]
    [SerializeField] private float m_TargetDetectionRadius;
    [SerializeField] private LayerMask m_WhatIsTarget;

    [Header("Attack System")]
    [SerializeField] private float m_CanAttackRadius;
    [SerializeField] private float m_attackChargeTime = 0.5f;
    [SerializeField] private Transform m_HitPoint;
    [SerializeField] private float m_HitRadius;

    private bool isDead;
    private bool isAttacking;
    private bool flipped;
    private Vector3 direction;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        actualLifePoint = maxLifePoint;
    }

    public void TakeDamage(int damage)
    {
        actualLifePoint -= damage;

        if (actualLifePoint <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        
        animator.SetTrigger("Death");
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

            if (seePlayer.Any())
            {
                var player = seePlayer.First();

                speed = m_MoveSpeed * Time.deltaTime * (isAttacking ? 0 : 1);
                dest = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y), speed);

                direction = (transform.position - player.transform.position).normalized;

                transform.position = dest;
            }
            
            if ( attackPlayer.Any() && !isAttacking)
            {
                isAttacking = true;
                Invoke("Attack", m_attackChargeTime);
            }

            // Turn the enemy in the right direction
            if (direction.x > 0.1 && !flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                flipped = true;
            }
            else if (direction.x < 0.1 && flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                flipped = false;
            }

            // Animator
            animator.SetFloat("Speed", speed);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] attackedPlayer = Physics2D.OverlapCircleAll(m_HitPoint.position, m_HitRadius, m_WhatIsTarget);

        if (attackedPlayer.Any())
        {
            var player = attackedPlayer.First();
            player.GetComponent<BaseCharacter>().Damage();
        }

        isAttacking = false;
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
