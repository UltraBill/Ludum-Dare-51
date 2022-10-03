using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public int numberMaxEnemy = 5;
    public int maxEnemyAtSameTime = 2;

    public List<GameObject> enemyPool;
    public List<GameObject> bossPool;

    private int numberGenerated;
    private int numberEnemyOnMap;
    private bool bossGenerated = false;

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
            if (enemyPool.Any())
            {
                Instantiate(enemyPool[Random.Range(0, enemyPool.Count)], transform.position, Quaternion.identity);
                numberGenerated++;
            }
        }

        else if (numberGenerated >= numberMaxEnemy && numberEnemyOnMap <= 0 && !bossGenerated)
        {
            if (bossPool.Any())
            {
                bossGenerated = true;
                Instantiate(bossPool[Random.Range(0, bossPool.Count)], transform.position, Quaternion.identity);
            }
        }
    }
}
