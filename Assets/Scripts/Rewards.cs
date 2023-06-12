using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    public List<GameObject> RewardGO = new List<GameObject>();
    public static bool scanCormorant;
    public static bool scanDuck;
    public static bool scanOtter;
    public static bool scanFallowDeer;
    public static bool scanRedDeer;
    public static bool scanRobin;
    public static bool scanTit;
    public static bool scanRabbit;
    public static bool scanDaisy;
    public static bool scanLemna;
    public static bool scanStratiotes;
    public static bool scanViola;
    public static bool scanLinnaea;
    public static bool scanCornus;
    public static bool scanLobelia;
    public static bool scanCornusMas;
    public static bool scanLeafyGoose;

    public List<bool> ScannedBools = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanCormorant);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < RewardGO.Count; i++)
        {
            if (RewardGO[i].activeSelf)
            {
                ScannedBools[i] = true;
            }
        }
    }
}
