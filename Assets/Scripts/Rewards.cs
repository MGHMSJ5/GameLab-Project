using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    public List<GameObject> RewardGO = new List<GameObject>();
    public static bool scanCormorant = false;
    public static bool scanDuck = false;
    public static bool scanOtter = false;
    public static bool scanFallowDeer = false;
    public static bool scanRedDeer = false;
    public static bool scanRobin = false;
    public static bool scanTit = false;
    public static bool scanRabbit = false;
    public static bool scanDaisy = false;
    public static bool scanLemna = false;
    public static bool scanStratiotes = false;
    public static bool scanViola = false;
    public static bool scanLinnaea = false;
    public static bool scanCornus = false;
    public static bool scanLobelia = false;
    public static bool scanCornusMas = false;
    public static bool scanLeafyGoose = false;

    public List<bool> ScannedBools = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        ScannedBools.Add(scanCormorant);
        ScannedBools.Add(scanOtter);
        ScannedBools.Add(scanFallowDeer);
        ScannedBools.Add(scanRedDeer);
        ScannedBools.Add(scanRobin);
        ScannedBools.Add(scanTit);
        ScannedBools.Add(scanRabbit);
        ScannedBools.Add(scanDaisy);
        ScannedBools.Add(scanLemna);
        ScannedBools.Add(scanStratiotes);
        ScannedBools.Add(scanViola);
        ScannedBools.Add(scanLinnaea);
        ScannedBools.Add(scanCornus);
        ScannedBools.Add(scanLobelia);
        ScannedBools.Add(scanCornusMas);
        ScannedBools.Add(scanLeafyGoose);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < RewardGO.Count; i++)
        {
            if (RewardGO[0].activeSelf)
            {
                scanCormorant = true;
                ScannedBools[0] = true;
            }
            if (RewardGO[1].activeSelf)
            {
                scanDuck = true;
                ScannedBools[1] = true;
            }
            if (RewardGO[2].activeSelf)
            {
                scanOtter = true;
                ScannedBools[2] = true;
            }
            if (RewardGO[3].activeSelf)
            {
                scanFallowDeer = true;
                ScannedBools[3] = true;
            }
            if (RewardGO[4].activeSelf)
            {
                scanRedDeer = true;
                ScannedBools[4] = true;
            }
            if (RewardGO[5].activeSelf)
            {
                scanRobin = true;
                ScannedBools[5] = true;
            }
            if (RewardGO[6].activeSelf)
            {
                scanTit = true;
                ScannedBools[6] = true;
            }
            if (RewardGO[7].activeSelf)
            {
                scanRabbit = true;
                ScannedBools[7] = true;
            }
            if (RewardGO[8].activeSelf)
            {
                scanDaisy = true;
                ScannedBools[8] = true;
            }
            if (RewardGO[9].activeSelf)
            {
                scanLemna = true;
                ScannedBools[9] = true;
            }
            if (RewardGO[10].activeSelf)
            {
                scanStratiotes = true;
                ScannedBools[10] = true;
            }
            if (RewardGO[11].activeSelf)
            {
                scanViola = true;
                ScannedBools[11] = true;
            }
            if (RewardGO[12].activeSelf)
            {
                scanLinnaea = true;
                ScannedBools[12] = true;
            }
            if (RewardGO[13].activeSelf)
            {
                scanCornus = true;
                ScannedBools[13] = true;
            }
            if (RewardGO[14].activeSelf)
            {
                scanLobelia = true;
                ScannedBools[14] = true;
            }
            if (RewardGO[15].activeSelf)
            {
                scanCornusMas = true;
                ScannedBools[15] = true;
            }
            if (RewardGO[16].activeSelf)
            {
                scanLeafyGoose = true;
                ScannedBools[16] = true;
            }

            if (ScannedBools[i])
            {
                RewardGO[i].SetActive(true);
            }
        }

    }
}
