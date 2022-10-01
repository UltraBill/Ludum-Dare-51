using Assets.Scripts.Passive;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Base values
    private const uint  b_maxLifePoint = 5;
    private const float b_movementSpeed = 5f;
    private const uint  b_maxDashNumber = 1;

    private const uint  b_damage = 1;
    private const float b_range = 4;
    private const float b_areaOfEffectSize = 0;
    private const float b_criticalChance = 0.1f;

    // Passives list
    List<Passive> passivePool;
    Passive actualPassive;

    // UpdatedValues
    private uint  u_maxLifePoint = b_maxLifePoint;
    private float u_movementSpeed = b_movementSpeed;
    private uint  u_maxDashNumber = b_maxDashNumber;

    private uint  u_damage = b_damage;
    private float u_range = b_range;
    private float u_areaOfEffectSize = b_areaOfEffectSize;
    private float u_criticalChance = b_criticalChance;

    // Others
    public uint actualLifePoint;
    public uint actualDashNumber;

    public Rigidbody2D rigidBody;
    private Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        passivePool = new List<Passive>()
        {
            new SpeedPassive(), new DamagePassive(), new MaxHealthPassive()
        };

        actualPassive = passivePool.First();

        actualLifePoint = u_maxLifePoint;
        actualDashNumber = u_maxDashNumber;

        movementDirection = new Vector2();

        UpdateVariables();

        InvokeRepeating(nameof(ChangePassive), 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movementDirection * u_movementSpeed * Time.fixedDeltaTime);
    }

    // Update statistic with the passive values
    void UpdateVariables()
    {
        u_maxLifePoint = actualPassive.MaxLifePoint ?? b_maxLifePoint;
        u_movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        u_maxDashNumber = actualPassive.MaxDashNumber ?? b_maxDashNumber;

        u_damage = actualPassive.Damage ?? b_damage;
        u_range = actualPassive.Range ?? b_range;
        u_areaOfEffectSize = actualPassive.AreaOfEffectSize ?? b_areaOfEffectSize;
        u_criticalChance = actualPassive.CriticalChance ?? b_criticalChance;
    }

    public uint GetMaxLifePoint()
    {
        return u_maxLifePoint;
    }

    // Call every 10 seconds 
    private void ChangePassive()
    {
        Debug.Log(passivePool.Count);

        int r = Random.Range(0, passivePool.Count );

        Debug.Log(r);

        actualPassive = passivePool[r];

        Debug.Log(actualPassive);

        UpdateVariables();

    }
}
