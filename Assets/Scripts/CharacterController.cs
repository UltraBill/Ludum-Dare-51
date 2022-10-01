using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;

        [SerializeField] private Transform m_GroundCheck;
        [SerializeField] private LayerMask m_WhatIsGround;

        const float k_GroundedRadius = .2f;
        private Rigidbody2D m_Rigidbody2D;
        private BaseCharacter m_Character;

        private float movementDirection;
        private uint numberJump;
        private bool m_jump;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Character = GetComponent<BaseCharacter>();

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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            if (colliders.Any())
            {
                numberJump = (uint)(1 + (m_Character.canDoubleJump ? 1 : 0));
            }
            else
            {
                numberJump = (uint)(m_Character.canDoubleJump ? 1 : 0);
            }

            // Move the player
            m_Rigidbody2D.velocity = new Vector2(movementDirection * m_Character.movementSpeed, m_Rigidbody2D.velocity.y);

            if (m_jump)
            {
                m_jump = false;

                m_Rigidbody2D.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }

        }
    }
}
