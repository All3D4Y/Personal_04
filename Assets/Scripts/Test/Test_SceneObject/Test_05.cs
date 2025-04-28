using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05 : TestBase
{
    public JudgeUI test;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        test.TestJudge(JudgeEnum.Perfect);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        test.TestJudge(JudgeEnum.Good);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        test.TestJudge(JudgeEnum.Bad);
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        test.TestJudge(JudgeEnum.Miss);
    }
}
