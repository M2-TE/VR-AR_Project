using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ControllerOptions))]
public class PlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 MovementInput, TurningInput;
    public float MovementSpeed = 1, TurningSpeed = 1;

    [Tooltip("It is recommended to use either the HMDDirection or the JoystickTurning, Using both can be nauseating!")]
    public bool UseHMDDirection = true, EnableJoystickTurning = false;

    [Tooltip("Enabling this Camera automatically enables mouse movement.")]
    public bool EnableDebugCamera = false;

    private ControllerOptions controllerOptions;

    private void Awake()
    {
        controllerOptions = GetComponent<ControllerOptions>();
    }

    void Update()
    {

        float moveX = controllerOptions.useVRControllerForMovement ? MovementInput.axis.x : Input.GetAxisRaw("Horizontal");
        float moveY = controllerOptions.useVRControllerForMovement ? MovementInput.axis.y : Input.GetAxisRaw("Vertical");

        Vector3 direction = UseHMDDirection ? Player.instance.hmdTransform.TransformDirection(new Vector3(moveX,0,moveY).normalized) : new Vector3(moveX, 0, moveY).normalized; //might need a little rework, depending on rotation :thinking:

        


        transform.position += MovementSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
    }

}
