using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanFlower : MonoBehaviour
{
    public GameObject flowerInfoPage; //flower information block that needs to be scanned
    private Animator questAnimator; //animator of quest
    public bool startDialogue = false;
    void Start()
    {
        questAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (flowerInfoPage.activeSelf) //if player has scanned the flower and it's active (but it doesn't have to be active in the hierarchy)
        {
            questAnimator.SetBool("Done", true);
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
