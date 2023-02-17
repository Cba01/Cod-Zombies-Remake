using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GivePerk : Interactable
{
    PlayerStats playerStats;
    UIAnim uiAnim;

    public Transform perksPivot;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip perkSound;
    public AudioClip cantBuySound;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        uiAnim = FindObjectOfType<UIAnim>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Start()
    {

    }
    protected override void Interact()
    {

    }

    public void Joggernog(GameObject logo)
    {
        if (playerStats.joggernog == false && playerStats.CanBePurchased(2500))
        {
            playerStats.DiscountBalance(2500);
            playerStats.maxHealth *= 2;
            playerStats.joggernog = true;
            GameObject joggernogUI = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(joggernogUI.transform);
            uiAnim.DiscountScore("2500");
            BuyAnimation();
            audioSource.PlayOneShot(perkSound);
        }
        else
        {
            audioSource.PlayOneShot(cantBuySound);
        }
    }

    public void SpeedCola(GameObject logo)
    {
        if (playerStats.speedCola == false && playerStats.CanBePurchased(3000))
        {
            playerStats.DiscountBalance(3000);
            playerStats.speedCola = true;
            GameObject speedCola = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(speedCola.transform);
            uiAnim.DiscountScore("3000");
            BuyAnimation();
            audioSource.PlayOneShot(perkSound);
        }
        else
        {
            audioSource.PlayOneShot(cantBuySound);
        }
    }
    public void StaminUp(GameObject logo)
    {
        if (playerStats.staminUp == false && playerStats.CanBePurchased(2000))
        {
            playerStats.DiscountBalance(2000);
            playerStats.maxStamina *= 1.5f;
            playerStats.walkSpeed *= 1.5f;
            playerStats.sprintSpeed *= 1.5f;
            playerStats.staminaRegenAmount *= 2;
            playerStats.staminUp = true;
            GameObject staminUp = Instantiate(logo, perksPivot);
            uiAnim.PerkAnimation(staminUp.transform);
            uiAnim.DiscountScore("2000");
            BuyAnimation();
            audioSource.PlayOneShot(perkSound);
        }
        else
        {
            audioSource.PlayOneShot(cantBuySound);
        }

    }

    private void BuyAnimation()
    {
        this.transform.DOShakeRotation(0.5f, 3f, 10, 90f);
    }



}
