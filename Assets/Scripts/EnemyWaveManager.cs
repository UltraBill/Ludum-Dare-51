using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private int numberMaxEnemy;
    private int numberGenerated;
    private int numberEnemyOnMap;
    private int maxEnemyAtSameTime;

    public List<GameObject> enemyPool;

    void Start()
    {
        numberMaxEnemy = Random.Range(15, 20);
        maxEnemyAtSameTime = 3;

        numberGenerated = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        numberEnemyOnMap = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (numberGenerated < numberMaxEnemy && numberEnemyOnMap <= maxEnemyAtSameTime)
        {
            Instantiate(enemyPool[Random.Range(0, enemyPool.Count)], transform.position, Quaternion.identity );
            numberGenerated++;
        }
    }
}
