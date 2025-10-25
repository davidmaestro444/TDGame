using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public GameObject robot;
    public Button button;
    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(SpawnRobot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRobot()
    {
        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        GameObject newRobot = Instantiate(robot, pos, Quaternion.identity);

        StartCoroutine(MoveRobotAfterBuying(newRobot));

        
        Debug.Log("Button pressed");
    }

    IEnumerator MoveRobotAfterBuying(GameObject robot)
    {
        while (!Mouse.current.leftButton.isPressed)
        {
            Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            robot.transform.position = pos;

            yield return null;
        }
    }
}
