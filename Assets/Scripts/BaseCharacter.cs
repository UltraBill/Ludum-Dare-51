using Assets.Scripts.Passive;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Base values
    private const float b_movementSpeed = 5f;
    private const int b_maxDashNumber = 1;
    private const bool b_canDoubleJump = false;

    private const uint b_damage = 1;
    private const float b_range = 4;
    private const float b_areaOfEffectSize = 0;
    private const float b_criticalChance = 0.1f;

    // Passives list
    List<Passive> passivePool;
    Passive actualPassive;

    // UpdatedValues
    [NonSerialized] public float movementSpeed = b_movementSpeed;
    [NonSerialized] public int  maxDashNumber = b_maxDashNumber;
    [NonSerialized] public bool canDoubleJump = b_canDoubleJump;

    [NonSerialized] public uint  damage = b_damage;
    [NonSerialized] public float range = b_range;
    [NonSerialized] public float areaOfEffectSize = b_areaOfEffectSize;
    [NonSerialized] public float criticalChance = b_criticalChance;

    // Attack
    const float m_HitRadius = .4f;

    [SerializeField] private Transform m_EnemyHitCheck;
    [SerializeField] private LayerMask m_WhatIsEnemy;
    [SerializeField] private float m_cooldown = 0.5f;
    [SerializeField] private float m_chargedAttackTimer = 1f;

    private float nextAttackTimer;
    private float beginCharged;
    private bool isCharging;

    // Dash 

    [SerializeField] private float m_dashCooldown = 1f;
    [SerializeField] private float m_dashForce = 50f;

    private bool canDash = true;
    private float startedDash;
    private int actualDashNumber;

    // Others
    private Animator m_Animator;
    private Assets.Scripts.CharacterController m_Controller;

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

        UpdateVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_Animator.ResetTrigger("Attack");
            if (Time.time >= nextAttackTimer)
            {
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

    }

    private IEnumerator Dash()
    {
        movementSpeed *= m_dashForce;

        yield return new WaitForSeconds(0.25f);

        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
    }

    // Update statistic with the passive values
    void UpdateVariables()
    {
        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        maxDashNumber = actualPassive.MaxDashNumber ?? b_maxDashNumber;
        canDoubleJump = actualPassive.CanDoubleJump ?? b_canDoubleJump;

        damage = actualPassive.Damage ?? b_damage;
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

    void Attack(bool isHeavy = false)
    {
        // Animator
        m_Animator.ResetTrigger("HoldAttack");
        m_Animator.SetTrigger("Attack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_EnemyHitCheck.position, m_HitRadius, m_WhatIsEnemy);

        foreach (Collider2D enemy in colliders)
        {
            Debug.Log(damage * (uint)(isHeavy ? 4 : 1));

            enemy.GetComponent<BaseEnemy>()?.TakeDamage((int)damage * (isHeavy ? 4 : 1));
        }
    }

    private void OnDrawGizmos()
    {
        if (!m_EnemyHitCheck)
            return;

        Gizmos.DrawWireSphere(m_EnemyHitCheck.position, m_HitRadius);
    }

}
