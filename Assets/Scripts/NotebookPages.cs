using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookPages : MonoBehaviour
{
    int currentPage = 0;

    public List<GameObject> Pages = new List<GameObject>();

    public GameObject notebookPages;
    public GameObject journalObj;
    bool isActive = false;
    public KeyCode getJournal;

    public bool canOpenJournal = true;

    void Update()
    {
        if (canOpenJournal)
        {
            if (!isActive && Input.GetKeyDown(getJournal))
            {
                journalObj.SetActive(true);
                notebookPages.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0f;

            }

            if (isActive && Input.GetKeyDown(getJournal))
            {
                journalObj.SetActive(false);
                notebookPages.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (!isActive && Input.GetKeyUp(getJournal))
            {
                isActive = true;
            }

            if (isActive && Input.GetKeyUp(getJournal))
            {
                StartCoroutine(WaitToSetFalse());
            }
        }
    }

    IEnumerator WaitToSetFalse()
    {
        yield return new WaitForSeconds(0.2f);
        isActive = false;
    }

    public void ToPlants()
    {
        Pages[currentPage].SetActive(false);
        Pages[2].SetActive(true);
        currentPage = 2;
    }

    public void ToAnimals()
    {
        Pages[currentPage].SetActive(false);
        Pages[0].SetActive(true);
        currentPage = 0;
    }

    public void NextPage()
    {
            Pages[currentPage].SetActive(false);
            currentPage = currentPage + 1;
            Pages[currentPage].SetActive(true);
    }

    public void PreviousPage()
    {
        Pages[currentPage].SetActive(false);
        currentPage = currentPage - 1;
        Pages[currentPage].SetActive(true);
    }
}
