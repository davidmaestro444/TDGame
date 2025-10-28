using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*private float spawnTimer;
    private float spawnInterval = 1f;

    [SerializeField] private ObjectPooler pool;

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnInterval;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject spawnedObject = pool.GetPooledObject();
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(true);
    }*/

    [Header("References")]
    public Path path; // Ide húzd be a Path objektumot
    public PoolInfo[] pools; // Itt adjuk meg, melyik prefab melyik poolhoz tartozik

    [Header("Waves")]
    public Wave[] waves; // Itt állítjuk be a hullámokat az Inspectorban
    public float timeBetweenWaves = 5f; // Mennyi szünet legyen a hullámok között

    private int currentWaveIndex = 0;

    // Egy segéd osztály, hogy az Inspectorban könnyû legyen párosítani
    [System.Serializable]
    public class PoolInfo
    {
        public GameObject prefab;
        public ObjectPooler pool;
    }

    void Start()
    {
        // Elindítjuk a hullámok kezelését
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        // Végigmegyünk az összes beállított hullámon
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Spawning Wave: " + currentWave.name);

            // Elindítjuk az aktuális hullám spawnolását
            yield return StartCoroutine(SpawnWave(currentWave));

            currentWaveIndex++;
            Debug.Log("Wave finished! Preparing for next wave...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        // Végigmegyünk a hullámban definiált összes ellenség-csoporton
        foreach (var group in wave.enemyGroups)
        {
            ObjectPooler pool = GetPoolForPrefab(group.enemyPrefab);
            if (pool == null)
            {
                Debug.LogError("Nincs pool beállítva ehhez a prefabhoz: " + group.enemyPrefab.name);
                continue; // Kihagyjuk ezt a csoportot, ha nincs poolja
            }

            // A csoportból annyi ellenséget spawnolunk, amennyi elõ van írva
            for (int i = 0; i < group.count; i++)
            {
                GameObject enemy = pool.GetPooledObject();
                if (enemy != null)
                {
                    // Az ellenséget a pálya elejére helyezzük
                    enemy.transform.position = path.Waypoints[0].transform.position;
                    enemy.SetActive(true);
                }

                // Várunk egy kicsit a következõ ellenség elõtt
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
    }

    // Ez a függvény megkeresi, hogy egy adott prefabhoz melyik pool tartozik
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
