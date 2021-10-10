#region Using Info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ThirdPersonCharacter : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private Transform cam;

    private float speed = 5f;
    private float rotationSmooth = 0.1f;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
