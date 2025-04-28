using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoad : PlayState
{
    public StageLoad(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        //SceneManager.LoadScene(0); 게임 씬 로드
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }
}
