using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InputHandler inputHandler;

    [SerializeField]
    private int currentWeaponIndex = 0;

    public GameObject[] weaponSlots;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }
    private void Update()
    {
        if (inputHandler.changingWeapon_Value != 0)
        {
            SwitchWeapon();
        }
    }

    public void SwitchWeapon()
    {

        if (inputHandler.changingWeapon_Value > 0.1f)
        {
            foreach (GameObject weapon in weaponSlots)
            {
                weapon.SetActive(false);
            }

            currentWeaponIndex++;

            if (currentWeaponIndex >= weaponSlots.Length)
            {
                currentWeaponIndex = 0;
            }

            weaponSlots[currentWeaponIndex].SetActive(true);
        }
        else if(inputHandler.changingWeapon_Value < -0.1f)
        {
            foreach (GameObject weapon in weaponSlots)
            {
                weapon.SetActive(false);
            }

            currentWeaponIndex--;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weaponSlots.Length - 1;
            }

            weaponSlots[currentWeaponIndex].SetActive(true);
        }



    }

}
