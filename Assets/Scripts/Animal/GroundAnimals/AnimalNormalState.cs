using UnityEngine;

public class AnimalNormalState : AnimalBaseState
{
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
