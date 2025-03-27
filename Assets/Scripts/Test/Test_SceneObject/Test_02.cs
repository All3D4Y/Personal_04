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

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            ResetTransform();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 오른손 아래
        ResetTransform();
        animator.SetFloat("AttackValue", 0.0f);
        animator.SetTrigger("RightAttack");
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 오른손 위
        ResetTransform();
        animator.SetFloat("AttackValue", 1.0f);
        animator.SetTrigger("RightAttack");
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 왼손 아래
        ResetTransform();
        animator.SetFloat("AttackValue", 0.0f);
        animator.SetTrigger("LeftAttack");
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        // 왼손 위
        ResetTransform();
        animator.SetFloat("AttackValue", 1.0f);
        animator.SetTrigger("LeftAttack");
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        // 양손
        ResetTransform();
        animator.SetTrigger("BothAttack");
    }

    void ResetTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }
}
