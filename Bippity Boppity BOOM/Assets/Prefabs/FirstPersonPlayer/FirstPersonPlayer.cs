using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPlayer : MonoBehaviour
{
    private CharacterController controller;    
    private Animation anim;

    [SerializeField] private ParticleSystem smoke, flash;
    [SerializeField] private Transform playerCam;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Light flashLight;

    private float speed = 5f;
    private float rotSpeed = 180f;
    private float gravity = 9.81f;
    private float jumpForce = 2.5f;
    private float mouseSens = 3f;
    private float fireRate = 0.7f;
    private float vertVel, mouseX, mouseY, shootCoolDown;
    private bool canShoot = true;
    private bool isCrouching;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animation>();
        //audio = GetComponentInChildren<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movement = Vector3.zero;
        Vector3 rotation = Vector3.zero;

        //Calculate X Movement
        movement.x = Input.GetAxis("Horizontal");

        //Calculate Y Movement
        if(IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                vertVel = jumpForce;
        }
        else
        {
            vertVel -= gravity * Time.deltaTime;
        }

        movement.y = vertVel;

        //Calculate Z Movement
        movement.z = Input.GetAxis("Vertical");

        //Calculate Rotation
        float pitch = -Input.GetAxis("Mouse Y") * mouseSens;        
        rotation.y = Input.GetAxis("Mouse X") * mouseSens;
        pitch = Mathf.Clamp(pitch, -60f, 60f);

        playerCam.Rotate(new Vector3(pitch, 0f, 0f));

        movement = transform.TransformDirection(movement);

        //Apply transforms
        transform.Rotate(rotation * rotSpeed * Time.deltaTime);
        controller.Move(movement * speed * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            if (canShoot)
                Shoot();            
        }

        if(!canShoot)
        {
            shootCoolDown += Time.deltaTime;

            if (shootCoolDown >= fireRate)
            {
                shootCoolDown = 0f;
                canShoot = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouching)
                StartCoroutine(Crouch(2, 1));
            else
                StartCoroutine(Crouch(1, 2));
        }
	}

    void Shoot()
    {
        sound.Play();
        anim.Play();
        smoke.Play();
        flash.Play();
        StartCoroutine(LightIntensity());

        canShoot = false;
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        return (Physics.Raycast(ray, 0.3f));
    }

    IEnumerator Crouch(float input, float output)
    {
        if (isCrouching)
            isCrouching = false;
        else
            isCrouching = true;

        yield return controller.height = Mathf.Lerp(input, output, 1f * Time.deltaTime);
    }

    IEnumerator LightIntensity()
    {
        flashLight.intensity = 10f;
        yield return new WaitForSeconds(0.2f);
        flashLight.intensity = 0f;
    }    
}
