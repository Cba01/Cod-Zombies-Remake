using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("Stats")]
    public float health;
    public float stamina;
    public int balance;
    public float speed;
    private float lerpTimer;
    [Header("Player Health")]
    public float maxHealth = 100f;
    public float chipSpeed = 10f;
    public Image frontHealthBar;
    public Image backHealthBar;
    private float healthRegenTimer;
    public float healthRegenAmount;

    [Header("Player Stamina")]
    public float maxStamina = 100f;
    public float staminaDrainPerFrame = 10.0f;
    public Image staminaBar;
    private float staminaRegenTimer;
    public float staminaRegenAmount;
    public float walkSpeed;
    public float sprintSpeed;


    [Header("Perks")]
    public bool joggernog;
    public bool speedCola;
    public bool staminUp;


    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        RegenHealth();
        UpdateHealthUI();
        UpdateStaminaUI();

    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        healthRegenTimer = 0f;
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void UpdateStaminaUI()
    {

        float fillStamina = staminaBar.fillAmount;
        float staminaFraction = stamina / maxStamina;
        staminaBar.fillAmount = staminaFraction;
        lerpTimer += Time.deltaTime;
        staminaBar.fillAmount = Mathf.Lerp(fillStamina, staminaFraction, 1);
    }
    public void DrainStamina()
    {
        staminaRegenTimer = 0f;
        if (stamina <= maxStamina)
        {
            stamina -= staminaDrainPerFrame * Time.deltaTime;
        }
    }
    public void RegenStamina()
    {
        staminaRegenTimer += Time.deltaTime;
        if (stamina < maxStamina && staminaRegenTimer > 2f)
        {
            stamina += staminaRegenAmount * Time.deltaTime;
        }
    }

    public void RegenHealth()
    {
        healthRegenTimer += Time.deltaTime;
        if (health < maxHealth && healthRegenTimer > 5f)
        {
            health += healthRegenAmount * Time.deltaTime;
        }
    }

    public bool CanBePurchased(int price)
    {
        if (price <= balance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DiscountBalance(int price)
    {
        balance -= price;

    }


}
