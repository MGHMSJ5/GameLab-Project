using UnityEngine;
using UnityEngine.AI;

public class BirdFlyingState : BirdBaseState
{
    public override void EnterState(BirdStateManager bird)
    {
        bird.agent.enabled = false;
    }

    public override void UpdateState(BirdStateManager bird)
    {
        Vector3 direction = bird.birdPerches[bird.perchListNum].transform.position - bird.transform.position;
        Vector3 moveDirection = direction.normalized;
        Vector3 velocity = moveDirection * bird.flyingSpeed;
        bird.transform.position += velocity * Time.deltaTime;

        if (Vector3.Distance(bird.transform.position, bird.birdPerches[bird.perchListNum].transform.position) < 0.1)
        {
            bird.SwitchState(bird.PerchState);

        }
    }

    public override void InRange(BirdStateManager bird)
    {

    }
}
