using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int health = 0;
    [SerializeField] private int income = 0;
    [SerializeField] private GameObject GameOverMenu = null;
    [SerializeField] private TextMeshProUGUI IncomeText = null;
    [SerializeField] private TextMeshProUGUI HealthText = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void takeDamage(int value)
    {
        health -= value;
        updatePlayerHealth(health);
        
        if (health <= 0)
        {
            //player dies
            GameOverMenu.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }
    
    public void updatePlayerHealth(int health)
    {
        if (health > 0)
        {
            HealthText.text = "Health: " + health;
        }
        else HealthText.text = "0";
    }

    public void updateIncome(int income)
    {
        IncomeText.text = "Income: " + income;
    }

    public void upgradeIncome()
    {
        income = income + 1;
    }
}
