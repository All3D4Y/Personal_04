using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06 : TestBase
{
    public MusicPanelManager manager;
    public TrackScrollController controller;

    protected override void Awake()
    {
        base.Awake();
        manager.Initialize();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        controller.MoveTrack(1);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        controller.MoveTrack(-1);
    }
}
