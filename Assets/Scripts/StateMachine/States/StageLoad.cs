using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : PlayState
{
    public StageLoad(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        FadeManager.Instance.SceneLoadWithFade(1);
    }

    public override void Update()
    {
        if (FadeManager.Instance.LoadingProgress >= 0.9f)
        {
            Manager.ChangeState<Setup>();
        }
    }

    public override void Exit()
    {

    }
}
