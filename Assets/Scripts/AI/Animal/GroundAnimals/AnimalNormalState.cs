using UnityEngine;
using UnityEngine.AI;

public class AnimalNormalState : AnimalBaseState
{
    float waitTimer = 0.0f;
    float idleWait = 0.0f;

    Vector3 centerPoint; //ChatGPT grouping
    float maxDistanceFromCenter; //ChatGPT grouping
    float desiredDistanceBetweenAgents; //ChatGPT grouping

    public override void EnterState(AnimalStateManager animal)
    {
        animal.agent.speed = animal.walkingSpeed;

        idleWait = Random.RandomRange(3f, 9f);
        animal.animator.SetBool("Running", false);
    }

    public override void UpdateState(AnimalStateManager animal)
    {
        DoIdle(animal);

        if (Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.detectDistance && !animal.playerMovement.isCrouching || Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.crouchingDistance && animal.playerMovement.isCrouching)
        {
            animal.SwitchState(animal.RunningAwayState);
        }

        Debug.DrawLine(animal.agent.transform.position, animal.agent.destination, Color.red);

        if (!animal.agent.pathPending && animal.agent.remainingDistance <= animal.agent.stoppingDistance)
        {
            animal.animator.SetBool("Idle", true);
            animal.animator.SetBool("Walking", false);
        }
    }

    public override void InRange(AnimalStateManager animal)
    {

    }

    private void DoIdle(AnimalStateManager animal)
    {
        if (idleWait > 0)
        {
            idleWait -= Time.deltaTime;
            return;
        }
        else if (idleWait <= 0)
        {
            if (waitTimer <= 0)
            {
                waitTimer = Random.RandomRange(2.0f, 9.0f);
                float distance = waitTimer * animal.agent.speed;
                animal.agent.SetDestination(RandomNavSphere(animal.transform.position, distance, animal.floorMask));
                animal.animator.SetBool("Idle", false);
                animal.animator.SetBool("Walking", true);
                idleWait = Random.RandomRange(3f, 9f);
                return;
            }
            else
            {
                waitTimer -= Time.deltaTime;
            }
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
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
