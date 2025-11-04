using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Ide húzzuk be a "Fill" képet
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Ezt a függvényt hívjuk meg kívülrõl, hogy frissítsük a csíkot
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Kiszámoljuk, hány százalékon áll az élet (0.0 és 1.0 között)
        fillImage.fillAmount = currentHealth / maxHealth;
    }
}
