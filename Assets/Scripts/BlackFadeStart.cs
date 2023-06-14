using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFadeStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForFadeStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitForFadeStart()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
