using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float health;
    public float stamina;
    public int balance;
    private float lerpTimer;
    [Header("Player Health")]
    public float maxHealth = 100f;
    public float chipSpeed = 10f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Player Stamina")]
    public float maxStamina = 100f;
    public float staminaDrainPerFrame = 10.0f;
    public Image staminaBar;
    public float staminaRegenTimer;
    public float staminaRegenAmount;


    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

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

    
}
