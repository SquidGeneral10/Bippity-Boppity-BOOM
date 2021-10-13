#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player, centrePoint;
    private float mouseX, mouseY;
    [SerializeField] private float rAnalogSens = 0f; // Lets you change the joystick sensitivity.
    [SerializeField] private bool invertVertical = false; // Allows for inverted camera options, if the player is a twisted individual.

    void Start() // Start is called before the first frame update
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the middle of the screen and makes it invisible.
    }

    void Update() // Update is called once per frame
    {
        MouseUpdate();
    }

    private void LateUpdate() {
        transform.position = player.transform.position;
    }

    void MouseUpdate()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        if (Input.GetJoystickNames().Length > 0)
        {
            mouseX += Input.GetAxis("RightVertical") * rAnalogSens;

            if (!invertVertical)
            {
                mouseY += Input.GetAxis("RightHorizontal") * rAnalogSens;
            }
            else
            {
                mouseY -= Input.GetAxis("RightHorizontal") * rAnalogSens;
            }
        }
        else
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseX -= Input.GetAxis("Mouse Y");
        }

        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        centrePoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
