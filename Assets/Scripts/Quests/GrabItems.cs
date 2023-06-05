using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabItems : MonoBehaviour
{
    public List<GameObject> itemsToPickUp = new List<GameObject>();
    public TextMeshProUGUI questCounter;
    private int maxItems;
    private int currentItemsCounter;
    private string counterInString;

    private Animator questAnimator;
    // Start is called before the first frame update
    void Start()
    {
        questAnimator = GetComponent<Animator>();
        maxItems = itemsToPickUp.Count;
    }

    // Update is called once per frame
    void Update()
    {
        counterInString = currentItemsCounter + "/" + maxItems;
        questCounter.text = counterInString;
        for (int i = 0; i < itemsToPickUp.Count; i++)
        {
            if (itemsToPickUp[i] == null)
            {
                currentItemsCounter += 1;
                itemsToPickUp.Remove(itemsToPickUp[i]);
            }
        }

        if (currentItemsCounter == maxItems)
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
