using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(CanvasGroup))]
public class CanvasGroupBase : MonoBehaviour
{
    public float popSpeed = 1.0f;
    public Image targetImage;
    protected CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup => canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnVisible(bool usePop = false)
    {
        StopAllCoroutines();
        if (usePop) StartCoroutine(OpenPop());
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void OnTransparent(bool usePop = false)
    {
        StopAllCoroutines();
        if (usePop) StartCoroutine(ClosePop());
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    IEnumerator OpenPop()
    {
        targetImage.transform.localScale = Vector3.zero;

        while (targetImage.transform.localScale.sqrMagnitude >= 2.99f)
        {
            targetImage.transform.localScale += Time.deltaTime * popSpeed * Vector3.one;
            yield return null;
        }

        targetImage.transform.localScale = Vector3.one;
    }

    IEnumerator ClosePop()
    {
        targetImage.transform.localScale = Vector3.one;

        while (targetImage.transform.localScale.sqrMagnitude <= 0.01f)
        {
            targetImage.transform.localScale -= Time.deltaTime * popSpeed * Vector3.one;
            yield return null;
        }

        targetImage.transform.localScale = Vector3.zero;
    }
}
