using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [Header("Rotate to player")]
    public Transform player;
    public float rotationSpeed = 2.5f;
    public bool rotateToPplayer = false;
    public Quaternion originalRot;
    private Vector3 originalPos;

    [Header("References")]
    public Animator npcAnimator;
    public AIWalk walkScript;
    public NPCDialogueTrigger nPCDialogueTrigger;

    [Header("Walking away")]
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
        if (nPCDialogueTrigger.isApproachable) //if it's the old lady
        {
            ApproachalbleNPC(rotateToPplayer);
        }

        if (nPCDialogueTrigger.isFarAway) //if the npc is the jogger
        {
            FarAwayNPC(rotateToPplayer); //run this, and also give the value of the rotateToPlayer bool. If true, then the npc must rotate, if false, then they must rotate back to their original position
        }

        if (talkingCompletelyDone) //bool that needs to be true when the talking is completely done. Results will be different depending on if the npc is the jogger or the old lady
        {
            nPCDialogueTrigger.enabled = false;
            WalkAway();
        }
    }

    private void ApproachalbleNPC(bool rotate)
    {
        if (rotate) //if the player is talking to the old lady
        {
            npcAnimator.SetBool("IsTalking", true);
            Vector3 direction = player.position - transform.position; //represents the direction from npc to player
            direction.y = 0; //ignore y axis
            Quaternion toRotation = Quaternion.LookRotation(direction); //set the new direction where the npc has to look at
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //use Lerp to smoothly change rotation
            transform.localPosition = originalPos;
        }
        if (!rotate) //if the player isn't talking to the old lady anymore
        {
            npcAnimator.SetBool("IsTalking", false);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, rotationSpeed * Time.deltaTime); //rotate back to original position
        }
    }
    private void FarAwayNPC(bool rotate)
    {
        if (rotate)
        {
            npcAnimator.SetBool("IsTalking", true);
            walkScript.stopWalking = true; //the only difference to the old lady. Jogger stops running
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (!rotate)
        {
            npcAnimator.SetBool("IsTalking", false);
            walkScript.stopWalking = false; //jogger starts running again
        }
    }

    private void WalkAway()
    {
        if (nPCDialogueTrigger.isFarAway) //the jogger needs to continue running. So the current walking sript needs to be disabled. And the jogger will go run on new waypoints
        {
            walkScript.enabled = false;
            NextWayPoint();
        }
        rotateToPplayer = false; //stop rotating to player
        npcAnimator.SetBool("Leaving", true);
        agent.enabled = true; //activate the agent (in the old ladies case)
        nPCDialogueTrigger.enabled = false; //deactivate the dialogue (since you can't talk to them again)
        UpdateDestination(); //set next destination
        if (Vector3.Distance(transform.position, target) < 1) //if they are nearing the target waypoint
        {
            waypointIndex += 1; //set next waypoint
        }
        if (waypointIndex == waypoints.Length && nPCDialogueTrigger.isApproachable) //is the old lady is at the waypoint
        {
            NPCParent.SetActive(false); //old lady is gone
        }
    }

    private void UpdateDestination() //set the next target waypoint as destination of the agent
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        transform.LookAt(target);
    }

    private void NextWayPoint()
    {
        if (waypointIndex == waypoints.Length) //reset the waypointIndex if the last waypoint was reached
        {
            waypointIndex = 0;
        }
    }
}
