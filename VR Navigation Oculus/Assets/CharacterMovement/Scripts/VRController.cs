using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{
    public OVRManager ovrManager;

    void Start()
    {
        ovrManager.RecenterPlayer();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.Start))
            ovrManager.RecenterPlayer();
    }
}
