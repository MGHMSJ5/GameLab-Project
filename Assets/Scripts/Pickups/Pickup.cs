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
    // Start is called before the first frame update
    void Start()
    {
        mouseLook = mainCamera.GetComponent<MouseLook>();
    }

    private void Update()
    {
        if (binocularGO != null)
        {
            if (!binocularGO.activeInHierarchy)
            {
                ActivateZooming();
                Destroy(binocularGO);
                binocularGO = null;
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
