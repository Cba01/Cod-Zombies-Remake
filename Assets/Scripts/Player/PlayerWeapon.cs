using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponSystem weaponSystem;

    void Start()
    {
        weaponSystem = GetComponentInChildren<WeaponSystem>();
    }

    public void HandleWeapon()
    {
        if (weaponSystem != null)
        {
            weaponSystem.ShootInput();
        }
        else
        {
            return;
        }
    }
}
