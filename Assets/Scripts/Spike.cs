using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 3f;
    public int damage = 50;
    public int robotCost = 10;

    [Header("Setup")]
    public string enemyTag = "Enemy";
    private Transform target;
    public GameObject paths;
    public Camera mainCamera;
    GameObject currentRobot;


    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);

        //if (paths != null)
        //{
        //    paths.SetActive(false);
        //}
    }

    //void UpdateTarget()
    //{
        
    //}

    void Update()
    {
        if (currentRobot == null) return;
        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentRobot.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceSpikeRobot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    void PlaceSpikeRobot()
    {
        int placingPathLayer = LayerMask.NameToLayer("Tower");
        int layerMask = ~(1 >> placingPathLayer);
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.CompareTag("Child street"))
        {
            currentRobot.layer = LayerMask.NameToLayer("Default");
            currentRobot.transform.position = hit.collider.transform.position;

            GameManager.instance.SpendMoney(robotCost);
            Debug.Log("Spike lerakva");
            currentRobot = null;
        }
        else
        {
            Debug.Log("Nem lehet lerakni valamiért.");
        }
    }

    void CancelPlacement()
    {
        Destroy(currentRobot);
        currentRobot = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            if (other.TryGetComponent<Enemy>(out var enemyScript))
            {
                enemyScript.Die();
            }
        }
    }
}
