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

    void Update()
    {
        if (!isActive && Input.GetKey(getJournal))
        {
            journalObj.SetActive(true);
            notebookPages.SetActive(true);
            StartCoroutine(WaitALittleA());
            Time.timeScale = 0f;
        }

        if (isActive && Input.GetKey(getJournal))
        {
            journalObj.SetActive(false);
            notebookPages.SetActive(false);
            StartCoroutine(WaitALittleB());
            Time.timeScale = 1f;
        }
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

    IEnumerator WaitALittleA()
    {
        yield return new WaitForSeconds(0.3f);
        isActive = true;
    }

    IEnumerator WaitALittleB()
    {
        yield return new WaitForSeconds(0.3f);
        isActive = false;
    }
}
