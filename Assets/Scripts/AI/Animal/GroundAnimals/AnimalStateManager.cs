using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalStateManager : MonoBehaviour
{
    public AnimalBaseState currentState; //reference to the active state in a state machine
    public AnimalNormalState NormalState = new AnimalNormalState();
    public AnimalRunningAwayState RunningAwayState = new AnimalRunningAwayState();

    [Header("Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public int detectDistance = 15;
    public int crouchingDistance = 8;

    public LayerMask floorMask = 0;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform chaser;
    public PlayerMovement playerMovement;
    public Animator animator;

    [Header("Used varables")]
    public Vector3 normDir = new Vector3();
    void Start()
    {
        playerMovement = chaser.GetComponent<PlayerMovement>();

        currentState = NormalState; //starting state for the state machine
        currentState.EnterState(this); //"this" is a reference to the context (this EXACT Monobehaviour script in this object)

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
        currentState.UpdateState(this); //run this 
        if (chaser == null)
        {
            return;
        }
        normDir = (chaser.position - transform.position).normalized; //position of the chaser (player)
    }

    public void SwitchState(AnimalBaseState state) //switch from states
    {
        currentState = state;
        state.EnterState(this); //enter the new state
    }
}
