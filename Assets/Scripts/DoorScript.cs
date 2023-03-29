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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pressed();
            Debug.Log("You Pressed E");
        }

    }

    void Pressed()
    {
        RaycastHit doorhit;

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {
            if (doorhit.transform.tag == "Door")
            {
                anim = doorhit.transform.GetComponentInParent<Animator>();
                opened = !opened;
                anim.SetBool("Opened", !opened);
            }
        }
    }
}
