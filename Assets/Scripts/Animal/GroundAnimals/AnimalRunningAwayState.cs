using UnityEngine;

public class AnimalRunningAwayState : AnimalBaseState
{
    int randomInt;
    bool canChangeRandomDirection = true;
    int leftOrRight = 0;
    float timeToWait;
    float currentTime;
    public override void EnterState(AnimalStateManager animal)
    {

    }

    public override void UpdateState(AnimalStateManager animal)
    {
        animal.normDir = Quaternion.AngleAxis(randomInt, Vector3.up) * animal.normDir;
        MoveToPos(animal.transform.position - (animal.normDir * animal.distWhenStartRunning), animal);

        if (Vector3.Distance(animal.chaser.position, animal.transform.position) > animal.detectDistance || animal.PlayerMovement.isCrouching)
        {
            animal.SwitchState(animal.NormalState);
        }

        if (canChangeRandomDirection)
        {
            SetRandomDirection();
        }
    }

    public override void InRange(AnimalStateManager animal)
    {

    }

    void MoveToPos(Vector3 pos, AnimalStateManager animal)
    {
        animal.agent.SetDestination(pos);
        animal.agent.isStopped = false; 
    }
    
    void SetRandomDirection()
    {
        timeToWait = Random.Range(2, 5);
        leftOrRight = Random.Range(0, 1);
        currentTime += Time.deltaTime;
        if (timeToWait <= currentTime)
        {
            if (leftOrRight == 0)
            {
                randomInt = Random.Range(315, 360);
            }
            else
            {
                randomInt = Random.Range(0, 45);
            }
            canChangeRandomDirection = false;
            currentTime = 0;
        }
    }

}
