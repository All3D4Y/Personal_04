using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06 : TestBase
{
    public MusicData data;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        MusicManager.Instance.SetData(data);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        GameFlowManager.Instance.FlowStart();
    }
}
