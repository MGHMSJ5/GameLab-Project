 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdStateManager : MonoBehaviour
{
    public BirdBaseState currentState;
    public BirdPerchState PerchState = new BirdPerchState();
    public BirdGroundState GroundState = new BirdGroundState();
    public BirdFlyingState FlyingState = new BirdFlyingState();

    public List<GameObject> birdPerches = new List<GameObject>();
    public int perchListNum = 0;

    [Header("Variables")]
    public float flyingSpeed;
    public float walkingSpeed;
    public int detectDistance = 10;
    public int crouchDetect = 6;

    public LayerMask floorMask = 0;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform chaser;
    public PlayerMovement playerMovement;
    public Animator birdAnimator;

    [Header("Used variables")]
    public Vector3 normDir = new Vector3();

    public float timerToSwitchState = 0;
    public int randomNumber; //random number which is used to switch to flying state when in ground/perch state

    void Start()
    {
        playerMovement = chaser.GetComponent<PlayerMovement>();

        currentState = FlyingState;
        currentState.EnterState(this);

        if (agent == null)
        {
            if (!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + " needs a navmesh agent");
            }
        }
    }

    void Update()
    {
        currentState.UpdateState(this);

        if (chaser == null)
        {
            return;
        }
        normDir = (chaser.position - transform.position).normalized;
    }

    public void SwitchState(BirdBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
