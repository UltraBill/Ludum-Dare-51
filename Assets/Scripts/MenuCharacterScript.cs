using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterScript : MonoBehaviour
{
    private float m_dir;
    public float speed = 1f;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_dir = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(speed * m_dir, 0, 0);
        m_Animator.SetFloat("Speed", Math.Abs(m_dir));

        if (m_dir < -0.1)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
        else if (m_dir > 0.1)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
    }
}
