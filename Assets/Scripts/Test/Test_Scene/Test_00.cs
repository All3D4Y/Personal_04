using UnityEngine;
using UnityEngine.InputSystem;

public class Test00 : TestBase
{
    public Transform generate_00;
    public Transform generate_01;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetNote<Note00_Ghost>(generate_00.position);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetNote<Note01_Slime>(generate_01.position);
    }
}
