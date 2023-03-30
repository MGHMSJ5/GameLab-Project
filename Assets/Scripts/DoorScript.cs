using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform PlayerCamera;
    [Header("MaxDistance you can open or close the door.")]
    public float MaxDistance = 5;

    private bool opened = false;
    private Animator anim;

    public GameObject pickUpIcon;

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
                    anim.SetBool("Opened", !opened);
                    opened = !opened;
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
