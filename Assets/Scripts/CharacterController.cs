﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Transform m_GroundCheck;
        [SerializeField] private LayerMask m_WhatIsGround;
        [SerializeField] private float m_jumpForce = 35f;


        const float m_GroundedRadius = .2f;

        private Rigidbody2D m_Rigidbody2D;
        private BaseCharacter m_Character;
        private Animator m_Animator;
        private AudioSource m_source;

        private float movementDirection;
        private int maxJump;
        private int usedJump;

        private bool isJumping;
        private bool m_animJumping;
        private bool flipped;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Character = GetComponent<BaseCharacter>();
            m_Animator = GetComponent<Animator>();
            m_source = GetComponent<AudioSource>();
        }

        void Update()
        {
            maxJump = 1 + (m_Character.canDoubleJump ? 1 : 0);
            movementDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && usedJump < maxJump)
            {
                isJumping = true;
            }
        }

        private void FixedUpdate()
        {

            // Check if Grounded
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);

            if (colliders.Any())
            {
                usedJump = 0;

                if (m_animJumping) 
                    m_animJumping = false;
            }

            // Move the player
            m_Rigidbody2D.velocity = new Vector2(movementDirection * m_Character.movementSpeed, m_Rigidbody2D.velocity.y);

            if (isJumping)
            {
                isJumping = false;
                m_animJumping = true;

                m_Rigidbody2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
                usedJump++;
            }

            // Animator 
            m_Animator.SetFloat("Speed", Math.Abs(movementDirection));
            m_Animator.SetBool("Jumping", m_animJumping);

            if (Math.Abs(movementDirection) > 0)
            {
                if (!m_source.isPlaying)
                {
                    if (!m_animJumping)
                        m_source.Play();
                }

                if (m_animJumping) m_source.Stop();
            }else {
                if (m_source.isPlaying)
                {
                    m_source.Stop();
                }
            }

            // Turn the player
            if (movementDirection < -0.1 && !flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                flipped = true;
            }
            else if (movementDirection > 0.1 && flipped)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                flipped = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (!m_GroundCheck)
                return;

            Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundedRadius);
        }
    }
}
