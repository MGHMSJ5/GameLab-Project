using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalStateManager : MonoBehaviour
{
    public AnimalBaseState currentState; //reference to the active state in a state machine
    public AnimalNormalState NormalState = new AnimalNormalState();
    public AnimalRunningAwayState RunningAwayState = new AnimalRunningAwayState();

    public NavMeshAgent agent;
    public Transform chaser;
    public float distWhenStartRunning = 5f;
    public PlayerMovement playerMovement;
    public Vector3 normDir = new Vector3();
    public int detectDistance = 6;

    public LayerMask floorMask = 0;

    public float walkingSpeed;
    public float runningSpeed;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = chaser.GetComponent<PlayerMovement>();

        currentState = NormalState; //starting state for the state machine

        currentState.EnterState(this); //"this" is a reference to the context (this EXACT Monobehaviour script)

        if (agent == null)
        {
            if (!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + " needs a navmesh aget");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        if (chaser == null)
        {
            return;
        }
        normDir = (chaser.position - transform.position).normalized;
    }

    public void SwitchState(AnimalBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
