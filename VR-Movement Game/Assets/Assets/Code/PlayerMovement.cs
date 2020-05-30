using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ControllerOptions))]
public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 MovementInput, TurningInput;
    public float MovementSpeed = 5f, TurningSpeed = 15f;

    [Tooltip("It is recommended to use either the HMDDirection or the JoystickTurning, Using both can be nauseating!")]
    public bool UseHMDDirection = true, EnableJoystickTurning = false;

    private ControllerOptions controllerOptions;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Awake()
    {
        controllerOptions = GetComponent<ControllerOptions>();
    }

    void Update()
    {

        float moveX = controllerOptions.useVRControllerForMovement ? MovementInput.axis.x : Input.GetAxisRaw("Horizontal");
        float moveY = controllerOptions.useVRControllerForMovement ? MovementInput.axis.y : Input.GetAxisRaw("Vertical");

        Vector3 direction = UseHMDDirection ? Player.instance.hmdTransform.TransformDirection(new Vector3(moveX,0,moveY).normalized) : new Vector3(moveX, 0, moveY).normalized; //might need a little rework, depending on rotation :thinking:

        if (EnableJoystickTurning && controllerOptions.useVRControllerForMovement)
        {
            rotationY += TurningInput.axis.x * TurningSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        }


        transform.position += MovementSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
    }

}
