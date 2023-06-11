using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAcive : MonoBehaviour
{
    public GameObject journalInfo;
    public GameObject animalOrPlantIcon;
    public GameObject highlight;
    public GameObject questionMarks;
    private InfoNotification infoNotification;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        infoNotification = journalInfo.GetComponent<InfoNotification>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (journalInfo.activeSelf)
        {
            button.enabled = true;
            animalOrPlantIcon.SetActive(true);
            highlight.SetActive(true);
            questionMarks.SetActive(false);
        }

        if (!infoNotification.hasSeen)
        {
            highlight.SetActive(false);
        }
    }
}
