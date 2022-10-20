using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
     public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

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

        public void LoadWeaponModel(GameObject weaponItem)
        {

            UnLoadWeaponAndDestroy();
            if (weaponItem == null)
            {
                UnLoadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                //Posicion y rotacion del model en relacion a la posicion y rotacion del padre
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                //model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
}
