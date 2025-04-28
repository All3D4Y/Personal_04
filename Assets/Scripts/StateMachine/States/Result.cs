using UnityEngine;

public class Result : PlayState
{
    public Result(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        // 결과 UI 초기화
        ResultUIInitialize();
        // 게임 CleanUp
        CleanUp();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        // 결과 UI CleanUp
        ResultUICleanUp();
    }

    void CleanUp()
    {
        // 인풋 클린업
        InputManager.Instance.CleanUp();
        // UI 클린업
        HUDManager.Instance.CleanUp();
        // 노트매니저 클린업
        NoteManager.Instance.CleanUp();
    }

    void ResultUIInitialize()
    {
        int score = HUDManager.Instance.GameHUDViewModel.Score;
    }

    void ResultUICleanUp()
    {

    }
}
