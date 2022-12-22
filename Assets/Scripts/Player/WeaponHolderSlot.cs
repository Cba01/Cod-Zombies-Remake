using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    private PlayerInventory playerInventory;


    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    public GameObject currentWeaponModel;

    public void UnLoadWeapon()
    {
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);
        }
    }
    public void UnLoadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void DestroyWeaponIndex()
    {
        if (playerInventory.parentOverride[playerInventory.currentWeaponIndex].childCount > 0)
        {
            Destroy(playerInventory.parentOverride[playerInventory.currentWeaponIndex].GetChild(0).gameObject);
        }
    }

    public void LoadWeaponModel(GameObject weaponItem)
    {
       
        DestroyWeaponIndex();

        if (weaponItem == null)
        {
            UnLoadWeapon();
            return;
        }

        playerInventory.weaponSlots[playerInventory.currentWeaponIndex] = weaponItem;

        GameObject model = Instantiate(weaponItem) as GameObject;
        if (model != null)
        {
            if (playerInventory.parentOverride[playerInventory.currentWeaponIndex] != null)
            {
                model.transform.parent = playerInventory.parentOverride[playerInventory.currentWeaponIndex];
            }
            else
            {
                model.transform.parent = transform;
            }
            //Posicion y rotacion del model en relacion a la posicion y rotacion del padre
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponModel = model;
    }
}
