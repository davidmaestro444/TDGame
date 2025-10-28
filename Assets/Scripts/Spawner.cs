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
    public Path path; // Ide h�zd be a Path objektumot
    public PoolInfo[] pools; // Itt adjuk meg, melyik prefab melyik poolhoz tartozik

    [Header("Waves")]
    public Wave[] waves; // Itt �ll�tjuk be a hull�mokat az Inspectorban
    public float timeBetweenWaves = 5f; // Mennyi sz�net legyen a hull�mok k�z�tt

    private int currentWaveIndex = 0;

    // Egy seg�d oszt�ly, hogy az Inspectorban k�nny� legyen p�ros�tani
    [System.Serializable]
    public class PoolInfo
    {
        public GameObject prefab;
        public ObjectPooler pool;
    }

    void Start()
    {
        // Elind�tjuk a hull�mok kezel�s�t
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        // V�gigmegy�nk az �sszes be�ll�tott hull�mon
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Spawning Wave: " + currentWave.name);

            // Elind�tjuk az aktu�lis hull�m spawnol�s�t
            yield return StartCoroutine(SpawnWave(currentWave));

            currentWaveIndex++;
            Debug.Log("Wave finished! Preparing for next wave...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        // V�gigmegy�nk a hull�mban defini�lt �sszes ellens�g-csoporton
        foreach (var group in wave.enemyGroups)
        {
            ObjectPooler pool = GetPoolForPrefab(group.enemyPrefab);
            if (pool == null)
            {
                Debug.LogError("Nincs pool be�ll�tva ehhez a prefabhoz: " + group.enemyPrefab.name);
                continue; // Kihagyjuk ezt a csoportot, ha nincs poolja
            }

            // A csoportb�l annyi ellens�get spawnolunk, amennyi el� van �rva
            for (int i = 0; i < group.count; i++)
            {
                GameObject enemy = pool.GetPooledObject();
                if (enemy != null)
                {
                    // Az ellens�get a p�lya elej�re helyezz�k
                    enemy.transform.position = path.Waypoints[0].transform.position;
                    enemy.SetActive(true);
                }

                // V�runk egy kicsit a k�vetkez� ellens�g el�tt
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
    }

    // Ez a f�ggv�ny megkeresi, hogy egy adott prefabhoz melyik pool tartozik
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
