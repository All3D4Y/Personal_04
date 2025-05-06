using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : PlayState
{
    public StageLoad(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        FadeManager.Instance.onLoadComplete += Manager.ChangeState<Setup>;
        FadeManager.Instance.SceneLoadWithFade(1);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        FadeManager.Instance.onLoadComplete -= Manager.ChangeState<Setup>;
    }
}
