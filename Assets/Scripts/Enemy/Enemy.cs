using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private float currentHealth;
    private Path currentPath;
    private Vector3 targetPosition;
    private int currentWaypoint;
    public GameObject healthBarPrefab;
    public Vector3 healthBarOffset = new Vector3(0, 1.2f, 0);
    private HealthBar healthBar;

    private void Awake()
    {
        currentPath = GameObject.Find("Path").GetComponent<Path>();
    }

    private void OnEnable()
    {
        currentHealth = data.lives;
        if (healthBar == null)
        {
            GameObject hb = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
            healthBar = hb.GetComponent<HealthBar>();
        }
        healthBar.UpdateHealthBar(currentHealth, data.lives);
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
                GameManager.instance.TakeDamage(data.damage);
                gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        healthBar.UpdateHealthBar(currentHealth, data.lives);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.AddMoney(data.moneyOnKill);
        gameObject.SetActive(false);
    }
}
