using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefab;

    public int waveNumber = 1;
    public int enemyCount = 0;
    private float spawnRange = 13.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = enemyPrefab.Count;
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave();
        }
    }

    void SpawnEnemyWave()
    {
        for(int i = 0; i < FindEnemy(); i++)
        {
            Instantiate(enemyPrefab[FindEnemy()], GenerateSpawnPosition(), enemyPrefab[FindEnemy()].transform.rotation);
        }
    }

    //Need to find a way of implementing object pooling for spawning enemies
    
    private int FindEnemy()
    {
        for(int i = 0; i < enemyPrefab.Count; i++)
        {
            int index = enemyPrefab.Count;
            if (!enemyPrefab[index].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
