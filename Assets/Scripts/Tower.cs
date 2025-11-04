using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 3f;
    public float fireRate = 1f;
    public int damage = 2;
    private float fireCountdown = 0f;

    [Header("Setup")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public GameObject area;
    private Transform target;
    private Camera mainCamera;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        mainCamera = Camera.main;
        if (IsMouseOverThis())
        {
            area.SetActive(true);
        }
        else
        {
            area.SetActive(false);
        }

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetDamage(damage);
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    bool IsMouseOverThis()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // Raycast csak akkor tér vissza true-val, ha ez az objektum van legfelül
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }

        return false;

    }
}
