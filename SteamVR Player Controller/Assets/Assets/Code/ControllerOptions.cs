using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControllerOptions : MonoBehaviour
{
    [Tooltip("Disabling this automatically enables keyboard-movement!")]
    public bool useVRControllerForMovement = false;
    public bool showVRController = false;

    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (showVRController)
            {
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }
    }
}
