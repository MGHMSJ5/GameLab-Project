using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying;

    private static DialogueManager instance;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;
    private bool hasBinoculars = false;
    public NotebookPages notebookPages;
    private bool hasJournal = false;
    public PauseMenu pauseMenu;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        if (currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Mouse0) || currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

        public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        Cursor.visible = true;

        playerMovement.enabled = false;
        mouseLook.canLookAround = false;
        pauseMenu.canPauseGame = false;

        if (mouseLook.canZoom)
        {
            hasBinoculars = true;
            mouseLook.canZoom = false;
        }
        if (notebookPages.canOpenJournal)
        {
            hasJournal = true;
            notebookPages.canOpenJournal = false;
        }

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        Cursor.visible = false;

        playerMovement.enabled = true;
        mouseLook.canLookAround = true;
        pauseMenu.canPauseGame = true;

        if (hasBinoculars)
        {
            mouseLook.canZoom = true;
        }

        if (hasJournal)
        {
            notebookPages.canOpenJournal = true;
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //Set the text for current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {

        List<Choice> currentChoices = currentStory.currentChoices;
        //check if the UI can handle the amount of choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given:" + currentChoices.Count);
        }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        //StartCoroutine(SelectFirstChoice());
    }

    //private IEnumerator SelectFirstChoice()
    //{
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    //}

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}
