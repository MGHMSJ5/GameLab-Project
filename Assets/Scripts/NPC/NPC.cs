using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public bool talkingCompletelyDone = false;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    public GameObject NPCParent;
    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
        originalPos = transform.localPosition;
        agent = gameObject.GetComponent<NavMeshAgent>();
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

        if (talkingCompletelyDone)
        {
            nPCDialogueTrigger.enabled = false;
            WalkAway();
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

    private void WalkAway()
    {
        if (nPCDialogueTrigger.isFarAway)
        {
            walkScript.enabled = false;
            NextWayPoint();
        }
        rotateToPplayer = false;
        npcAnimator.SetBool("Leaving", true);
        agent.enabled = true;
        nPCDialogueTrigger.enabled = false;
        UpdateDestination();
        if (Vector3.Distance(transform.position, target) < 1)
        {
            waypointIndex += 1;
        }
        if (waypointIndex == waypoints.Length && nPCDialogueTrigger.isApproachable)
        {
            NPCParent.SetActive(false);
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    private void NextWayPoint()
    {
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
