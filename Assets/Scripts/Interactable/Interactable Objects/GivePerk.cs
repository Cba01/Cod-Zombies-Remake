using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GivePerk : Interactable
{
    PlayerStats playerStats;
    UIAnim uiAnim;

    public Transform perksPivot;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        uiAnim = FindObjectOfType<UIAnim>();

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
            GameObject joggernogUI = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(joggernogUI.transform);
            BuyAnimation();
        }
    }

    public void SpeedCola(GameObject logo)
    {
        if (playerStats.speedCola == false)
        {
            playerStats.speedCola = true;
            GameObject speedCola = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(speedCola.transform);
            BuyAnimation();
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
            GameObject staminUp = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(staminUp.transform);
            BuyAnimation();
        }

    }

    private void BuyAnimation()
    {
        this.transform.DOShakeRotation(0.5f, 3f, 10, 90f);
    }



}
