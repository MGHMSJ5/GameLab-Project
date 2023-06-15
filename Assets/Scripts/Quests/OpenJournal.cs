using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenJournal : MonoBehaviour
{
    public NotebookPages notebookPages;
    private Animator questAnimator;

    public List<GameObject> dialogueBorders = new List<GameObject>(); //Dialogue - Lea
    public bool startDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        questAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notebookPages.isActive) //if the notebook has been activated
        {
            questAnimator.SetBool("Done", true);
            for (int i = 0; i < dialogueBorders.Count; i++) //Dialogue - Lea
            {
                Destroy(dialogueBorders[i]);
                dialogueBorders.RemoveAt(i);
            }
            StartCoroutine(animDone());
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
