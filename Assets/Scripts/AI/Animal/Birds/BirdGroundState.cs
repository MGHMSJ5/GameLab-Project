using UnityEngine;
using UnityEngine.AI;

public class BirdGroundState : BirdBaseState
{
    enum WanderingStates
    {
        Idle,
        Wandering
    }

    WanderingStates curState = WanderingStates.Idle;
    float waitTimer = 0.0f;
    public override void EnterState(BirdStateManager bird)
    {
        bird.agent.enabled = true;
        bird.agent.speed = bird.walkingSpeed;

        bird.randomNumber = Random.Range(20, 40);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        bird.timerToSwitchState = Time.deltaTime;

        switch (curState)
        {
            case WanderingStates.Idle:
                DoIdle(bird);
                break;
            case WanderingStates.Wandering:
                DoWander(bird);
                break;
            default:
                Debug.LogError("Should not be here, away with you! D:");
                break;
        }

        if (Vector3.Distance(bird.chaser.position, bird.transform.position) < bird.detectDistance && !bird.playerMovement.isCrouching)
        {
            bird.SwitchState(bird.FlyingState);
        }

        if (bird.randomNumber < bird.timerToSwitchState)
        {
            bird.SwitchState(bird.FlyingState);
            bird.timerToSwitchState = 0;
            bird.randomNumber = Random.Range(20, 40);
        }
    }

    public override void InRange(BirdStateManager bird)
    {

    }

    private void DoIdle(BirdStateManager bird)
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        bird.agent.SetDestination(RandomNavSphere(bird.transform.position, 10.0f, bird.floorMask));
        curState = WanderingStates.Wandering;
    }

    private void DoWander(BirdStateManager bird)
    {
        if (bird.agent.pathStatus != NavMeshPathStatus.PathComplete)
            return;

        waitTimer = Random.Range(2.0f, 9.0f);
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
