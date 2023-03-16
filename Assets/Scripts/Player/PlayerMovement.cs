﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Reference to Controller")]
    public CharacterController controller; //get the Character Controller of the player. This is the motor that drives the player

    [Header("Control the speed of the Player")]
    public float speed = 12f; //use this to control the speed of the player
    [Header("Control the speed of the player falling (gravity)")]
    public float gravity = -9.81f; //use this to control how fast the player will fall
    [Header("Control how heigh the player can jump")]
    public float jumpHeight = 3f; //use this to control how high the player can jump

    [Header("GameObject at bottom of player")]
    public Transform groundCheck; //Transform from GameObject that is at the bottom of the player
    [Header("Radius of groundchecking sphere")]
    public float groundDistance = 0.4f; //this will be the radius of the sphere that will be used to check for the ground
    [Header("Control where velocity will reset")]
    public LayerMask groundMask; //this will be used to controll what objects the sphere should check for
    //groundMask will use the layer: "Ground". 

    Vector3 velocity; //use this to make the player fall with gravity
    bool isGrounded; //true means that the player is on the ground. False means that the player is in the air

    [Header("Running")]
    public KeyCode runningButton;
    public float runningSpeed = 15f;
    float originalSpeed;

    [Header("Crouching")]
    public GameObject playerCameraObj;
    public KeyCode crouchButton;
    public float cameraDown = 1f;
    public float crouchingSpeed = 5f;


    [Header("Zooming")]
    public Camera playerCamera;
    public GameObject binocularVision;
    public KeyCode zoomButton;
    bool canZoom = true;
    public float timeToZoom = 0.3f;
    public float zoomPOV = 30f;
    private float defaultPOV;
    private Coroutine zoomRoutine;

    private void Awake()
    {
        defaultPOV = playerCamera.fieldOfView;
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //create a shpere at the bottom of the player that has a certain radius. 
        //if that sphere collides with anything that is in the groundMask, then isGrounded will be true

        if (isGrounded && velocity.y < 0) //if the player touches the ground and is in the air
        {
            velocity.y = -2f; //set velocity to this. Not to 0. Because this can happen before the player is on the ground (because of the sphere).
            //So it is -2f to force the player on the ground a bit better
        }

        float x = Input.GetAxis("Horizontal"); //get the A and D input (so walking horizontaly)
        float z = Input.GetAxis("Vertical"); //get the W and S input (so walking vertical, or forwards and backwards)
        //this ↑ also works for controller

        Vector3 move = transform.right * x + transform.forward * z; //use transform. so that the player will move on the x, or z axis, that depends on the way the player is facing

        controller.Move(move * speed * Time.deltaTime); //move the player by a certain speed, and make it frame rate independent

        if (Input.GetButtonDown("Jump") && isGrounded) //if Space Bar is pressed and the player is on the ground then ↓
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //based on physics equation (v = √(h x -2 x g) )
        }

        velocity.y += gravity * Time.deltaTime; //add the gravity on the velocity on the y axis

        controller.Move(velocity * Time.deltaTime); //add the velocity to the player. Multiply it with Time.deltaTime again to complete the formule (because time is t squared (t2))

        //running
        if (Input.GetKeyDown(runningButton))
        {
            speed = runningSpeed;
        }

        if (Input.GetKeyUp(runningButton))
        {
            speed = originalSpeed;
        }

        //crouching
        if (Input.GetKeyDown(crouchButton))
        {
            speed = crouchingSpeed;
            playerCameraObj.transform.position = playerCameraObj.transform.position + new Vector3(0, -cameraDown, 0);
        }

        if (Input.GetKeyUp(crouchButton))
        {
            speed = originalSpeed;
            playerCameraObj.transform.position = playerCameraObj.transform.position + new Vector3(0, cameraDown, 0);
        }

        //zoom
        if (canZoom)
        {
            HandleZoom();
        }

        if (defaultPOV != playerCamera.fieldOfView)
        {
            binocularVision.SetActive(true);
        }
        else if (defaultPOV == playerCamera.fieldOfView)
        {
            binocularVision.SetActive(false);
        }
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomButton))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }

        if (Input.GetKeyUp(zoomButton))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetPOV = isEnter ? zoomPOV : defaultPOV;
        float startingPOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingPOV, targetPOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetPOV;
        zoomRoutine = null;
    }
}
