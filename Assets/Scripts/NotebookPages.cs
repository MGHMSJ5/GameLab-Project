using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookPages : MonoBehaviour
{
    int currentPage = 0;

    public List<GameObject> Pages = new List<GameObject>();

    public GameObject notebookPages;
    public GameObject journalObj;
    public bool isActive = false;
    public KeyCode getJournal;

    public bool canOpenJournal;

    public GameObject scanDoneUI;

    public MouseLook mouseLook;

    void Update()
    {
        if (canOpenJournal)
        {
            if (!isActive && Input.GetKeyDown(getJournal) && !mouseLook.isZooming)
            {
                journalObj.SetActive(true);
                notebookPages.SetActive(true);
                scanDoneUI.SetActive(false); //remove the check from screen. this check appears after done scanning. And will stay there if notebook is activated without this line of code
                //Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0f;
                mouseLook.canZoom = false;
            }

            if (isActive && Input.GetKeyDown(getJournal))
            {
                journalObj.SetActive(false);
                notebookPages.SetActive(false);
                Time.timeScale = 1f;
                //Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.canZoom = true;
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
        Pages[5].SetActive(true);
        currentPage = 5;
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
