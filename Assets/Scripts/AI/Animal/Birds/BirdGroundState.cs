using UnityEngine;
using UnityEngine.AI;

public class BirdGroundState : BirdBaseState
{
    float waitTimer = 0.0f;
    float idleWait = 0.0f;
    public override void EnterState(BirdStateManager bird)
    {
        bird.timerToSwitchState = 0; //reset timer
        bird.birdAnimator.SetBool("Flying", false);
        bird.agent.enabled = true;
        bird.agent.speed = bird.walkingSpeed;

        bird.randomNumber = Random.Range(20, 40); //random number which causes the state 
        idleWait = Random.RandomRange(3f, 9f);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        bird.timerToSwitchState += Time.deltaTime;

        DoMovement(bird);

        if (Vector3.Distance(bird.chaser.position, bird.transform.position) < bird.detectDistance && !bird.playerMovement.isCrouching || bird.playerMovement.isCrouching && Vector3.Distance(bird.chaser.position, bird.transform.position) > bird.crouchDetect)
        {
            bird.perchListNum = Random.Range(0, bird.birdPerches.Count); //random perch
            bird.SwitchState(bird.FlyingState);
        }

        if (!bird.agent.pathPending && bird.agent.remainingDistance <= bird.agent.stoppingDistance)
        {
            bird.birdAnimator.SetBool("Idle", true);
            bird.birdAnimator.SetBool("Walking", false);
        }

        if (bird.randomNumber < bird.timerToSwitchState) //this is not in the GrountAnimals state
        {
            bird.perchListNum = Random.Range(0, bird.birdPerches.Count); //random perch
            bird.SwitchState(bird.FlyingState);
            bird.randomNumber = Random.Range(20, 40);
        }
    }

    public override void InRange(BirdStateManager bird)
    {
    }

    private void DoMovement(BirdStateManager bird) //this one works the same as in AnimalNormalState
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
