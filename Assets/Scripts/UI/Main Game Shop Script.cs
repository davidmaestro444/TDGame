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
    }

    private void PlaceWeaponRobot()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
        {
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

    void PlaceMeeleRobot()
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

    private void CancelPlacement()
    {
        Debug.Log("Lerakás megszakítva.");
        Destroy(currentRobot);
        currentRobot = null;
    }


}