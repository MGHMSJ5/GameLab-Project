using UnityEngine;
using UnityEngine.AI;

public class BirdGroundState : BirdBaseState
{
    float waitTimer = 0.0f;
    float idleWait = 0.0f;
    public override void EnterState(BirdStateManager bird)
    {
        bird.birdAnimator.SetBool("Flying", false);
        bird.agent.enabled = true;
        bird.agent.speed = bird.walkingSpeed;

        bird.randomNumber = Random.Range(20, 40);

        idleWait = Random.RandomRange(3f, 9f);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        bird.timerToSwitchState = Time.deltaTime;

        DoIdle(bird);

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


        if (!bird.agent.pathPending && bird.agent.remainingDistance <= bird.agent.stoppingDistance)
        {
            bird.birdAnimator.SetBool("Idle", true);
            bird.birdAnimator.SetBool("Walking", false);
        }
    }

    public override void InRange(BirdStateManager bird)
    {

    }

    private void DoIdle(BirdStateManager bird)
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
                float distance = waitTimer * bird.agent.speed;
                bird.agent.SetDestination(RandomNavSphere(bird.transform.position, distance, bird.floorMask));
                bird.birdAnimator.SetBool("Idle", false);
                bird.birdAnimator.SetBool("Walking", true);
                idleWait = Random.RandomRange(3f, 9f);
                //curState = WanderingStates.Wandering;
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

        return navHit.position;
    }
}
