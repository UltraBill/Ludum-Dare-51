using Assets.Scripts.Passive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDrop : MonoBehaviour
{
    public List<Passive> passivesPool;

    [SerializeField] private GameObject m_TooltipObject;
    [SerializeField] private GameObject m_takeSprite;
    [NonSerialized] public Passive passive;

    GameObject player = null;
    private bool canPickup = false;

    void Start()
    {
        
        passivesPool = new List<Passive>()
        {
            new DoubleJumpPassive(), new SpeedPassive(), new DamagePassive(), new ShieldPassive()
        };

        passive = passivesPool[UnityEngine.Random.Range(0, passivesPool.Count)];

        m_takeSprite.SetActive(false);
    }

    private void Update()
    {
        submit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;

            canPickup = true;
            m_takeSprite.SetActive(canPickup);

            Tooltip tt = m_TooltipObject.GetComponent<Tooltip>();

            if (tt)
            {
                tt.Show(passive);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
            canPickup = false;

            m_takeSprite.SetActive(canPickup);
            Tooltip tt = m_TooltipObject.GetComponent<Tooltip>();

            if (tt)
            {
                tt.Hide();
            }
        }
    }

    protected virtual void submit()
    {
        if (Input.GetButton("Submit") && canPickup && player)
        {
            player.GetComponent<BaseCharacter>().AddPassivePool(passive);

            this.gameObject.SetActive(false);

            //Destroy(this);
        }
    }
}
