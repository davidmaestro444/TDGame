using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    [SerializeField] Slider healthSlider;
    [SerializeField] Slider moneySlider;
    [SerializeField] ButtonScript buttonScript;
    [SerializeField] GameManager gameManagerScript;
    public Button saveButton;
    public Button cancelButton;
    public GameObject settingsObject;
    public GameObject menuObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveButton.onClick.AddListener(SaveSettings);
        cancelButton.onClick.AddListener(CancelSettings);
    }

    void SaveSettings()
    {
        //TODO: beállítások átvitele a fõjátékba

        if (buttonScript != null)
            buttonScript.SetRobotCost((int)moneySlider.value);
        else
            Debug.LogError("Nem található a button script.");

        if (gameManagerScript != null)
            gameManagerScript.SetHealth((int)healthSlider.value);
        else
            Debug.LogError("Nem található a game manager script.");

        CancelSettings();
    }

    void CancelSettings()
    {
        settingsObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
