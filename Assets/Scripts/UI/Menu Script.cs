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
    public GameObject background;
    public GameObject towerSpots;

    public void GameOverScript()
    {
        Start();
    }

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
        mainGameObject.SetActive(false);
        settingsObject.SetActive(false);
        background.SetActive(false);
        menuObject.SetActive(true);
        towerSpots.SetActive(false);
    }

    void StartGame()
    {
        menuObject.SetActive(false);
        mainGameObject.SetActive(true);
        background.SetActive(true);
        towerSpots.SetActive(true);
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
