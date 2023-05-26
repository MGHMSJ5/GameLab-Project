using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    //The visual cue
    public GameObject mainObject;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    //Bool for if the player is in range
    private bool playerInRange;

    //new
    public KeyCode talkButton;
    public GameObject talkUI;
    public bool isFarAway; //tick on if the NPC is far away and the player has to look at them
    public bool isApproachable; //tick on in inspector if the NPC is on the map and the player has to walk to them
    public bool raycastHit;
    private bool startedDialogue;

    //Set inactive at the start of the game
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (isFarAway && raycastHit)
        {
            playerInRange = true;
        }
        if (isFarAway && !raycastHit)
        {
            playerInRange = false;
        }

        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(talkButton))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                startedDialogue = true;
            }
            if (playerInRange && !startedDialogue)
            {
                talkUI.SetActive(true);
            }
            else if(!playerInRange)
            {
                talkUI.SetActive(false);
            }
            if (startedDialogue == true)
            {
                talkUI.SetActive(false);
                startedDialogue = false;
            }
        }
        else
        {
            visualCue.SetActive(false);
        }


    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isApproachable)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" &&isApproachable)
        {
            talkUI.SetActive(false);
            playerInRange = false;
        }
    }
}
