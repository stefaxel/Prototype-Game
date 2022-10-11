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

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledPrefab("Basic Enemy");
        enemyCount = FindObjectsOfType<ObjectPooler>().Length;
        //Debug.Log("No. of enemies = " + enemyCount);
        if (enemyCount == 1)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            //Debug.Log("In the for loop, in spawn enemy wave method");
            GameObject enemy = ObjectPooler.SharedInstance.GetPooledPrefab("Basic Enemy");
            if (enemy != null)
            {
                //Debug.Log("Spawning an enemy");
                enemy.transform.position = GenerateSpawnPosition();
                enemy.SetActive(true);
            }
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
