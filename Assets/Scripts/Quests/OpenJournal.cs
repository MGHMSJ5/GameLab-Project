using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenJournal : MonoBehaviour
{
    public GameObject journalManager;
    NotebookPages notebookPages;
    private Animator questAnimator;

    public List<GameObject> dialogueBorders = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        notebookPages = journalManager.GetComponent<NotebookPages>();
        questAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notebookPages.isActive)
        {
            questAnimator.SetBool("Done", true);
            for (int i = 0; i < dialogueBorders.Count; i++)
            {
                Destroy(dialogueBorders[i]);
                dialogueBorders.RemoveAt(i);
            }
            StartCoroutine(animDone());
        }
    }
    IEnumerator animDone()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
