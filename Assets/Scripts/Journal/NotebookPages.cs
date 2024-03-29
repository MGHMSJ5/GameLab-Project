using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookPages : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI pageNumberUI;

    public MouseLook mouseLook;

    public GameObject notebookPages;
    public GameObject journalObj;
    public GameObject scanDoneUI;

    [Header("Journal")]
    public List<GameObject> Pages = new List<GameObject>();
    public int currentPage = 0;
    public bool isActive = false;
    public KeyCode getJournal;

    public bool canOpenJournal;

    void Update()
    {
        pageNumberUI.text = (currentPage + 1).ToString();
        if (canOpenJournal)
        {
            if (!isActive && Input.GetKeyDown(getJournal))
            {
                journalObj.SetActive(true);
                notebookPages.SetActive(true);
                scanDoneUI.SetActive(false); //remove the check from screen. this check appears after done scanning. And will stay there if notebook is activated without this line of code
                //Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0f;
                mouseLook.canZoom = false;
                if (mouseLook.isZooming)
                {
                    Time.timeScale = 1f;
                    mouseLook.StartCoroutine(mouseLook.QuitZooming());
                }
            }

            if (!mouseLook.isZooming && isActive)
            {
                Time.timeScale = 0f;
            }

            if (isActive && Input.GetKeyDown(getJournal) || isActive && Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log("Press");
                journalObj.SetActive(false);
                notebookPages.SetActive(false);
                Time.timeScale = 1f;
                //Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.canZoom = true;
                isActive = false;
            }

            if (journalObj.activeInHierarchy)
            {
                isActive = true;
            }
            if (!journalObj.activeInHierarchy)
            {
                isActive = false;
            }
        }
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
