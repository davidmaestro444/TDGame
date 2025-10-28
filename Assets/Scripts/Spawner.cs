using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    public Path path;
    public PoolInfo[] pools;

    [Header("Waves")]
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    private int currentWaveIndex = 0;

    [System.Serializable]
    public class PoolInfo
    {
        public GameObject prefab;
        public ObjectPooler pool;
    }

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Spawning Wave: " + currentWave.name);

            yield return StartCoroutine(SpawnWave(currentWave));

            currentWaveIndex++;
            Debug.Log("Wave finished!");
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        List<EnemyGroup> spawnList = new List<EnemyGroup>();
        foreach (var group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                spawnList.Add(group);
            }
        }

        for (int i = 0; i < spawnList.Count; i++)
        {
            int randomIndex = Random.Range(i, spawnList.Count);
            EnemyGroup temp = spawnList[i];
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        foreach (var itemToSpawn in spawnList)
        {
            ObjectPooler pool = GetPoolForPrefab(itemToSpawn.enemyPrefab);
            if (pool == null)
            {
                Debug.LogError("Nincs pool beállítva ehhez a prefabhoz: " + itemToSpawn.enemyPrefab.name);
                continue;
            }

            GameObject enemy = pool.GetPooledObject();
            if (enemy != null)
            {
                enemy.transform.position = path.Waypoints[0].transform.position;
                enemy.SetActive(true);
            }

            yield return new WaitForSeconds(itemToSpawn.timeBetweenSpawns);
        }
    }

    ObjectPooler GetPoolForPrefab(GameObject prefab)
    {
        foreach (var info in pools)
        {
            if (info.prefab == prefab)
            {
                return info.pool;
            }
        }
        return null;
    }
}
