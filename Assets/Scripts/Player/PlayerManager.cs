using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private InputHandler inputHandler;
    private PlayerWeapon playerWeapon;
    private PlayerInventory playerInventory;
    private PlayerUI playerUI;

    void Start()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputHandler = GetComponent<InputHandler>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerInventory = GetComponent<PlayerInventory>();
        playerUI = GetComponent<PlayerUI>();


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        inputHandler.TickInput();
        
        //Movement && Interact
        playerLocomotion.HandleMovement(inputHandler.movementInput);
        playerLocomotion.HandleLook(inputHandler.cameraInput);
        playerLocomotion.HandleJump();
        playerLocomotion.HandleCrouch();
        playerLocomotion.HandleSprint();
        playerLocomotion.HandleInteract();
        playerLocomotion.HandleMovementAnimation();
/*         playerLocomotion.HandleAimAnimation();
 */        playerLocomotion.HandleInterface();

        //Weapon
        

        //UI
        playerUI.UpdateBalance();

    }
    private void LateUpdate()
    {
        inputHandler.jump_Input = false;
        inputHandler.interact_Input = false;
        inputHandler.shootOnce_Input = false;
        inputHandler.reload_Input = false;
        inputHandler.changingWeapon_Input = false;
        inputHandler.debug_Input = false;
    }
}
