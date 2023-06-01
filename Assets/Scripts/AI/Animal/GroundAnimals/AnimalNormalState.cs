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

        return navHit.position;
    }
}
