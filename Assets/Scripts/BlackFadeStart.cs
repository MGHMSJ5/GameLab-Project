using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFadeStart : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(waitForFadeStart());
    }

    IEnumerator waitForFadeStart()
    {
        yield return new WaitForSeconds(0.5f);
        animator.enabled = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
