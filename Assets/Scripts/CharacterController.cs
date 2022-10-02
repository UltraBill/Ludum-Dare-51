using System;
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

        private float movementDirection;
        private uint numberJump;
        private bool m_jump;
        private bool m_animJumping;
        private bool flipped;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Character = GetComponent<BaseCharacter>();
            m_Animator = GetComponent<Animator>();

            numberJump = (uint)(1 + ((m_Character.canDoubleJump) ? 1 : 0));
        }

        void Update()
        {
            movementDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && numberJump > 0)
            {
                numberJump--;
                m_jump = true;
            }
        }

        private void FixedUpdate()
        {
            // Check if Grounded
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);

            if (colliders.Any())
            {
                numberJump = (uint)(1 + (m_Character.canDoubleJump ? 1 : 0));
            }
            else
            {
                if (m_animJumping) m_animJumping = false;
                numberJump = (uint)(m_Character.canDoubleJump ? 1 : 0);
            }

            // Move the player
            m_Rigidbody2D.velocity = new Vector2(movementDirection * m_Character.movementSpeed, m_Rigidbody2D.velocity.y);

            if (m_jump)
            {
                m_jump = false;
                m_animJumping = true;

                m_Rigidbody2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            }

            // Animator 
            m_Animator.SetFloat("Speed", Math.Abs(movementDirection));
            m_Animator.SetBool("Jumping", m_animJumping);

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
