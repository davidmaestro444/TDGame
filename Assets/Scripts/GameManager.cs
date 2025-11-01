using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateUI();
        if (currentHealth <= 0) GameOver();
    }

    void UpdateUI()
    {
        if (healthText != null) healthText.text = "Életerõ: " + currentHealth;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");
        Time.timeScale = 0f;
    }
}
