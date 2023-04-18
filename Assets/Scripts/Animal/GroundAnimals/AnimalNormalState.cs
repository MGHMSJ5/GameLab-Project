using UnityEngine;
using UnityEngine.AI;

public class AnimalNormalState : AnimalBaseState
{
    enum AIWandering
    {
        Idle, 
        Wandering
    }

    private float waitTimer = 0f;

    public override void EnterState(AnimalStateManager animal)
    {

    }

    public override void UpdateState(AnimalStateManager animal)
    {
        if (Vector3.Distance(animal.chaser.position, animal.transform.position) < animal.detectDistance && !animal.PlayerMovement.isCrouching)
        {
            animal.SwitchState(animal.RunningAwayState);
        }
    }

    public override void InRange(AnimalStateManager animal)
    {

    }
}
