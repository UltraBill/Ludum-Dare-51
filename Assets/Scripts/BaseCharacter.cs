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
    private const bool  b_canDoubleJump = false;

    private const uint  b_damage = 1;
    private const float b_range = 4;
    private const float b_areaOfEffectSize = 0;
    private const float b_criticalChance = 0.1f;

    // Passives list
    List<Passive> passivePool;
    Passive actualPassive;

    // UpdatedValues
    public uint  maxLifePoint = b_maxLifePoint;
    public float movementSpeed = b_movementSpeed;
    public uint  maxDashNumber = b_maxDashNumber;
    public bool  canDoubleJump = b_canDoubleJump;

    public uint  damage = b_damage;
    public float range = b_range;
    public float areaOfEffectSize = b_areaOfEffectSize;
    public float criticalChance = b_criticalChance;

    // Others
    public uint actualLifePoint;
    public uint actualDashNumber;


    // Start is called before the first frame update
    void Start()
    {
        passivePool = new List<Passive>()
        {
            new DoubleJumpPassive(), new SpeedPassive()
        };

        actualPassive = passivePool.First();

        actualLifePoint = maxLifePoint;
        actualDashNumber = maxDashNumber;

        UpdateVariables();

        InvokeRepeating(nameof(ChangePassive), 0f, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    // Update statistic with the passive values
    void UpdateVariables()
    {
        maxLifePoint = actualPassive.MaxLifePoint ?? b_maxLifePoint;
        movementSpeed = actualPassive.MovementSpeed ?? b_movementSpeed;
        maxDashNumber = actualPassive.MaxDashNumber ?? b_maxDashNumber;
        canDoubleJump = actualPassive.CanDoubleJump ?? b_canDoubleJump;

        damage = actualPassive.Damage ?? b_damage;
        range = actualPassive.Range ?? b_range;
        areaOfEffectSize = actualPassive.AreaOfEffectSize ?? b_areaOfEffectSize;
        criticalChance = actualPassive.CriticalChance ?? b_criticalChance;
        
    }

    public uint GetMaxLifePoint()
    {
        return maxLifePoint;
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
