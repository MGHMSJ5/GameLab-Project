using UnityEngine;
using UnityEngine.AI;

public class BirdFlyingState : BirdBaseState
{
    PerchDetect perchDetect; //script in perch which is used to look is perch is already used
    public override void EnterState(BirdStateManager bird)
    {
        bird.agent.enabled = false; //not on Nav Mesh anymore

        bird.birdAnimator.SetBool("Flying", true); //play flying animation
        bird.birdAnimator.SetBool("Walking", false);
        bird.birdAnimator.SetBool("Idle", false);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        perchDetect = bird.birdPerches[bird.perchListNum].GetComponent<PerchDetect>(); //get the scipt from the perch number
        if (perchDetect.perchIsUsed) //if the perch is already used↓
        {
            for (int i = 0; i < bird.birdPerches.Count; i++) //go through list of perches and find one that isn't used
            {
                PerchDetect perchDetectInList = bird.birdPerches[i].GetComponent<PerchDetect>();
                if (perchDetectInList.perchIsUsed == false)
                {
                    bird.perchListNum = i; //set new perch to go to
                    return;
                }
            }
        }
        Vector3 direction = (bird.birdPerches[bird.perchListNum].transform.position - bird.transform.position).normalized; //direction from bird to perch
        Vector3 velocity = direction * bird.flyingSpeed; //speed
        bird.transform.position += velocity * Time.deltaTime; //go to perch

        if (velocity.magnitude > 0.0f) //if bird is on/near perch
        {
            bird.transform.LookAt(bird.transform.position + velocity); //make the bird sit up
        }

        if (Vector3.Distance(bird.transform.position, bird.birdPerches[bird.perchListNum].transform.position) < 0.1) //if the bird has reached perch↓
        {
            bird.SwitchState(bird.PerchState); //switch to perch state
        }
    }

    public override void InRange(BirdStateManager bird)
    {
    }
}
