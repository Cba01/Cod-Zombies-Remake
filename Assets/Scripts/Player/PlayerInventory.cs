using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InputHandler inputHandler;

    public int currentWeaponIndex = 0;

    public GameObject[] weaponSlots;

    public Transform[] parentOverride;

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
            // Aumenta el indice del arma equipada
            currentWeaponIndex++;

            // Cambia resetea el indice si se pasa del tamaño del inventario
            if (currentWeaponIndex >= weaponSlots.Length)
            {
                currentWeaponIndex = 0;
            }

            /* if (parentOverride[currentWeaponIndex].childCount > 0)

                return;
 */
            // Desactiva todos las armas
            for (int i = 0; i < parentOverride.Length; i++)
            {
                parentOverride[i].GetChild(0).gameObject.SetActive(false);
            }

            // Activa el arma dependiendo del indice actual
            parentOverride[currentWeaponIndex].GetChild(0).gameObject.SetActive(true);

        }
        else if (inputHandler.changingWeapon_Value < -0.1f)
        {
            currentWeaponIndex--;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weaponSlots.Length - 1;
            }

            /* if (parentOverride[currentWeaponIndex].childCount > 0)

                return; */

            for (int i = 0; i < parentOverride.Length; i++)
            {
                parentOverride[i].GetChild(0).gameObject.SetActive(false);
            }

            parentOverride[currentWeaponIndex].GetChild(0).gameObject.SetActive(true);
        }



    }



}
