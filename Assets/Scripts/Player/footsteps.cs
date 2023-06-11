using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;
    public AudioSource crouchingSound;

    private PlayerMovement playerMovement;

    private bool isCrouching = false;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))//if this is being pressed the player will move
        {
            if (!isCrouching)  //if the player is not crouching, this will play
            {
                footstepsSound.enabled = true;
                crouchingSound.enabled = false;
            }
            if (isCrouching) //if the player is crouching, this will play
            {
                footstepsSound.enabled = false;
                crouchingSound.enabled = true;
            }
        }
        else //if the player is standing still, this will play
        {
            footstepsSound.enabled = false;
            crouchingSound.enabled = false;
        }

        if (Input.GetKeyDown(playerMovement.crouchButton))  //reference to other script, also if crouching button isd being pressed down
        {
            isCrouching = true;
        }

        if (Input.GetKeyUp(playerMovement.crouchButton)) //reference to other script, also if crouching button is released
        {
            isCrouching = false;
        }
    }
}
