using UnityEngine;
using UnityEngine.AI;

public class AnimalNormalState : AnimalBaseState
{
    enum WanderingStates
    {
        Idle,
        Wandering
    }

    WanderingStates curState = WanderingStates.Idle;
    float waitTimer = 0.0f;

    Vector3 centerPoint; //ChatGPT grouping
    float maxDistanceFromCenter; //ChatGPT grouping
    float desiredDistanceBetweenAgents; //ChatGPT grouping

    public override void EnterState(AnimalStateManager animal)
    {
        animal.agent.speed = animal.walkingSpeed;
    }

    public override void UpdateState(AnimalStateManager animal)
    {
        switch (curState)
        {
            case WanderingStates.Idle:
                DoIdle(animal);
                break;
            case WanderingStates.Wandering:
                DoWander(animal);
                break;
            default:
                Debug.LogError("Should not be here, away with you! D:");
                break;
        }

        if (Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.detectDistance && !animal.playerMovement.isCrouching)
        {
            animal.SwitchState(animal.RunningAwayState);
        }

        Debug.DrawLine(animal.agent.transform.position, animal.agent.destination, Color.red);
    }

    public override void InRange(AnimalStateManager animal)
    {

    }

    private void DoIdle(AnimalStateManager animal)
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        animal.agent.SetDestination(RandomNavSphere(animal.transform.position, 10.0f, animal.floorMask));
        curState = WanderingStates.Wandering;
    }

    private void DoWander(AnimalStateManager animal)
    {
        if (animal.agent.pathStatus != NavMeshPathStatus.PathComplete)
            return;

        waitTimer = Random.Range(3.0f, 9.0f);
        curState = WanderingStates.Idle;
        
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

    //ChatGPT grouping
    public Vector3 GetRandomPositionWithinRange(Vector3 origin, Vector3 center, float range, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;
        randomDirection += center;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, range, layerMask);

        return navHit.position;
    }
    //ChatGPT grouping
    public void KeepAgentsWithinArea(AnimalStateManager[] agents)
    {
        foreach (var agent in agents)
        {
            Vector3 agentPosition = agent.transform.position;
            Vector3 directionToCenter = centerPoint - agentPosition;
            float distanceToCenter = directionToCenter.magnitude;

            if (distanceToCenter > maxDistanceFromCenter)
            {
                Vector3 targetPosition = centerPoint + directionToCenter.normalized * maxDistanceFromCenter;
                agent.agent.SetDestination(targetPosition);
            }

            foreach (var otherAgent in agents)
            {
                if (otherAgent == agent)
                    continue;

                Vector3 otherAgentPosition = otherAgent.transform.position;
                float distanceToOtherAgent = Vector3.Distance(agentPosition, otherAgentPosition);

                if (distanceToOtherAgent < desiredDistanceBetweenAgents)
                {
                    Vector3 separationDirection = (agentPosition - otherAgentPosition).normalized;
                    Vector3 targetPosition = agentPosition + separationDirection * desiredDistanceBetweenAgents;
                    agent.agent.SetDestination(targetPosition);
                }
            }
        }
    }
}
