using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [Header("Control the Camera Speed")]
    public float mouseSensitivity = 100f; //use this to controll the speed of the mouse

    [Header("Reference to player model")]
    public Transform playerBody; //this will be the player
    float xRotation = 0f; //will be used to rotate around the x-axis (to look up and down)

    [Header("Zooming")]
    public Camera cameraOptions; //this will be used to change the fieldOfView of the camera
    public GameObject binocularVision; //this is the binocular obj which will be visible when the player is zooming
    public KeyCode zoomButton; //the button that will be used to zoom
    bool canZoom = true; //if true > player can zoom, if false > player can't zoom
    public float timeToZoom = 0.3f; //the time it takes to fully zoom in
    public float zoomPOV = 30f; //how far the player can zoom in
    private float defaultPOV; //this will be the original fieldOfFiew
    private Coroutine zoomRoutine;

    [Header("Raycasting")]
    public float scanningDistance; //this will be dinstance the player is able to 'scan' an animal or plant
    int infoToAppear;
    float timerHit = 0;
    public float scanTime = 4f;
    public List<GameObject> InformationBlocks = new List<GameObject>(); //list with all of the parent GameObjects of the information about the endangered animals and plants. 
    public Slider scanSlider;
    public GameObject scanDone;

    private void Awake()
    {
        scanSlider.maxValue = scanTime;
        defaultPOV = cameraOptions.fieldOfView; //set the original fieldOfView
    }
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

        //zoom
        if (canZoom) //if the player can zoom in
        {
            HandleZoom(); //run this
        }

        if (defaultPOV != cameraOptions.fieldOfView) //if the fieldOfView of the camera is not the original view (the player is zooming in/out), then
        {
            binocularVision.SetActive(true); //active this so that the player has the bonocular vision
        }
        else if (defaultPOV == cameraOptions.fieldOfView) //if the plsyer is not zooming in or out (the fieldOfView is int eh original setting)
        {
            binocularVision.SetActive(false); //deactivate the binocular vision
        }


        //Raycasting
        RaycastHit hit; //is storing everything that gets hit by the ray
        Ray landingRay = new Ray(transform.position, transform.forward); //the direction of the ray. transform.forward is used to point the ray in the direction the camera is facing (since the player will be able to scan from the camera's view)

        Debug.DrawRay(transform.position, transform.forward * scanningDistance); //line that will be drawn in the scene to see the raycast

        if (Physics.Raycast(landingRay, out hit, scanningDistance) && cameraOptions.fieldOfView == zoomPOV) //if the raycast (the ray is in the direction of the camera, hit is what it will store, scanningdistance is the length of the ray), and if the player is not zooming in/out
        {
            for (int i = 0; i < InformationBlocks.Count; i++)
            {
                if (InformationBlocks[i].name == hit.collider.tag)
                {
                    timerHit += Time.deltaTime;
                    infoToAppear = i;
                }
                if(hit.collider.tag != InformationBlocks[infoToAppear].name)
                {
                       timerHit = 0;
                }
            }
        }else
        {
            timerHit = 0;
        }

        if (timerHit > scanTime)
        {
            StartCoroutine(ScanIsDone());
        }
        scanSlider.value = timerHit;
    }
    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomButton)) //if zoom button is pressed
        {
            if (zoomRoutine != null) //check if the player is mid zoom
            {
                StopCoroutine(zoomRoutine); //stop this
                zoomRoutine = null; //set to null
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true)); //start this with true parameter (enter the zoom state)
        }

        if (Input.GetKeyUp(zoomButton)) //if zoom button is releaed
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false)); //start this with false parameter (enter the zoom state)
        }
    }
    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetPOV = isEnter ? zoomPOV : defaultPOV; //set target to zoomPOV if parameter is true (it will zooming in), set target to defaultPOV if parameter is false (it will zoom out)
        float startingPOV = cameraOptions.fieldOfView;  //set the current fieldOfView (can be different if player pressed button while it was zooming in or out)
        float timeElapsed = 0; //for timer

        while (timeElapsed < timeToZoom) //while the timer is less than the amount of time to zoom in/out
        {
            cameraOptions.fieldOfView = Mathf.Lerp(startingPOV, targetPOV, timeElapsed / timeToZoom); //changes the fieldOfView of the camera by going from the current fiew, to the target fiew (depends on true or false), the time step is timeElapsed/timeToZoom
            timeElapsed += Time.deltaTime; //increase in time
            yield return null; //to wait for the next frame
        }

        cameraOptions.fieldOfView = targetPOV; //(just to be sure) set the fieldOfView to the target view (depends if the parameter was true (zoom in) of false(zoom out))
        zoomRoutine = null;
    }

    IEnumerator ScanIsDone()
    {
        InformationBlocks[infoToAppear].SetActive(true);
        InformationBlocks.RemoveAt(infoToAppear);
        timerHit = 0;
        infoToAppear = 0;
        scanDone.SetActive(true);
        yield return new WaitForSeconds(3f);
        scanDone.SetActive(false);
    }
}
