using UnityEngine;
using UnityEngine.XR;

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

    // dissolve shading
    void Dissolve()
    {
        dissolveValue -= Time.deltaTime;
        mesh.material.SetFloat("_Dissolve", dissolveValue);
    }
}
