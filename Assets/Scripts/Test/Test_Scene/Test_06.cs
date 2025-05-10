using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06 : TestBase
{
    public MusicPanelManager manager;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        manager.Initialize();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        
    }
}
