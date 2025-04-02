using System;
using UnityEngine;

public class NoteBase : RecycleObject
{
    public float noteSpeed = 10.0f;

    protected Animator animator;
    protected NoteGuideLine guide;

    bool isHit = false;

    protected readonly int Attack_Hash = Animator.StringToHash("Attack");
    protected readonly int Die_Hash = Animator.StringToHash("Die");

    public bool IsHit 
    { 
        get => isHit; 
        set 
        { 
            if (isHit != value)
            {
                isHit = value;
                onHit?.Invoke();
            }
        } 
    }
    public NoteGuideLine Guide => guide;

    public Action onHit;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        guide = GetComponent<NoteGuideLine>();
    }

    protected virtual void Update()
    {
        if (transform.position.z <= 0.0f)
            Attack();
        else
            transform.Translate(noteSpeed * Time.deltaTime * Vector3.forward);
    }
    protected override void OnReset()
    {
        isHit = false;
        DisableTimer(20.0f);
    }

    public virtual void Attack()
    {
        animator.SetTrigger(Attack_Hash);
        IsHit = true;
    }

    public virtual void Die()
    {
        animator.SetTrigger(Die_Hash);
        DisableTimer(0.1f);
    }
}
