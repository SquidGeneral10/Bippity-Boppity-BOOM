#region Using Info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class ThirdPersonCharacter : MonoBehaviour
{
    private CharacterController controller;

    private GUIStyle style;

    [SerializeField] private Transform cam;

    private float speed = 5f; // The speed at which the player moves.
    private float rotationSmooth = 0.1f; // Makes turning a bit more smooth.
    private float turnSmoothVelocity;
    private int knowledge; // knowledge will determine the recipes you can understand and is increased by finding collectibles.

    #region Jumping variables
    private float gravity = 15f; // The speed at which you fall back to the ground.
    private float jumpForce = 5f;
    private Vector3 vertVelocity; // Controls the player's ability to jump up and down.
    #endregion

    void Start() // Start is called before the first frame update
    {
        controller = GetComponent<CharacterController>();

        InitialiseGUIStyle();
    }

    void Update() // Update is called once per frame
    {
        Jump();
        Movement();
    }

    void InitialiseGUIStyle()
    {
        style = new GUIStyle();
        style.normal.textColor = Color.black;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump")) // Making it jump when a jump input is detected means the game can work with controllers.
                vertVelocity.y = jumpForce;
        }
        else
        {
            vertVelocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(vertVelocity * Time.deltaTime); // Makes the character jump.
    }
    
    void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hor, 0f, ver);

        if(direction.sqrMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 finalDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(finalDirection * speed * Time.deltaTime); // Moves the player in the direction that they're facing.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            SingletonManager.Instance.Knowledge += 1;
            print(knowledge);
            Destroy(other.gameObject);
        }
    }

    bool IsGrounded() // Will be responsible for making sure the player is on the ground before they can jump.
    {
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        return Physics.Raycast(ray, 0.5f);
    }

    private void OnGUI() // This will be deleted for a proper UI eventually.
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Current Level: " + SceneManager.GetActiveScene().name);
        GUI.Label(new Rect(10, 30, 200, 20), "Knowledge: " + SingletonManager.Instance.Knowledge, style);
    }
}
