using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject weaponRobot;
    public GameObject sniperRobot;
    public GameObject meeleRobot;
    public Camera mainCamera;
    private GameObject currentRobot;

    public int robotCost = 10;

    public void SetRobotCost(int cost)
    {
        robotCost = cost;
    }

    void Update()
    {
        if (currentRobot == null)
        {
            return;
        }

        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentRobot.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceWeaponRobot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    public void SelectWeaponRobotToSpawn()
    {
        if (GameManager.instance.CurrentMoney < robotCost)
        {
            Debug.Log("Nincs elég pénzed!");
            return;
        }

        if (currentRobot != null)
        {
            return;
        }

        currentRobot = Instantiate(weaponRobot, Vector3.zero, Quaternion.identity);

        Tower towerScript = currentRobot.GetComponent<Tower>();
        if (towerScript != null)
        {
            towerScript.enabled = false;
        }
    }

    public void SelectSniperRobotToSpawn()
    {
        if (GameManager.instance.CurrentMoney < robotCost)
        {
            Debug.Log("Nincs elég pénzed!");
            return;
        }

        if (currentRobot != null)
        {
            return;
        }

        currentRobot = Instantiate(sniperRobot, Vector3.zero, Quaternion.identity);

        Tower towerScript = currentRobot.GetComponent<Tower>();
        if (towerScript != null)
        {
            towerScript.enabled = false;
        }
    }

    public void SelectMeeleRobotToSpawn()
    {
        if (GameManager.instance.CurrentMoney < robotCost)
        {
            Debug.Log("Nincs elég pénzed!");
            return;
        }

        if (currentRobot != null)
        {
            return;
        }

        currentRobot = Instantiate(meeleRobot, Vector3.zero, Quaternion.identity);

        Tower towerScript = currentRobot.GetComponent<Tower>();
        if (towerScript != null)
        {
            towerScript.enabled = false;
        }

        //co-pilot írta az alábbit, hogy legyen egy kör a robot körül

        GameObject circle = new GameObject("CircleVisual");

        // SpriteRenderer hozzáadása
        SpriteRenderer sr = circle.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("circle"); // A "circle.png" legyen a Resources mappában
        sr.sortingOrder = -1; // Háttérbe helyezés, hogy ne takarja a robotot

        // Szülõ beállítása
        circle.transform.parent = currentRobot.transform;

        // Pozíció beállítása relatív a robothoz
        circle.transform.localPosition = Vector3.zero;

        // Méret módosítása (opcionális)
        circle.transform.localScale = new Vector3(2f, 2f, 1f); // Például nagyobb kör

        //innentõl én

        circle.SetActive(false);

    }

    private void PlaceWeaponRobot()
    {
        int placingTowerLayer = LayerMask.NameToLayer("Tower");
        int layerMask = ~(1 << placingTowerLayer);
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
        {
            currentRobot.layer = LayerMask.NameToLayer("Default");
            TowerSpot towerSpot = hit.collider.GetComponent<TowerSpot>();

            if (towerSpot != null && !towerSpot.isOccupied)
            {
                currentRobot.transform.position = hit.collider.transform.position;
                towerSpot.isOccupied = true;

                Tower towerScript = currentRobot.GetComponent<Tower>();
                if (towerScript != null)
                {
                    towerScript.enabled = true;
                }
                GameManager.instance.SpendMoney(robotCost);
                Debug.Log("Robot lerakva ide: " + hit.collider.name);
                currentRobot = null;
            }
            else
            {
                Debug.Log("Ez a hely már foglalt!");
            }
        }
        else
        {
            Debug.Log("Ide nem lehet lerakni!");
        }
    }







    void PlaceSniperRobot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mainCamera.ScreenToViewportPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
            {
                TowerSpot towerSpot = hit.collider.GetComponent<TowerSpot>();

                if (towerSpot != null && towerSpot.isOccupied)
                {
                    currentRobot.transform.position = hit.collider.transform.position;
                    towerSpot.isOccupied = true;

                    Tower towerScript = currentRobot.GetComponent<Tower>();
                    if (towerScript != null)
                    {
                        towerScript.enabled = true;
                    }
                    GameManager.instance.SpendMoney(robotCost);
                    Debug.Log("Robot lerakva ide: " + hit.collider.name);
                    currentRobot = null;
                }
                else
                {
                    Debug.Log("Ez a hely már foglalt.");
                }
            }
            else
            {
                Debug.Log("Ide nem lehet lerakni!");
            }
        }
    }

    void PlaceTrap()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mainCamera.ScreenToViewportPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
            {
                TowerSpot towerSpot = hit.collider.GetComponent<TowerSpot>();

                if (towerSpot != null && towerSpot.isOccupied)
                {
                    currentRobot.transform.position = hit.collider.transform.position;
                    towerSpot.isOccupied = true;

                    Tower towerScript = currentRobot.GetComponent<Tower>();
                    if (towerScript != null)
                    {
                        towerScript.enabled = true;
                    }
                    GameManager.instance.SpendMoney(robotCost);
                    Debug.Log("Csapda lerakva ide: " + hit.collider.name);
                    currentRobot = null;
                }
                else
                {
                    Debug.Log("Ez a hely már foglalt.");
                }
            }
            else
            {
                Debug.Log("Ide nem lehet lerakni!");
            }
        }
    }

    private void CancelPlacement()
    {
        Debug.Log("Lerakás megszakítva.");
        Destroy(currentRobot);
        currentRobot = null;
    }


}