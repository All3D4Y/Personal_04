using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04 : TestBase
{
    public ScoreUI score;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        score.GetScore(100);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        score.GetScore(1000);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        score.GetScore(10000);
    }
}
