using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Control the Camera Speed")]
    public float mouseSensitivity = 100f; //use this to controll the speed of the mouse

    [Header("Reference to player model")]
    public Transform playerBody; //this will be the player
    float xRotation = 0f; //will be used to rotate around the x-axis (to look up and down)

    [Header("Raycasting")]
    public float scanningDistance;

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked; //hide and lock the cursor. (So that the cursor won't leave the screen
    }

    // Update is called once per frame
    void Update()
    {
        //get the x and y location of the mouse:
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //use Time.deltaTime here to make sure that the rotation is independent of the current frame rate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; //decrease the rotion depending on the mouse. Not increasing, because then the rotation is flipped
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //make sure that they player can't rotate too far. Clamp is a way to restrict a number between two other numbers.

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //apply the rotation, Quaternion is responsible for rotation. This is also used so that line 24 can happen
        playerBody.Rotate(Vector3.up * mouseX); //rotate the player body based on the mouse movement (horizontal movement)


        //Raycasting
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * scanningDistance);

        if (Physics.Raycast(landingRay, out hit, scanningDistance))
        {
            if (hit.collider.tag == "Animal")
            {
                Debug.Log("Hij doet t!!");
            }
        }
    }
}
