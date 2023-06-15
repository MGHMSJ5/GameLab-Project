using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseLook : MonoBehaviour
{
    public bool canLookAround = true;
    [Header("Control the Camera Speed")]
    public float mouseSensitivity = 100f; //use this to controll the speed of the mouse

    [Header("Reference to player model")]
    public Transform playerBody; //this will be the player
    float xRotation = 0f; //will be used to rotate around the x-axis (to look up and down)

    [Header("Zooming")]
    public Camera cameraOptions; //this will be used to change the fieldOfView of the camera
    public GameObject binocularVision; //this is the binocular obj which will be visible when the player is zooming
    public KeyCode zoomButton; //the button that will be used to zoom
    public bool canZoom; //if true > player can zoom, if false > player can't zoom
    public float timeToZoom = 0.3f; //the time it takes to fully zoom in
    public float zoomPOV = 30f; //how far the player can zoom in
    private float defaultPOV; //this will be the original fieldOfView
    private Coroutine zoomRoutine;
    public GameObject journal;
    NotebookPages notebookPages;
    public int zoomSensitivity = 1;

    public bool isZooming = false;

    [Header("Scanning")]
    public float scanningDistance; //this will be dinstance the player is able to 'scan' an animal or plant
    int infoToAppear;
    float timerHit = 0; //this is used to gove a time that would take to scan an animal or plant. Timer is linked to a slider in the UI
    public float scanTime = 4f;
    public List<GameObject> InformationBlocks = new List<GameObject>(); //list with all of the parent GameObjects of the information about the endangered animals and plants. 
    public List<GameObject> RewardCards = new List<GameObject>();
    public Slider scanSlider;
    public GameObject scanDone;
    public LayerMask ignoreBordersScan;
    public bool hasPickedUp;
    public GameObject notificationUI;
    public GameObject confetti;
    public int notificationCounter;
    public string numberOfNotifications;
    public TextMeshProUGUI notificationNumberUI;


    [Header("Pickup")]
    public float pickupDistance;
    public KeyCode pickupButton;
    public GameObject buttonUI;

    public DialogueManager dialogueManager;

    [Header("Pruner")]
    public KeyCode prunerKey;
    public float pruningDistance;
    public bool canPrune = false;
    public GameObject pruneUI;

    [Header("Long distance NPC")]
    public float talkDistance;
    private bool wasHit;
    [SerializeField]
    NPCDialogueTrigger npcScript;
    public LayerMask ignoreBorders;

    private void Awake()
    {
        scanSlider.maxValue = scanTime;
        defaultPOV = cameraOptions.fieldOfView; //set the original fieldOfView
    }
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; //hide and lock the cursor. (So that the cursor won't leave the screen
        notebookPages = journal.GetComponent<NotebookPages>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canLookAround) //is player can't look around
        {
            //get the x and y location of the mouse:
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //use Time.deltaTime here to make sure that the rotation is independent of the current frame rate
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY; //decrease the rotion depending on the mouse. Not increasing, because then the rotation is flipped
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); //make sure that they player can't rotate too far. Clamp is a way to restrict a number between two other numbers.

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //apply the rotation, Quaternion is responsible for rotation. This is also used so that line 24 can happen
            playerBody.Rotate(Vector3.up * mouseX); //rotate the player body based on the mouse movement (horizontal movement)
        }

        //zoom
        if (canZoom) //if the player can zoom in (this will be true if the player picked up the binoculars
        {
            HandleZoom(); //run this
        }

           // if (defaultPOV != cameraOptions.fieldOfView) //if the fieldOfView of the camera is not the original view (the player is zooming in/out), then
            //{
            //    binocularVision.SetActive(true); //active this so that the player has the bonocular vision
            //}
            //else if (defaultPOV == cameraOptions.fieldOfView) //if the plsyer is not zooming in or out (the fieldOfView is int eh original setting)
           // {
            //    binocularVision.SetActive(false); //deactivate the binocular vision
            //}

            if (hasPickedUp) //picked up journal. This was first used to make sure te the player can't open the journal when zooming. Byt that changed
            {
                if (defaultPOV != cameraOptions.fieldOfView) //if the fieldOfView of the camera is not the original view (the player is zooming in/out), then
                {
                    notebookPages.canOpenJournal = true;
                }
                else if (defaultPOV == cameraOptions.fieldOfView && !dialogueManager.dialogueIsPlaying) //if the plsyer is not zooming in or out (the fieldOfView is int eh original setting)
                {
                    notebookPages.canOpenJournal = true;
                }
            }


        //Raycasting scanning
        RaycastHit hit; //is storing everything that gets hit by the ray
        Ray landingRay = new Ray(transform.position, transform.forward); //the direction of the ray. transform.forward is used to point the ray in the direction the camera is facing (since the player will be able to scan from the camera's view)

        //Debug.DrawRay(transform.position, transform.forward * pickupDistance); //line that will be drawn in the scene to see the raycast

        if (Physics.Raycast(landingRay, out hit, scanningDistance,~ ignoreBordersScan) && cameraOptions.fieldOfView != defaultPOV && hasPickedUp) //if the raycast (the ray is in the direction of the camera, hit is what it will store,
                                                                                                                                                  //scanningdistance is the length of the ray), and if the player is zooming in/out. and has picked up the journal
        {
            for (int i = 0; i < InformationBlocks.Count; i++) //go through all the information blocks
            {
                if (InformationBlocks[i].name == hit.collider.tag) //find the matching block
                {
                    timerHit += Time.deltaTime; //start the scan time
                    infoToAppear = i; //this will be the animal/plant information that is scanning and will be scanned
                }
                if(hit.collider.tag != InformationBlocks[infoToAppear].name && timerHit > 0) //if the player isn't hitting the animal/plant that they were scanning
                {
                       timerHit -= (Time.deltaTime/8); //reduce the timer
                }
            }
        }else if(timerHit > 0) //if the player is hitting the air
        {
            timerHit -= (Time.deltaTime/8); //reduce timer
        }

        if (timerHit <= 0) //set timer to exactly 0 when it's lower than 0
        {
            timerHit = 0;
        }
        if (timerHit > scanTime) //if the player looked at the animal/plant for a certain time
        {
            StartCoroutine(ScanIsDone()); //start this
        }
        scanSlider.value = timerHit; //set hte value of the slider to the value of the timer. When the player scans an animal/plant, they can see the progress of the scanning

        //notification on UI
        numberOfNotifications = notificationCounter.ToString(); //notification counter will be increased in  'ScanIsDone'
        notificationNumberUI.text = numberOfNotifications;

        //Rayasting pickup
        if (Physics.Raycast(landingRay, out hit, pickupDistance)) //raycast for picking things up
        {
            if (hit.collider.tag == "Pickup") //if object has pickup tag, then ↓
            {
                buttonUI.SetActive(true); //'E' appears
                if (Input.GetKeyDown(pickupButton)) //if 'E' is pressed
                {
                    hit.transform.gameObject.SetActive(false); //deactivate gameobjet
                }
            }
            if (hit.collider.tag != "Pickup") //if hitting something else that doesn't have pickup tag
            {
                buttonUI.SetActive(false); //'E' disappeard
            }
        }
        else
        {
            buttonUI.SetActive(false); //if hitting nothing, button also disappeard
        }

        //pruning - Marije
        if (Physics.Raycast(landingRay, out hit, pruningDistance)) //raycast for pruner
        {
            if (hit.collider.tag == "Pruned" && canPrune) //if obj has pruner tag, and if the player picked up the pruner
            {
                pruneUI.SetActive(true); //pruner UI on screen
                if (Input.GetKeyDown(prunerKey)) //if pressed 'E'' 
                {
                    Destroy(hit.collider.gameObject); //destroy the thing that could be pruned
                }
            }
            if (hit.collider.tag != "Pruned") //if hitting anything else. icon gone
            {
                pruneUI.SetActive(false);
            }
        }
        else //if hitting nothing, icon gone
        {
            pruneUI.SetActive(false);
        }

        //long distance NPC
        if (Physics.Raycast(landingRay, out hit, talkDistance, ~ ignoreBorders)) //raycast, ignore these borders that are in certain layers
        {
            if (hit.collider.tag == "NPC") //if collider is NPC
            {
                npcScript = hit.collider.gameObject.GetComponent<NPCDialogueTrigger>(); //get the script
                npcScript.raycastHit = true; //set to true
                wasHit = true; //↓
            }
            else if (wasHit) //↓ if not hitting the NPC trigger, then reset↓ (wasHit is used, because otherwise the UI would also be gone when in range of other NPC's
            {
                npcScript.talkUI.SetActive(false);
                npcScript.raycastHit = false;
                wasHit = false;
                npcScript = null;
            }
        }
        else if(wasHit) //↓ if not hitting the NPC trigger, then reset↓ (wasHit is used, because otherwise the UI would also be gone when in range of other NPC's
        {
            npcScript.talkUI.SetActive(false);
            npcScript.raycastHit = false;
            wasHit = false;
            npcScript = null;
        }
        Debug.DrawRay(transform.position, transform.forward * talkDistance); //used this to test and see from how far away the player could talk
    }
    private void HandleZoom()
    {
        if (isZooming && Input.GetKeyDown(KeyCode.Escape)) //is player is zoming and pressed ESC, they would quit
        {
            StartCoroutine(QuitZooming());
        }
        if (Input.GetKeyDown(zoomButton)) //if zoom button is pressed
        {
            if (isZooming)
            {
                StartCoroutine(QuitZooming());
            }else
            {
                StartCoroutine(StartZooming());
            }
            
            //Older code↓
            //if (zoomRoutine != null) //check if the player is mid zoom
            //{
            //    StopCoroutine(zoomRoutine); //stop this
            //    zoomRoutine = null; //set to null
            //}

            //zoomRoutine = StartCoroutine(ToggleZoom(true)); //start this with true parameter (enter the zoom state)
        }

       // if (Input.GetKeyUp(zoomButton)) //if zoom button is releaed
        //{
            //if (zoomRoutine != null)
            //{
            //    StopCoroutine(zoomRoutine);
            //    zoomRoutine = null;
            //}
            //zoomRoutine = StartCoroutine(ToggleZoom(false)); //start this with false parameter (enter the zoom state)
        //}
        if (isZooming) //this is used to make it so that the zooming is udated depending on the mousewheel scrolling
        {
            float scrollData = Input.mouseScrollDelta.y * -2; //how fast the scrolling is
            float _POVChange = scrollData * zoomSensitivity;
            float newPOV = cameraOptions.fieldOfView + _POVChange;

            newPOV = Mathf.Clamp(newPOV, zoomPOV, defaultPOV); //change the newPOV

            cameraOptions.fieldOfView = newPOV; //set the new zoom view
        }
    }
    public IEnumerator QuitZooming()
    {
        binocularVision.SetActive(false); //binocular vision is gone
        float startingPOV = cameraOptions.fieldOfView; //set the current zooming view
        float timeElapsed = 0;//set timer

        while (timeElapsed < timeToZoom) //view will zoom to normal position using Lerp in a certain time
        {
            cameraOptions.fieldOfView = Mathf.Lerp(startingPOV, defaultPOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cameraOptions.fieldOfView = defaultPOV;
        isZooming = false; //is not zooming anymore
    }

    private IEnumerator StartZooming()
    {
        isZooming = true;//is now zooming
        binocularVision.SetActive(true); //active this so that the player has the bonocular vision
        float startingPOV = cameraOptions.fieldOfView; //set current zooming view
        float timeElapsed = 0; //set timer

        while (timeElapsed < timeToZoom) //view will zoom to normal position using Lerp in a certain time
        {
            cameraOptions.fieldOfView = Mathf.Lerp(startingPOV, defaultPOV - zoomPOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cameraOptions.fieldOfView = defaultPOV - zoomPOV;
    }

    IEnumerator ScanIsDone()
    {
        notificationUI.SetActive(true); //activate the red dot that is on the UI of the journal
        InformationBlocks[infoToAppear].SetActive(true); //set the right information GameObject active in the inspector, so that when the player opens the journal and goes to the right page, the information of the animal/plant will be there
        InfoNotification infoNotification = InformationBlocks[infoToAppear].GetComponent<InfoNotification>(); //get the script of the notifications
        infoNotification.hasSeen = true; //set hasSeen to true
        //was used to make it so that the last scanned animal/plant would be opened when player opens the journal↓
        //for (int i = 0; i < notebookPages.Pages.Count; i++)
        //{
            //if (notebookPages.Pages[i].tag == InformationBlocks[infoToAppear].name)
            //{
                //Debug.Log(InformationBlocks[infoToAppear].name);
                //notebookPages.Pages[notebookPages.currentPage].SetActive(false);
                //notebookPages.Pages[i].SetActive(true);
                //notebookPages.currentPage = i;
            //}
        //}

        for (int i = 0; i < RewardCards.Count; i++)//go through the reward cards, set the matching reward card to true so that it will appear in screen
        {
            if (RewardCards[i].name.Contains(InformationBlocks[infoToAppear].name))
            {
                RewardCards[i].SetActive(true);
            }
        }
        notificationCounter += 1; //increase the notification counter
        InformationBlocks.RemoveAt(infoToAppear); //remove from list so that it can't be scanned again
        infoToAppear = 0; //reset
        timerHit = 0; //reset
        scanDone.SetActive(true); //check is now in screen. To show the player that they scanned the animal/plant
        yield return new WaitForSeconds(2f); //wait for the animation to be done
        confetti.SetActive(true); //confetti appears
        scanDone.SetActive(false); //check disappeard
        yield return new WaitForSeconds(3f); //wait for confetti
        confetti.SetActive(false); //confetti disappears
    }

    private IEnumerator ToggleZoom(bool isEnter) //was used for the previous version of zooming. Where you had to hold C to zoom in, and release it zo zoom out
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
}
