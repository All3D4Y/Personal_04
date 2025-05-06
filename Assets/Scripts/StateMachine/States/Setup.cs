using UnityEngine;

public class Setup : PlayState
{
    public Setup(GameFlowManager manager) : base(manager) { }

    public override void Enter()
    {
        Initialize();
        Manager.ChangeState<Playing>();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }

    void Initialize()
    {
        // 노트매니저(음악) 초기화
        NoteManager.Instance.Initialize(MusicManager.Instance.MusicData);
        // 인풋초기화
        InputManager.Instance.Initialize();
        // UI 초기화
        HUDManager.Instance.Initialize();
    }
}
