using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private InputHandler inputHandler;
    private PlayerUI playerUI;
    private PlayerStats playerStats;


    [Header("Player Config")]
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer;
    public bool lerpCrouch;

    [Header("Camera Prefs")]
    public GameObject cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    [Header("Player Interact")]
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;


    [Header("Player Animation")]
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
        playerUI = GetComponent<PlayerUI>();
        playerStats = GetComponent<PlayerStats>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Debug.Log(inputHandler.moveAmount);

    }

    public void HandleMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (inputHandler.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void HandleLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void HandleJump()
    {
        if (inputHandler.isGrounded)
        {
            if (inputHandler.jump_Input)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
    }

    public void HandleCrouch()
    {
        inputHandler.isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (inputHandler.crouch_Input)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void HandleSprint()
    {
        if (inputHandler.sprint_Input && playerStats.stamina > 0.1f)
        {
            speed = 8f;
            playerStats.staminaRegenTimer = 0f;
            playerStats.DrainStamina();
        }
        else
        {
            speed = 5f;
            playerStats.RegenStamina();
        }
    }

    public void HandleInteract()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputHandler.interact_Input)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }

    public void HandleMovementAnimation()
    {
        if (inputHandler.moveAmount == 0)
        {
            anim.SetFloat("Speed", 0f);
        }
        else if (inputHandler.moveAmount > 0 && !inputHandler.sprint_Input)
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);

        }
        else if (inputHandler.moveAmount > 0 && inputHandler.sprint_Input)
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);

        }
    }

    public void HandleShootAnimation()
    {
/*         anim.SetTrigger("Shoot");
 */        anim.Play("Pistol_Shoot");
    }

}
