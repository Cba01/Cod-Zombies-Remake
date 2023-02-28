using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGun : Interactable
{
    PlayerStats playerStats;
    UIAnim uiAnim;


    private WeaponHolderSlot weaponHolderSlot;
    private PlayerInventory playerInventory;
    public GameObject gun;
    public int gunPrice;
    // Start is called before the first frame update
    void Start()
    {
        uiAnim = FindObjectOfType<UIAnim>();
        playerStats = FindObjectOfType<PlayerStats>();
        weaponHolderSlot = FindObjectOfType<WeaponHolderSlot>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }


    protected override void Interact()
    {
        if (playerStats.CanBePurchased(gunPrice))
        {
            playerStats.DiscountBalance(gunPrice);
            uiAnim.DiscountScore(gunPrice.ToString());
            weaponHolderSlot.LoadWeaponModel(gun);

        }

    }
}
