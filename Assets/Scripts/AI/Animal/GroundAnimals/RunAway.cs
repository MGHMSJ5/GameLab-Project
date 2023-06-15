using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform chaser;
    [SerializeField] private float displacementDist = 5f;

    private int randomInt;
    private bool canChangeRandomDirection = true;
    private int leftOrRight = 0; //if 0, then will go left, if 1, will go right
    public PlayerMovement playerMovement;
    void Start()
    {
        if (agent == null)
        {
            if (!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + " needs a navmesh aget");
            }
        }
    }

    void Update()
    {
        Vector3 normDir = (chaser.position - transform.position).normalized;

        if (Vector3.Distance(chaser.position, transform.position) < 6 && !playerMovement.isCrouching)
        {
            normDir = Quaternion.AngleAxis(randomInt, Vector3.up) * normDir;
            MoveToPos(transform.position - (normDir * displacementDist));
        }

        if (canChangeRandomDirection)
        {
            StartCoroutine(RandomDirection());
        }

    }
    private void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }

    private IEnumerator RandomDirection()
    {
        canChangeRandomDirection = false;
        yield return new WaitForSeconds(Random.Range(3, 6));
        leftOrRight = Random.Range(0, 2);
        if (leftOrRight == 0)
        {
            randomInt = Random.Range(315, 360);
        }
        else
        {
            randomInt = Random.Range(0, 45);
        }
        canChangeRandomDirection = true;

    }
}
