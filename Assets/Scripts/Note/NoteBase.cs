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
        transform.Translate(noteSpeed * Time.deltaTime * Vector3.forward);
        if (transform.position.z < 0.8f)
            Attack();
    }
    protected override void OnReset()
    {
        isHit = false;
        DisableTimer(20.0f);
    }

    public virtual void Attack()
    {
        animator.SetTrigger(Attack_Hash);
    }

    public virtual void Die()
    {
        animator.SetTrigger(Die_Hash);
        DisableTimer(0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("HitZone"))
        {
            float distance = collision.transform.position.z - transform.position.z;
            distance = Mathf.Abs(distance);

            if (distance <= 0.2)
            {
                // perfect
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Perfect);
                Debug.Log("Perfect");
            }
            else if (distance <= 0.5)
            {
                // good
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Good);
                Debug.Log("Good");
            }
            else
            {
                // bad
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Bad);
                Debug.Log("Bad");
            }

            IsHit = true;
            guide.EraseLine();
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitZone"))
        {
            float distance = other.transform.position.z - transform.position.z;
            distance = Mathf.Abs(distance);

            if (distance <= 0.2)
            {
                // perfect
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Perfect);
            }
            else if (distance <= 0.5)
            {
                // good
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Good);
            }
            else
            {
                // bad
                GameManager.Instance.HitZone.onHit?.Invoke(HitEnum.Bad);
            }

            IsHit = true;
            guide.EraseLine();
            Die();
        }
    }
}
