using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    public Slider healthSlider;
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

        CancelSettings();
    }

    void CancelSettings()
    {
        settingsObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
