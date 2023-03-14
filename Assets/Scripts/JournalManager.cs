using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public GameObject journalObj;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && Input.GetKey(KeyCode.Q))
        {
            journalObj.SetActive(true);
            StartCoroutine(WaitALittleA());
        }

        if (isActive && Input.GetKey(KeyCode.Q))
        {
            journalObj.SetActive(false);
            StartCoroutine(WaitALittleB());
        }
    }

    IEnumerator WaitALittleA()
    {
        yield return new WaitForSeconds(1);
        isActive = true;
    }

    IEnumerator WaitALittleB()
    {
        yield return new WaitForSeconds(1);
        isActive = false;
    }
}
