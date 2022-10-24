using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// enemy count needs to be populated from the object pooler of enemies, this should increase with each new wave.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 13.0f;
    public int waveNumber = 1;
    public int enemyCount;
    private GameObject[] allEnemy;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        //Debug.Log("No. of enemies = " + enemyCount);
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy1 = ObjectPooler.SharedInstance.GetPooledPrefab("Basic Enemy");
            if (enemy1 != null)
            {
                enemy1.transform.position = GenerateSpawnPosition();
                enemy1.SetActive(true);
            }
        }
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy2 = ObjectPooler.SharedInstance.GetPooledPrefab("Fast Enemy");
            if (enemy2 != null)
            {
                enemy2.transform.position = GenerateSpawnPosition();
                enemy2.SetActive(true);
            }
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.25f, spawnPosZ);

        return randomPos;
    }
}
