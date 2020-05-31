//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Controls for the non-VR debug camera
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Camera ) )]
	public class FallbackCameraController : MonoBehaviour
	{
		public float speed = 12.0f;
		public bool showInstructions = true;

        public Transform player;
        public float mouseSensitivity = 100f;
        public bool LockMouse = false;

        private float xRotation = 0f;
        private float yRotation = 0f;

        private bool lockMouse;

		//-------------------------------------------------
		void OnEnable()
		{
            ChangeMouseLockState();
        }

        private void ChangeMouseLockState()
        {
            lockMouse = LockMouse;
            if (lockMouse)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }


        //-------------------------------------------------
        void Update()
		{
            if (LockMouse != lockMouse)
                ChangeMouseLockState();

            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            float movementX = Input.GetAxisRaw("Horizontal");
            float movementZ = Input.GetAxisRaw("Vertical");

            Quaternion aim = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            Vector3 move = (aim * Vector3.right) * movementX + (aim * Vector3.forward) * movementZ;

            player.transform.position += move * speed * Time.deltaTime;
		}


		//-------------------------------------------------
		void OnGUI()
		{
			if ( showInstructions )
			{
				GUI.Label( new Rect( 10.0f, 10.0f, 600.0f, 400.0f ),
					"WASD/Arrow Keys to translate the camera\n" +
					"Move mouse to rotate the camera\n" +
					"Left mouse click for standard interactions.\n" );
			}
		}
	}
}
