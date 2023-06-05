using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanFlower : MonoBehaviour
{
    public InfoNotification flowerInfoPage;
    private Animator questAnimator;
    // Start is called before the first frame update
    void Start()
    {
        questAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flowerInfoPage.hasSeen)
        {
            questAnimator.SetBool("Done", true);
            StartCoroutine(animDone());
        }
    }
    IEnumerator animDone()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
