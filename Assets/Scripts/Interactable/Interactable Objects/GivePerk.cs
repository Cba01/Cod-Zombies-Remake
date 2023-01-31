using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePerk : Interactable
{
    PlayerStats playerStats;
    
    public Transform perksPivot;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        
    }
    private void Start()
    {

    }
    protected override void Interact()
    {

    }

    public void Joggernog(GameObject logo)
    {
        if (playerStats.joggernog == false)
        {
            playerStats.maxHealth *= 2;
            playerStats.joggernog = true;
            Instantiate(logo, perksPivot);
        }
    }

    public void SpeedCola(GameObject logo)
    {
        if (playerStats.speedCola == false)
        {
            playerStats.speedCola = true;
            Instantiate(logo, perksPivot);

        }
    }
    public void StaminUp(GameObject logo)
    {
        if (playerStats.staminUp == false)
        {
            playerStats.maxStamina *= 1.5f;
            playerStats.walkSpeed *= 1.5f;
            playerStats.sprintSpeed *= 1.5f;
            playerStats.staminaRegenAmount *= 2;
            playerStats.staminUp = true;
            Instantiate(logo, perksPivot);

        }

    }



}
