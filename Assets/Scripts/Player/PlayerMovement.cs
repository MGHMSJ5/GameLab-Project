using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller; //get the Character Controller of the player. This is the motor that drives the player
    public Animator playerAnimator;
    public Transform shadowPlayer;
    private Vector3 originalPosition;

    [Header("Control the speed of the Player")]
    public float speed = 12f; //use this to control the speed of the player
    private float movementThreshold = 0.1f;
    [Header("Control the speed of the player falling (gravity)")]
    public float gravity = -9.81f; //use this to control how fast the player will fall
    [Header("Jumping")]
    public float jumpHeight = 3f; //use this to control how high the player can jump

    public Transform groundCheck; //Transform from GameObject that is at the bottom of the player
    public float groundDistance = 0.4f; //this will be the radius of the sphere that will be used to check for the ground
    public LayerMask groundMask; //this will be used to controll what objects the sphere should check for
    //groundMask will use the layer: "Ground". 

    Vector3 velocity; //use this to make the player fall with gravity
    bool isGrounded; //true means that the player is on the ground. False means that the player is in the air
    public bool canJump;

    [Header("Running")]
    public KeyCode runningButton; //this will be the button that will activate the running
    public float runningSpeed = 15f; //this will be the new running speed
    float originalSpeed; //this will be the original speed

    [Header("Crouching")]
    public GameObject playerCameraObj; //get the camera (since for crouching, the camera will be a bit lower)
    public KeyCode crouchButton; //this will be the button that will make the player crouch
    public float cameraDown = 1f; //how much the camera will go down
    public float crouchingSpeed = 5f; //the speed of the player when crouching
    public bool isCrouching = false;

    [Header("Headbob")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    public float defaultYPos = 0;
    private float timer;

    private void Awake()
    {
        originalSpeed = speed; //set the original speed
        originalPosition = shadowPlayer.localPosition;
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

        bool isMoving = controller.velocity.magnitude > movementThreshold;
        if (isMoving)
        {  
           playerAnimator.SetBool("Walking", true);
           if (speed == runningSpeed)
            {
                playerAnimator.SetBool("Running", true);
            }
        }
        else
        {
            //playerAnimator.SetBool("Idle", true);
            playerAnimator.SetBool("Walking", false);
            playerAnimator.SetBool("Running", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && canJump) //if Space Bar is pressed and the player is on the ground then ↓
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //based on physics equation (v = √(h x -2 x g) )
        }

        velocity.y += gravity * Time.deltaTime; //add the gravity on the velocity on the y axis

        controller.Move(velocity * Time.deltaTime); //add the velocity to the player. Multiply it with Time.deltaTime again to complete the formule (because time is t squared (t2))

        //running
        //if (Input.GetKeyDown(runningButton)) //if running button is pressed
        //{
        //    speed = runningSpeed; //change speed
        //}

        if (Input.GetKeyUp(runningButton)) //if running button is released
        {
            speed = originalSpeed; //set speed back to normal
            playerAnimator.SetBool("Running", false);
        }

        //crouching
        if (Input.GetKeyDown(crouchButton)) //if the crouching button is pressed
        {
            isCrouching = true;
            speed = crouchingSpeed; //set the speed slower (crouching speed)
            playerCameraObj.transform.position = playerCameraObj.transform.position + new Vector3(0, -cameraDown, 0); //change the camera position (down) depending on what posisiton it is
            playerAnimator.SetBool("Crouching", true);
        }

        if (Input.GetKeyUp(crouchButton)) //if the crouching button is released
        {
            isCrouching = false;
            speed = originalSpeed; //set the speed back to normal
            playerCameraObj.transform.position = playerCameraObj.transform.position + new Vector3(0, cameraDown, 0); //set the camera back up
            playerAnimator.SetBool("Crouching", false);
        }

        shadowPlayer.localPosition = originalPosition;

        if (isMoving)
        {
            HandleHeadBob();
        }
    }

    private void HandleHeadBob()
    {
        defaultYPos = playerCameraObj.transform.position.y;
        timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : walkBobSpeed);
        playerCameraObj.transform.position = new Vector3(playerCameraObj.transform.position.x, defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : walkBobAmount), playerCameraObj.transform.position.z);
    }
}
