using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public BaseBoss boss;
    public int numberMaxEnemy = 5;
    public int maxEnemyAtSameTime = 2;

    public List<GameObject> enemyPool;
    public List<GameObject> bossPool;

    private int numberGenerated;
    private int numberEnemyOnMap;
    private bool bossGenerated = false;

    void Start()
    {
        boss.gameObject.SetActive(false);
        numberMaxEnemy = Random.Range(15, 20);
        maxEnemyAtSameTime = 3;
        bossGenerated = false;

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
                boss.gameObject.SetActive(true);
                boss.transform.position = transform.position;
                Debug.Log("Spawn Boss");

                //Instantiate(bossPool[Random.Range(0, bossPool.Count)], transform.position, Quaternion.identity);
            }
        }
    }

    public void Reset()
    {
        numberMaxEnemy = Random.Range(15, 20);
        maxEnemyAtSameTime = 3;

        numberGenerated = 0;
        bossGenerated = false;
        boss.Reset();
        boss.gameObject.SetActive(false);

        Debug.Log("Reset SpawnManager");
    }
}
