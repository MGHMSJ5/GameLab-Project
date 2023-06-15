using UnityEngine;
using UnityEngine.AI;

public class AnimalNormalState : AnimalBaseState
{
    float waitTimer = 0.0f;
    float idleWait = 0.0f;

    public override void EnterState(AnimalStateManager animal)
    {
        animal.agent.speed = animal.walkingSpeed; //st speed to walking

        idleWait = Random.RandomRange(3f, 9f); //random number used for how long in idle
        animal.animator.SetBool("Running", false); //animation is no longer running
    }

    public override void UpdateState(AnimalStateManager animal)
    {
        DoMovement(animal); //idle or wandering

        if (Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.detectDistance && !animal.playerMovement.isCrouching || Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.crouchingDistance && animal.playerMovement.isCrouching)
            //if player is in certain distance and not crouching. Or, if player is (closer), and crouching in a certain distance
        {
            animal.SwitchState(animal.RunningAwayState); //run away
        }

        Debug.DrawLine(animal.agent.transform.position, animal.agent.destination, Color.red); //to see the point where the animal will go to

        if (!animal.agent.pathPending && animal.agent.remainingDistance <= animal.agent.stoppingDistance) //if the animal agent is not moving
        {
            animal.animator.SetBool("Idle", true); //idle animation is playing
            animal.animator.SetBool("Walking", false); //not walking anymore
        }
    }

    public override void InRange(AnimalStateManager animal)
    {

    }

    private void DoMovement(AnimalStateManager animal)
    {
        if (idleWait > 0) //if counter is higher than  0
        {
            idleWait -= Time.deltaTime; //decreae timer
            return;
        }
        else if (idleWait <= 0) //is counter is lower or equal than 0
        {
            if (waitTimer <= 0) //wandering timer
            {
                waitTimer = Random.RandomRange(2.0f, 9.0f); //set wandering timer to a number
                float distance = waitTimer * animal.agent.speed; //set a distance that depends on the speed and timer
                animal.agent.SetDestination(RandomNavSphere(animal.transform.position, distance, animal.floorMask)); //set a random destination, with the help of RandomNaSphere)
                animal.animator.SetBool("Idle", false); //not doing idle animation anymore
                animal.animator.SetBool("Walking", true);//walking animation is playing
                idleWait = Random.RandomRange(3f, 9f); //set a new random number for the idle
                return;
            }
            else
            {
                waitTimer -= Time.deltaTime; //decrease the wandering timer
            }
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask) //use to set a new randome position for the agent to go to
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        //help from chatGPT making it so that the animals (especially the animals in the water) don't go to the edge of the NavMesh
        Vector3 targetPosition = navHit.position;
        Vector3 directionToCenter = origin - targetPosition; //points from the generated position towards the center of the NavMesh
        directionToCenter.Normalize();

        float maxDistanceFromCenter = distance * 0.5f; //split the distance in half
        targetPosition = origin + directionToCenter * maxDistanceFromCenter; //move towards the center along the 'directionToCenter vector

        return targetPosition;
    }
}
