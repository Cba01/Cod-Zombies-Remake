using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private CharacterController controller;
    private PlayerLocomotion playerLocomotion;
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.WeaponActions weapon;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    [Header("Player inputs")]
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;


    [Header("Player Flags")]
    public bool jump_Input;
    public bool crouch_Input;
    public bool sprint_Input;
    public bool interact_Input;
    public bool aim_Input;
    public bool isGrounded;
    public bool shootOnce_Input;
    public bool shootHold_Input;
    public bool reload_Input;

    public bool changingWeapon_Input;
    public float changingWeapon_Value;
    public int inventoryIndex = 0;
    public float changeTime;
    public float switchTime = 1f;



    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        weapon = playerInput.Weapon;

    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInput = new PlayerInput();
    }
    private void Update()
    {
        changeTime += Time.deltaTime;
        Debug.Log("CHANGE WEAPON INPUT "+ weapon.ChangeWeapon.ReadValue<float>());

    }

    public void TickInput()
    {
        MoveInput();

        JumpInput();
        CrouchInput();
        SprintInput();
        InteractInput();
        GroundedInput();

        OneShotInput();
        HoldShootInput();
        ReloadInput();
        AimInput();
        ChangeWeaponInput();
    }


    private void JumpInput()
    {
        onFoot.Jump.performed += i => jump_Input = true;
    }
    private void CrouchInput()
    {
        onFoot.Crouch.performed += i => crouch_Input = !crouch_Input;
        onFoot.Crouch.performed += i => playerLocomotion.crouchTimer = 0;
        onFoot.Crouch.performed += i => playerLocomotion.lerpCrouch = true;
    }
    private void AimInput()
    {
        aim_Input = weapon.Aim.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    }
    private void SprintInput()
    {
        sprint_Input = onFoot.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    }
    private void InteractInput()
    {
        onFoot.Interact.performed += i => interact_Input = true;
    }
    private void GroundedInput()
    {
        isGrounded = controller.isGrounded;
    }
    private void OneShotInput()
    {
        weapon.Shoot.performed += i => shootOnce_Input = true;

    }
    private void HoldShootInput()
    {
        shootHold_Input = weapon.Shoot.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    }
    private void ReloadInput()
    {
        weapon.Reload.performed += i => reload_Input = true;
    }
    private void ChangeWeaponInput()
    {
            weapon.ChangeWeapon.performed += i => changingWeapon_Input = true;
            changingWeapon_Value = weapon.ChangeWeapon.ReadValue<float>();
            /* float scrollValue = weapon.ChangeWeapon.ReadValue<float>();
            if (scrollValue > 0)
            {
                if (inventoryIndex == 1)
                {
                    inventoryIndex = 0;
                }
                else
                {
                    inventoryIndex = 1;
                }
            }
            else if (scrollValue < 0)
            {
                if (inventoryIndex == 0)
                {
                    inventoryIndex = 1;
                }
                else
                {
                    inventoryIndex = 0;
                }
            } */
    }


    private void MoveInput()
    {
        movementInput = onFoot.Movement.ReadValue<Vector2>();
        cameraInput = onFoot.Look.ReadValue<Vector2>();
        float delta = Time.deltaTime;
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void OnEnable()
    {
        onFoot.Enable();
        weapon.Enable();
    }
    public void OnDisable()
    {
        onFoot.Disable();
        weapon.Disable();
    }
}
