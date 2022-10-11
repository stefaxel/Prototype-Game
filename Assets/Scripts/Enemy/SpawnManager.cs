using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Need to find a way of spawning enemies once the current enemies have been deactivated,
/// or to spawn them in waves as with one of the Unity Tutorials.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    //public static SpawnManager SharedInstance;
    //[SerializeField] List<GameObject> enemyPrefab;
    //public GameObject prefabToPool;
    //public int enemyCount;

    //public int waveNumber = 1;

    private float spawnRange = 13.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        SpawnEnemyWave();

    }

    void SpawnEnemyWave()
    {
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledPrefab("Basic Enemy");
        if (enemy != null)
        {
            enemy.transform.position = GenerateSpawnPosition();
            enemy.SetActive(true);
        }
    }

    //private int FindEnemy()
    //{
    //    for (int i = 0; i < enemyPrefab.Count; i++)
    //    {
    //        int index = enemyPrefab.Count;
    //        if (!enemyPrefab[index].activeInHierarchy)
    //        {
    //            return i;
    //        }
    //    }
    //    return 0;
    //}

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
