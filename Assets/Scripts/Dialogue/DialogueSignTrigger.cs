using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSignTrigger : MonoBehaviour
{
    //The visual cue
    public List<GameObject> allSignTriggers = new List<GameObject>(); //always add the trigger where the script is in last!!!

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    //Bool for if the player is in range
    private bool playerInRange;

    //Set inactive at the start of the game
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            for (int i = 0; i < allSignTriggers.Count; i++)
            {
                Destroy(allSignTriggers[i]);
                allSignTriggers.RemoveAt(i);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
