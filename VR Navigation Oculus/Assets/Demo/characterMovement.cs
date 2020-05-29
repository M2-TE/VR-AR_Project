using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType { NONE, CONTROLLER, RANDOMTP}

public class characterMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float tpCooldown = 1f;
    public MovementType mType = MovementType.NONE;
    
    private float timeSinceLastTp = 0f;

    void Update()
    {

        switch (mType)
        {
            case MovementType.NONE:
                break;
            case MovementType.CONTROLLER:
                Vector3 movement = new Vector3(transform.position.x - Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") * speed, transform.position.y, transform.position.z - Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") * speed);
                gameObject.transform.position = movement;

                break;
            case MovementType.RANDOMTP:

                if(timeSinceLastTp <= 0f && Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") > 0.2f)
                {
                    timeSinceLastTp = tpCooldown;
                    gameObject.transform.position = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
                }
                break;
            default:
                break;
        }

        if (Input.GetAxisRaw("Oculus_GearVR_RThumbstickY") >= 0.5f)
            UnityEditor.EditorApplication.isPlaying = false;

        timeSinceLastTp -= timeSinceLastTp > 0f ? Time.deltaTime : 0f;
    }
}
