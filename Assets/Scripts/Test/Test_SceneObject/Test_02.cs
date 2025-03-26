using UnityEngine;
using UnityEngine.InputSystem;

public class Test_02 : TestBase
{
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 오른손 아래
        animator.SetFloat("AttackValue", 0.0f);
        animator.SetTrigger("Attack");
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 오른손 위
        animator.SetFloat("AttackValue", 1.0f);
        animator.SetTrigger("Attack");
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 왼손 아래
        animator.SetFloat("AttackValue", 0.0f);
        animator.SetTrigger("Attack");
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        // 왼손 위
        animator.SetFloat("AttackValue", 1.0f);
        animator.SetTrigger("Attack");
    }
}
