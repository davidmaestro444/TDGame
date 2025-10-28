using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public EnemyGroup[] enemyGroups;
    public float timeBetweenSpawns = 0.5f;
}

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int count;
}
