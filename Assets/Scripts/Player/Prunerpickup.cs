using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prunerpickup : MonoBehaviour
{
    public KeyCode prunerKey;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pruned")
        {
            if (Input.GetKeyDown(prunerKey))
            {
                Destroy(other);
            }
        }
    }
}

