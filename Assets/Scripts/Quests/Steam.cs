using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public List<GameObject> scannables = new List<GameObject>();
    private int totalScannable;
    private int numerOfScanned;
    public NPC runningNPC;
    public NPC grandmaNPC;

    public List<GameObject> achievements = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        totalScannable = scannables.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (grandmaNPC.talkingCompletelyDone)
        {
            achievements[0].SetActive(true);
        }
        if (runningNPC.talkingCompletelyDone)
        {
            achievements[1].SetActive(true);
        }

        for (int i = 0; i < scannables.Count; i++)
        {
            if (scannables[i].activeSelf)
            {
                numerOfScanned += 1;
                scannables.RemoveAt(i);
            }
        }

        if (numerOfScanned == totalScannable)
        {
            achievements[2].SetActive(true);
        }
    }
}
