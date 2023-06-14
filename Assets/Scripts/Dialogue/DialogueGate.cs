using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to be able to change scenes in Unity

public class DialogueGate : MonoBehaviour
{
    [Header("Referneces")]
    public GameObject mainObject;
    public Pickup pickups;
    public GameObject blackness;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset inkJSON1;

    //Bool for if the player is in range
    private bool playerInRange;

    private bool dialogueStarted = false;

    //Set inactive at the start of the game
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && pickups.keyGO != null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            StartCoroutine(waitForPlayer());
        }

        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && pickups.keyGO == null)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON1);
            StartCoroutine(waitForPlayer());
            dialogueStarted = true;
            //Fix with tag ↓
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //go to the previous scene that is in the queue (In Build Settings).
        }

        if (dialogueStarted && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            blackness.SetActive(true);
            Animator blackFade = blackness.GetComponent<Animator>();
            blackFade.SetBool("ToNothing", false);
            blackFade.SetBool("ToBlack", true);
            StartCoroutine(waitForBlack());
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

    IEnumerator waitForPlayer()
    {
        playerInRange = false;
        yield return new WaitForSeconds(30);
    }

    IEnumerator waitForBlack()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //go to the previous scene that is in the queue (In Build Settings).
    }
}
