using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 3f;
    public int damage = 50;

    [Header("Setup")]
    public string enemyTag = "Enemy";
    private Transform target;

    void Update()
    {
        
    }
}
