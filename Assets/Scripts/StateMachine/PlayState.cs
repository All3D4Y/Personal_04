using UnityEngine;

public abstract class PlayState
{
    protected GameFlowManager Manager {  get; set; }

    protected PlayState(GameFlowManager manager)
    {
        Manager = manager;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
