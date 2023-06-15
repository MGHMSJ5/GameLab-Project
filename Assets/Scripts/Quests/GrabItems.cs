using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabItems : MonoBehaviour
{
    public List<GameObject> itemsToPickUp = new List<GameObject>(); //put the items in here that need to be grabbed to complete quest
    public TextMeshProUGUI questCounter; //is the UI that used to keep track of the collected items
    private int maxItems; //will be tha max amount of items to collect
    private int currentItemsCounter; //int that keeps track of collected items
    private string counterInString; //is used to set int to string and use it in UI

    private Animator questAnimator; //animator of quest
    public Animator controlsAnimator;//animator of controls UI

    public bool isFirstQuest; //is it the first quest?
    public bool startDialogue = false;
    void Start()
    {
        questAnimator = GetComponent<Animator>(); //get the animator of the quest
        maxItems = itemsToPickUp.Count; //set max grabable items

        if (isFirstQuest)
        {
            controlsAnimator.Play("Out"); //make the controls disappear
        }
    }
    void Update()
    {
        counterInString = currentItemsCounter + "/" + maxItems; //set the string for the UI
        questCounter.text = counterInString; //set the text
        for (int i = 0; i < itemsToPickUp.Count; i++) //go trough list
        {
            if (itemsToPickUp[i] == null) //if item is piked up
            {
                currentItemsCounter += 1; //go up, so that player can see that there is progress in the quest
                itemsToPickUp.Remove(itemsToPickUp[i]);//remove from list so that this won't happen again
            }
        }

        if (currentItemsCounter == maxItems) //is the player has picked up the items
        {
            questAnimator.SetBool("Done", true); //quest is done animation
            StartCoroutine(animDone()); //wait a bit for the animation to be done and deactivate the quest
        }
    }
    IEnumerator animDone()
    {
        yield return new WaitForSeconds(1);
        startDialogue = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
