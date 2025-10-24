using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Button startButton;
    public Button settingsButton;
    public Button exitButton;
    public GameObject menuObject;
    public GameObject mainGameObject;
    public GameObject settingsObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        menuObject.SetActive(false);
        mainGameObject.SetActive(true);
        //TODO: játék elindítása innen
    }
    
    void OpenSettings()
    {
        menuObject.SetActive(false);
        settingsObject.SetActive(true);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
