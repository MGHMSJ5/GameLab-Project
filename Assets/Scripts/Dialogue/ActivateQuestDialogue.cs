using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestDialogue : MonoBehaviour
{
    public GrabItems grabItems;
    public ScanFlower scanFlower;
    public OpenJournal openJournal;
    public List<GameObject> Dialogue = new List<GameObject>();

    void Update()
    {
        if (grabItems.startDialogue && Dialogue[0] != null)
        {
            Dialogue[0].SetActive(true);
        }
        if (scanFlower.startDialogue && Dialogue[1] != null)
        {
            Dialogue[1].SetActive(true);
        }
        if (openJournal.startDialogue && Dialogue[2] != null)
        {
            Dialogue[2].SetActive(true);
        }
    }
}
