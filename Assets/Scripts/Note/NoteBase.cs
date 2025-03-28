using UnityEngine;

public class NoteBase : RecycleObject
{
    public float noteSpeed = 10.0f;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        transform.Translate(noteSpeed * Time.deltaTime * Vector3.forward);
    }
    protected override void OnReset()
    {
        DisableTimer(5.0f);
    }
}
