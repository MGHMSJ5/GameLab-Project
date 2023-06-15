using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public List<GameObject> scannables = new List<GameObject>(); //all the animal and plant information blocks
    private int totalScannable; //length of list. Total number of things that can be scanned
    private int numerOfScanned; //currently scanned
    public NPC runningNPC; //jogger script
    public NPC grandmaNPC;//old lady script

    public List<GameObject> achievements = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        totalScannable = scannables.Count; //set max scannables
    }

    // Update is called once per frame
    void Update()
    {
        if (grandmaNPC.talkingCompletelyDone) //if old lady walk away
        {
            achievements[0].SetActive(true); //activate achievement
        }
        if (runningNPC.talkingCompletelyDone) //if jogger runs away (on the right path)
        {
            achievements[1].SetActive(true); //activate achievement
        }

        for (int i = 0; i < scannables.Count; i++) //go through list
        {
            if (scannables[i].activeSelf) //if player has scanned the animal/plant
            {
                numerOfScanned += 1; //increase
                scannables.RemoveAt(i); //remove from list
            }
        }

        if (numerOfScanned == totalScannable) //if the player has scanned all the animals and plants
        {
            achievements[2].SetActive(true); //activate achievement
        }
    }
}
