using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject robot;
    public Camera mainCamera;
    private GameObject weaponRobot;


    public int robotCost = 10;

    void Update()
    {
        if (weaponRobot == null)
        {
            return;
        }

        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        weaponRobot.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceWeaponRobot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    public void SelectRobotToSpawn()
    {
        if (GameManager.instance.CurrentMoney < robotCost)
        {
            Debug.Log("Nincs elég pénzed!");
            return;
        }

        if (weaponRobot != null)
        {
            return;
        }

        weaponRobot = Instantiate(robot, Vector3.zero, Quaternion.identity);

        Tower towerScript = weaponRobot.GetComponent<Tower>();
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
                weaponRobot.transform.position = hit.collider.transform.position;
                towerSpot.isOccupied = true;

                Tower towerScript = weaponRobot.GetComponent<Tower>();
                if (towerScript != null)
                {
                    towerScript.enabled = true;
                }
                GameManager.instance.SpendMoney(robotCost);
                Debug.Log("Robot lerakva ide: " + hit.collider.name);
                weaponRobot = null;
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
                    weaponRobot.transform.position = hit.collider.transform.position;
                    towerSpot.isOccupied = true;

                    Tower towerScript = weaponRobot.GetComponent<Tower>();
                    if (towerScript != null)
                    {
                        towerScript.enabled = true;
                    }
                    GameManager.instance.SpendMoney(robotCost);
                    Debug.Log("Robot lerakva ide: " + hit.collider.name);
                    weaponRobot = null;
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
                    weaponRobot.transform.position = hit.collider.transform.position;
                    towerSpot.isOccupied = true;

                    Tower towerScript = weaponRobot.GetComponent<Tower>();
                    if (towerScript != null)
                    {
                        towerScript.enabled = true;
                    }
                    GameManager.instance.SpendMoney(robotCost);
                    Debug.Log("Robot lerakva ide: " + hit.collider.name);
                    weaponRobot = null;
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
        Destroy(weaponRobot);
        weaponRobot = null;
    }


}