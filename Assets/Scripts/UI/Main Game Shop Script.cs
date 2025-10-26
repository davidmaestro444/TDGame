using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject robot;
    public Camera mainCamera;

    private GameObject currentRobot;

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
            PlaceRobot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    public void SelectRobotToSpawn()
    {
        if (currentRobot != null)
        {
            return;
        }

        currentRobot = Instantiate(robot, Vector3.zero, Quaternion.identity);
    }

    private void PlaceRobot()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
        {
            currentRobot.transform.position = hit.collider.transform.position;
            Debug.Log("Robot lerakva ide: " + hit.collider.name);

            currentRobot = null;
        }
        else
        {
            Debug.Log("Ide nem lehet lerakni!");
        }
    }

    private void CancelPlacement()
    {
        Debug.Log("Lerakás megszakítva.");
        Destroy(currentRobot);
        currentRobot = null;
    }


}