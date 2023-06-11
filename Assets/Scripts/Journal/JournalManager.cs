using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public GameObject journalObj;
    bool isActive = false;
    public KeyCode getJournal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && Input.GetKey(getJournal))
        {
            journalObj.SetActive(true);
            StartCoroutine(WaitALittleA());
        }

        if (isActive && Input.GetKey(getJournal))
        {
            journalObj.SetActive(false);
            StartCoroutine(WaitALittleB());
        }
    }

    IEnumerator WaitALittleA()
    {
        yield return new WaitForSeconds(0.3f);
        isActive = true;
    }

    IEnumerator WaitALittleB()
    {
        yield return new WaitForSeconds(0.3f);
        isActive = false;
    }
}
