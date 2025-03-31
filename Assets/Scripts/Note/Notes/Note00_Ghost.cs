using UnityEngine;

public class Note00_Ghost : NoteBase
{
    float dissolveValue;

    SkinnedMeshRenderer mesh;

    protected override void OnEnable()
    {
        base.OnEnable();
        dissolveValue = 1.0f;
    }

    protected override void Awake()
    {
        base.Awake();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override void Attack()
    {
        base.Attack();
        Dissolve();
    }

    public override void Die()
    {
        base.Die();
        Dissolve();
    }

    void Dissolve()
    {
        dissolveValue -= Time.deltaTime;
        mesh.material.SetFloat("_Dissolve", dissolveValue);
    }
}
