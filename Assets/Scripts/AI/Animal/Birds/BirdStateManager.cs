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

    public float flyingSpeed;

    public NavMeshAgent agent;
    public Transform chaser;
    public float distanceWhenStartFlying = 5f;
    public PlayerMovement playerMovement;
    public Vector3 normDir = new Vector3();
    public int detectDistance = 6;

    public LayerMask floorMask = 0;

    public float walkingSpeed;

    public float timerToSwitchState = 0;
    public int randomNumber;

    // Start is called before the first frame update
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

    public void SwitchState(BirdBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
