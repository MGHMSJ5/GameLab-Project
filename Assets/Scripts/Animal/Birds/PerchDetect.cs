using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerchDetect : MonoBehaviour
{
    public bool isPlayerNear = false;
    public bool isItGround; //set to true in inspecto if this perch is on the ground

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
