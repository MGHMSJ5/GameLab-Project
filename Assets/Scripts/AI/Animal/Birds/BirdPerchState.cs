using UnityEngine;

public class BirdPerchState : BirdBaseState
{
    PerchDetect perchDetect;
    int currentPerch;
    public override void EnterState(BirdStateManager bird)
    {
        bird.timerToSwitchState = 0; //reset timer
        bird.randomNumber = Random.Range(10, 20); //set number for random timer

        perchDetect = bird.birdPerches[bird.perchListNum].GetComponent<PerchDetect>(); //get script from perch where bird is on
        currentPerch = bird.perchListNum; //is used to make sure that the bird won't go to the same perch when it flies to another place
        Vector3 zRotation = bird.transform.eulerAngles;
        zRotation.x = 0f;
        bird.transform.eulerAngles = zRotation; //reset x rotation

        if (!perchDetect.isItGround) //is the perch is in a tree
        {
            perchDetect.perchIsUsed = true; //perch is occupied
        }
        //change to the right animatioin
        bird.birdAnimator.SetBool("Flying", false);
        bird.birdAnimator.SetBool("Walking", false);
        bird.birdAnimator.SetBool("Idle", true);
    }

    public override void UpdateState(BirdStateManager bird)
    {
        bird.timerToSwitchState += Time.deltaTime; //set timer

        if (currentPerch == bird.perchListNum) //make sure that the random perch isn't the same as the original one
        {
            bird.perchListNum = Random.Range(0, bird.birdPerches.Count); //choose a random perch
        }
        if (perchDetect.isPlayerNear) //from the perch scipt, if player enters collider
        {
            perchDetect.perchIsUsed = false; //perch is not used anymore, and flies to different perch
            bird.SwitchState(bird.FlyingState);
        }
        if (perchDetect.isItGround) //is the perch was on the groun
        {
            bird.SwitchState(bird.GroundState); //activate ground state
        }
        if (bird.randomNumber < bird.timerToSwitchState) //if it's time to switch to another state↓
        {
            bird.SwitchState(bird.FlyingState);
        }
    }
    public override void InRange(BirdStateManager bird)
    {
    }
}
