using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;

    private Path currentPath;
    private Vector3 targetPosition;
    private int currentWaypoint;

    private void Awake()
    {
        currentPath = GameObject.Find("Path").GetComponent<Path>();
    }

    private void OnEnable()
    {
        currentWaypoint = 0;
        targetPosition = currentPath.GetPosition(currentWaypoint);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, data.speed * Time.deltaTime);
        float relativeDistance = (transform.position - targetPosition).magnitude;
        if (relativeDistance < 0.1f)
        {
            if (currentWaypoint < currentPath.Waypoints.Length - 1)
            {
                currentWaypoint++;
                targetPosition = currentPath.GetPosition(currentWaypoint);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
