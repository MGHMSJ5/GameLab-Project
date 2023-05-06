using UnityEngine;
using UnityEngine.AI;

public class BirdPerchState : BirdBaseState
{
    PerchDetect perchDetect;

    int currentPerch;
    int listLenght;
    public override void EnterState(BirdStateManager bird)
    {
        bird.randomNumber = Random.Range(10, 20);

        perchDetect = bird.birdPerches[bird.perchListNum].GetComponent<PerchDetect>();
        listLenght = bird.birdPerches.Count;
        currentPerch = bird.perchListNum; //is used to make sure that the bird won't go to the same perch when it flies to another place
        Vector3 zRotation = bird.transform.eulerAngles;
        zRotation.x = 0f;
        bird.transform.eulerAngles = zRotation;

        if (!perchDetect.isItGround)
        {
            perchDetect.perchIsUsed = true;
        }
    }

    public override void UpdateState(BirdStateManager bird)
    {
        bird.timerToSwitchState += Time.deltaTime;

        if (currentPerch == bird.perchListNum)
        {
            bird.perchListNum = Random.Range(0, listLenght);
        }
        if (perchDetect.isPlayerNear)
        {
            perchDetect.perchIsUsed = false;
            bird.SwitchState(bird.FlyingState);
        }
        if (perchDetect.isItGround)
        {
            bird.SwitchState(bird.GroundState);
        }

        if (bird.randomNumber < bird.timerToSwitchState)
        {
            bird.SwitchState(bird.FlyingState);
            bird.timerToSwitchState = 0;
        }
    }

    public override void InRange(BirdStateManager bird)
    {
        
    }
}
