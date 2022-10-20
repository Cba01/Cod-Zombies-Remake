using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InputHandler inputHandler;
    private GameObject currentGun;

    public GameObject[] inventorySlots = new GameObject[1];
    public List<GameObject> inventorySlotss;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }
    private void Update()
    {
        if (inputHandler.changingWeapon_Input)
        {
            
        }
    }

    public void ChangeWeapon()
    {
        currentGun = inventorySlots[inputHandler.inventoryIndex];
    }
    public void LoadGun(int index, GameObject gun)
    {
        inventorySlots[index] = gun;
    }
}
