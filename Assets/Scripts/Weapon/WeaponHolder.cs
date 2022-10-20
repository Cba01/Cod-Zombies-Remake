using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private InputHandler inputHandler;


    public Transform parentOverride;

    [SerializeField]
    private Transform[] weapons;
    [SerializeField]
    private float switchTime;
    [SerializeField]
    private float timeSinceLastSwitch;

    void Awake()
    {
        inputHandler = GetComponentInParent<InputHandler>();

        SetWeapons();
        Select(inputHandler.inventoryIndex);
    }

    void Update()
    {
        Debug.Log("weapon lenght " + weapons.Length);
        Debug.Log("Inventory index " + inputHandler.inventoryIndex);

        if (inputHandler.changingWeapon_Input)
        {
            Select(inputHandler.inventoryIndex);
        }
        timeSinceLastSwitch += Time.deltaTime;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }
    }
    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        timeSinceLastSwitch = 0f;
    }

    private void OnWeaponSelected()
    {

    }
    public bool InventoryIsFull()
    {
        if (transform.childCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReplaceWeapon(GameObject gunPrefab)
    {
        if (InventoryIsFull())
        {
            /*             Destroy(weapons[inputHandler.inventoryIndex].gameObject);
             */
            Destroy(transform.GetChild(inputHandler.inventoryIndex).gameObject);
            /*             weapons[inputHandler.inventoryIndex] = gunPrefab.transform;
             */
            GameObject gun = Instantiate(gunPrefab) as GameObject;
            if (parentOverride != null)
            {
                gun.transform.parent = parentOverride;
            }
            else
            {
                gun.transform.parent = transform;
            }
            //Posicion y rotacion del model en relacion a la posicion y rotacion del padre
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
            //model.transform.localScale = Vector3.one;
        }
        else if (!InventoryIsFull())
        {
            GameObject gun = Instantiate(gunPrefab) as GameObject;
            if (parentOverride != null)
            {
                gun.transform.parent = parentOverride;
            }
            else
            {
                gun.transform.parent = transform;
            }
            //Posicion y rotacion del model en relacion a la posicion y rotacion del padre
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
            //model.transform.localScale = Vector3.one;
        }

        SetWeapons();
        Select(inputHandler.inventoryIndex);

    }

}
