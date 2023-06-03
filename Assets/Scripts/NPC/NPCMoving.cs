using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoving : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2.5f;
    public bool rotateToPplayer = false;

    public Animator npcAnimator;
    public AIWalk walkScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateToPplayer)
        {
            npcAnimator.SetBool("IsTalking", true);
            walkScript.stopWalking = true;
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (!rotateToPplayer)
        {
            npcAnimator.SetBool("IsTalking", false);
            walkScript.stopWalking = false;
        }
    }
}
