using UnityEngine;
using UnityEngine.AI;

public class BirdFlyingState : BirdBaseState
{
    PerchDetect perchDetect;
    public override void EnterState(BirdStateManager bird)
    {
        bird.agent.enabled = false;

        bird.birdAnimator.SetBool("Flying", true);
        bird.birdAnimator.SetBool("Walking", false);
        bird.birdAnimator.SetBool("Idle", false);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        perchDetect = bird.birdPerches[bird.perchListNum].GetComponent<PerchDetect>();
        if (perchDetect.perchIsUsed)
        {
            for (int i = 0; i < bird.birdPerches.Count; i++)
            {
                PerchDetect perchDetectInList = bird.birdPerches[i].GetComponent<PerchDetect>();
                if (perchDetectInList.perchIsUsed == false)
                {
                    bird.perchListNum = i;
                    return;
                }
            }
        }
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
