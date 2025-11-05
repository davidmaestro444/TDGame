using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 100;
    public int startingMoney = 20;
    private int currentHealth;
    private int currentMoney;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;

    public void SetHealth(int health)
    {
        currentHealth = health;
    }

    public int CurrentMoney 
    { 
        get 
        { 
            return currentMoney; 
        } 
    }

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
        currentMoney = startingMoney;
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateUI();
        if (currentHealth <= 0) GameOver();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateUI();
    }

    public void SpendMoney(int amount)
    {
        currentMoney -= amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "Életerõ: " + currentHealth;
        }

        if (moneyText != null)
        {
            moneyText.text = "Pénz: " + currentMoney;
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");
        Time.timeScale = 0f;
    }
}
