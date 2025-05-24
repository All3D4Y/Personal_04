using System;
using System.Collections;
using UnityEngine;

public class CampaignCharacterControl : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    Animator animator;

    //Action<Transform> action = null;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        CampaignInputControl.Instance.onTouch += MoveCharacter;
    }

    void OnDisable()
    {
        CampaignInputControl.Instance.onTouch -= MoveCharacter;
    }

    void MoveCharacter(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(target));
    }

    IEnumerator MoveCoroutine(Transform target)
    {
        Vector3 targetPos = target.position;
        Debug.Log($"{targetPos}");
        Debug.Log($"{Vector3.SqrMagnitude(targetPos - transform.position)}");
        if (Vector3.SqrMagnitude(targetPos - transform.position) > 1.0f)
            animator.SetBool("IsRun", true);

        while (Vector3.SqrMagnitude(targetPos - transform.position) <= 1.0f)
        {
            Debug.Log($"{Vector3.SqrMagnitude(targetPos - transform.position)}");
            transform.position += moveSpeed * Time.deltaTime * (targetPos - transform.position).normalized;
            yield return null;
        }
        animator.SetBool("IsRun", false);
        yield return new WaitForSeconds(1.0f);

        LoadStage(target.GetComponent<StagePrefab>().stageData);
    }

    void LoadStage(StageData data)
    {
        Debug.Log("Stage Load Start");
    }
}
