using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRoadBlocked : MonoBehaviour
{
    //The visual cue
    public GameObject mainObject;
    public Pickup pickups;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset inkJSON1;

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
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && pickups.prunerGO != null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            Destroy(mainObject);
        }

        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && pickups.prunerGO == null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON1);
            Destroy(mainObject);
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
