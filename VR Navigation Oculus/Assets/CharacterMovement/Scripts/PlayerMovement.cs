using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movementtype { NONE, CONTROLLER, AIMEDTP }

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public OVRManager ovrManager;

    public MovementType type = MovementType.CONTROLLER;

    public MouseLook LookXAxis, LookYAxis;

    public float speed = 12f;
    public float adjSpeed = 5f;
    public float gravity = -9.81f;

    public Transform colliderAdjustment, groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool enableMouseMovement = true;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        Cursor.visible = false;
        LookXAxis.enabled = enableMouseMovement;
        LookYAxis.enabled = enableMouseMovement;

        ovrManager.RecenterPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.Start))
            ovrManager.RecenterPlayer();

        switch (type)
        {
            case MovementType.NONE:
                break;
            case MovementType.CONTROLLER:
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");

                x += Input.GetAxisRaw("Oculus_GearVR_LThumbstickX");
                z += Input.GetAxisRaw("Oculus_GearVR_LThumbstickY");
                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);

                Vector3 colliderAdjust = new Vector3(colliderAdjustment.position.x - Input.GetAxisRaw("Oculus_GearVR_RThumbstickX") * adjSpeed * Time.deltaTime, colliderAdjustment.position.y, colliderAdjustment.position.z - Input.GetAxisRaw("Oculus_GearVR_RThumbstickY") * adjSpeed * Time.deltaTime);
                colliderAdjustment.position = colliderAdjust;

                break;
            case MovementType.RANDOMTP:
                break;
            default:
                break;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
