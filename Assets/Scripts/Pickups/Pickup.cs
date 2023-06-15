using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject mainCamera;
    MouseLook mouseLook;

    public GameObject binocularGO;
    public GameObject binocularUI;
    public GameObject journalGO;
    public GameObject journalUI;
    public GameObject prunerGO;
    public GameObject keyGO;

    [Header("Gate Ainimators")]
    public Animator gateALeft;
    public Animator gateBRight;
    // Start is called before the first frame update
    void Start()
    {
        mouseLook = mainCamera.GetComponent<MouseLook>();
    }

    private void Update()
    {
        if (binocularGO != null) //if object is not destroyed
        {
            if (!binocularGO.activeInHierarchy) //if it's deactivated (it has been picked up)
            {
                ActivateZooming(); //activate the function
                Destroy(binocularGO); //destroy
                binocularGO = null; //set to null
            }
        }

        if (journalGO != null)
        {
            if (!journalGO.activeInHierarchy)
            {
                ActivateJournal();
                Destroy(journalGO);
                journalGO = null;
            }
        }

        if (prunerGO != null)
        {
            if (!prunerGO.activeInHierarchy)
            {
                ActivatePruner();
                Destroy(prunerGO);
                prunerGO = null;
            }
        }

        if (keyGO != null)
        {
            if (!keyGO.activeInHierarchy)
            {
                Destroy(keyGO);
                keyGO = null;
                gateALeft.Play("Gate1Aopen"); //open the gate, is needed for the story
                gateBRight.Play("Gate1Bopen");
            }
        }
    }

    void ActivateZooming()
    {
        mouseLook.canZoom = true;
        binocularUI.SetActive(true);
    }

    void ActivateJournal()
    {
        mouseLook.hasPickedUp = true;
        journalUI.SetActive(true);
    }

    void ActivatePruner()
    {
        mouseLook.canPrune = true;
    }
}
