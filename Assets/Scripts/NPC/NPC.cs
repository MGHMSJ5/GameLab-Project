using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2.5f;
    public bool rotateToPplayer = false;

    public Quaternion originalRot;
    public Animator npcAnimator;
    public AIWalk walkScript;
    public NPCDialogueTrigger nPCDialogueTrigger;

    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (nPCDialogueTrigger.isApproachable)
        {
            ApproachalbleNPC(rotateToPplayer);
        }

        if (nPCDialogueTrigger.isFarAway)
        {
            FarAwayNPC(rotateToPplayer);
        }

    }

    private void ApproachalbleNPC(bool rotate)
    {
        if (rotate)
        {
            npcAnimator.SetBool("IsTalking", true);
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            transform.localPosition = originalPos;
        }
        if (!rotate)
        {
            npcAnimator.SetBool("IsTalking", false);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, rotationSpeed * Time.deltaTime);
        }
    }
    private void FarAwayNPC(bool rotate)
    {
        if (rotate)
        {
            npcAnimator.SetBool("IsTalking", true);
            walkScript.stopWalking = true;
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (!rotate)
        {
            npcAnimator.SetBool("IsTalking", false);
            walkScript.stopWalking = false;
        }
    }
}
