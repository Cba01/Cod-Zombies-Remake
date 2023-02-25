using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGun : Interactable
{

    [SerializeField]
    private WeaponHolderSlot weaponHolderSlot;
    private PlayerInventory playerInventory;
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        weaponHolderSlot = FindObjectOfType<WeaponHolderSlot>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    
    protected override void Interact()
    {
         weaponHolderSlot.LoadWeaponModel(gun);

    }
}
