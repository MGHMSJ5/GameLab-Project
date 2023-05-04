using UnityEngine;

public abstract class BirdBaseState
{
    public abstract void EnterState(BirdStateManager bird);

    public abstract void UpdateState(BirdStateManager bird);

    public abstract void InRange(BirdStateManager bird);
}
