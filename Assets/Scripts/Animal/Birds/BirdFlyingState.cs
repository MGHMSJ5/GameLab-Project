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
        Vector3 direction = (bird.birdPerches[bird.perchListNum].transform.position - bird.transform.position).normalized;
        Vector3 velocity = direction * bird.flyingSpeed;
        bird.transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > 0.0f)
        {
            bird.transform.LookAt(bird.transform.position + velocity);
        }

        if (Vector3.Distance(bird.transform.position, bird.birdPerches[bird.perchListNum].transform.position) < 0.1)
        {
            bird.SwitchState(bird.PerchState);

        }
    }

    public override void InRange(BirdStateManager bird)
    {

    }
}
