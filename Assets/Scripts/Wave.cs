using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public EnemyGroup[] enemyGroups;
}

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int count;
    public float timeBetweenSpawns = 1f;
}
