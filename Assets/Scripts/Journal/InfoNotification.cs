using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoNotification : MonoBehaviour
{
    public bool hasSeen = false;
    public GameObject notificationUI;

    public List<GameObject> Notifications = new List<GameObject>();

    public GameObject mainCamera;
    MouseLook mouseLook;

    private void Start()
    {
        mouseLook = mainCamera.GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSeen)
        {
            mouseLook.notificationCounter -= 1;
            hasSeen = false;
        }
        if (mouseLook.notificationCounter == 0)
        {
            notificationUI.SetActive(false);
        }
    }
}
