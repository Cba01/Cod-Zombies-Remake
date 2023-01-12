using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePerk : Interactable
{
    PlayerStats playerStats;
    public WeaponSystem[] weaponSystem;
    public GunData[] gunData;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        weaponSystem = FindObjectsOfType<WeaponSystem>();
        gunData = FindObjectsOfType<GunData>();
        
    }
    private void Start()
    {

    }
    protected override void Interact()
    {

    }

    public void Joggernog()
    {
        if (playerStats.joggernog == false)
        {
            playerStats.maxHealth *= 2;
            playerStats.joggernog = true;
        }
    }

    public void SpeedCola()
    {
        if (playerStats.speedCola == false)
        {
            playerStats.speedCola = true;

        }
    }
    public void StaminUp()
    {
        if (playerStats.staminUp == false)
        {
            playerStats.maxStamina *= 1.5f;
            playerStats.walkSpeed *= 1.5f;
            playerStats.sprintSpeed *= 1.5f;
            playerStats.staminaRegenAmount *= 2;
            playerStats.staminUp = true;
        }

    }



}
