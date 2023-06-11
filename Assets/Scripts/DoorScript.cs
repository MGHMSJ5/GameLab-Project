using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;


public class DoorScript : MonoBehaviour
{
    public Transform PlayerCamera;
    [Header("MaxDistance you can open or close the door.")]
    public float MaxDistance = 5;

    private bool opened = false;
    private Animator anim;

    public GameObject pickUpIcon;

    public AudioSource audioDoorOpen;
    public AudioSource audioDoorClose;

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit doorhit;

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {
            if (doorhit.transform.tag == "Door")
            {
                pickUpIcon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim = doorhit.transform.GetComponentInParent<Animator>();
                    opened = anim.GetBool("Opened");
                    anim.SetBool("Opened", !opened);
                    if (opened)
                    {
                        //Debug.Log("Close");
                        audioDoorClose.Play();
                    }
                    if (!opened)
                    {
                        //Debug.Log("Open");
                        audioDoorOpen.Play();
                    }
                    opened = !opened;

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "doorIsOpen", true},
                    };
                    AnalyticsService.Instance.CustomData("doorInteractable", parameters);

                }
            }
            if (doorhit.collider.tag != "Door")
            {
                pickUpIcon.SetActive(false);
            }
        }
        else
        {
            pickUpIcon.SetActive(false);
        }
    }
}
