using UnityEngine;

public class Playing : PlayState
{
    public Playing(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        NoteManager.Instance.onStageEnd += StageEnd;
        // 음악 시작
        CoroutineManager.Instance.DelayMusicStart(1.0f);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        NoteManager.Instance.onStageEnd -= StageEnd;
    }

    void StageEnd(bool isClear)
    {
        if (isClear)
        {
            // 클리어
            Manager.ChangeState<Result>();
        }
        else
        {
            // 실패
            Manager.ChangeState<Result>();
        }
    }
}
