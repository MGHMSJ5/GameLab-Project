using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject mainCamera;
    MouseLook mouseLook;

    public GameObject binocularGO;
    public GameObject journalGO;
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
    }

    void ActivateZooming()
    {
        mouseLook.canZoom = true;
    }

    void ActivateJournal()
    {
        mouseLook.hasPickedUp = true;
    }
}
