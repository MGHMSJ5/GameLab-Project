using UnityEngine;
using UnityEngine.AI;

public class BirdPerchState : BirdBaseState
{
    PerchDetect perchDetect;

    int curretPerch;
    int listLenght;
    public override void EnterState(BirdStateManager bird)
    {
        perchDetect = bird.birdPerches[bird.perchListNum].GetComponent<PerchDetect>();
        listLenght = bird.birdPerches.Count;
        curretPerch = bird.perchListNum; //is used to make sure that the bird won't go to the same perch when it flies to another place
        Vector3 zRotation = bird.transform.eulerAngles;
        zRotation.x = 0f;
        bird.transform.eulerAngles = zRotation;
    }

    public override void UpdateState(BirdStateManager bird)
    {
        if (curretPerch == bird.perchListNum)
        {
            bird.perchListNum = Random.Range(0, listLenght);
        }
        if (perchDetect.isPlayerNear)
        {
            bird.SwitchState(bird.FlyingState);
        }
        if (perchDetect.isItGround)
        {
            bird.SwitchState(bird.GroundState);
        }
    }

    public override void InRange(BirdStateManager bird)
    {
        
    }
}
