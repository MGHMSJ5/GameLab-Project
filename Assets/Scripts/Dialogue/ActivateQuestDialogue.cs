using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestDialogue : MonoBehaviour
{
    public List<GameObject> Quests = new List<GameObject>();
    public List<GameObject> Dialogue = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i] == null)
            {
                //Debug.Log("Quest" + i + "gone")
                    Dialogue[i].SetActive(true);

                Quests.RemoveAt(i);
                Dialogue.RemoveAt(i);
                
            }

        }
    }
}
