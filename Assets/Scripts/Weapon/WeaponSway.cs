using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("References")]
    InputHandler inputHandler;

    [Header("Sway")]
    public float step = 0.01f;
    public float maxStepDistance = 0.6f;
    public float smooth = 10f;
    Vector3 swayPos;

    [Header("Sway Rotation")]
    public float rotationStep = 0.01f;
    public float maxRotationStep = 0.6f;
    public float smoothRot = 12f;
    Vector3 swayEulerRot;

    [Header("Settings")]
    public bool sway = true;
    public bool swayRotation = true;



    void Start()
    {
        inputHandler = GetComponentInParent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Sway();
        SwayRotation();

        CompositePositionRotation();
    }

    Vector2 walkInput;
    Vector2 lookInput;

    void GetInput()
    {
        walkInput.x = inputHandler.horizontal;
        walkInput.y = inputHandler.vertical;
        walkInput = walkInput.normalized;

        lookInput.x = inputHandler.mouseX;
        lookInput.y = inputHandler.mouseY;
    }

    void Sway()
    {
        if (sway == false) { swayPos = Vector3.zero; return; }
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;
    }
    void SwayRotation()
    {
        if (swayRotation == false) { swayEulerRot = Vector3.zero; return; }
        Vector3 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
    void CompositePositionRotation()
    {
        transform.localPosition =
        Vector3.Lerp(transform.localPosition,
        swayPos,
        Time.deltaTime * smooth);

        transform.localRotation =
        Quaternion.Slerp(transform.localRotation,
        Quaternion.Euler(swayEulerRot),
        Time.deltaTime * smoothRot);
    }
}
