using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrollController : MonoBehaviour
{
    [Header("References")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public HorizontalLayoutGroup horizontalLayoutGroup;

    [Header("Snapping Settings")]
    public float snapSpeed = 100f;
    public float velocityThreshold = 300f;

    protected AudioSource previewSource;
    protected int itemCount;
    protected int currentIndex = 0;
    protected float itemWidth = 0f;
    protected float currentSnapSpeed;
    protected bool isSnapped;
    protected bool isBtnClicked;
    protected Coroutine snapCoroutine;

    protected virtual void Awake()
    {
        previewSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        // item의 폭, item 개수
        if (content.childCount != 0)
        {
            var item = content.GetChild(0) as RectTransform;
            itemWidth = item.rect.width;
            itemCount = content.childCount;
        }
    }

    protected virtual void Update()
    {
        if (!isBtnClicked)
        {
            currentIndex = Mathf.RoundToInt(0 - content.localPosition.x / (itemWidth + horizontalLayoutGroup.spacing));

            if (scrollRect.velocity.magnitude < velocityThreshold && !isSnapped)
            {
                scrollRect.velocity = Vector3.zero;
                currentSnapSpeed += snapSpeed * Time.deltaTime;
                content.localPosition = new Vector3(
                    Mathf.MoveTowards(content.localPosition.x, 0 - currentIndex * (itemWidth + horizontalLayoutGroup.spacing), currentSnapSpeed),
                    content.localPosition.y,
                    content.localPosition.z);
                if (content.localPosition.x == 0 - currentIndex * (itemWidth + horizontalLayoutGroup.spacing))
                {
                    isSnapped = true;
                    ExecuteWhenSnapped();
                }
            }
            if (scrollRect.velocity.magnitude > velocityThreshold)
            {
                isSnapped = false;
                currentSnapSpeed = 0;
            }
        }
    }

    protected virtual void ExecuteWhenSnapped()
    {
    }

    public void MoveTrack(int direction)
    {
        int newIndex = currentIndex + direction;
        if (newIndex > -1 || newIndex < itemCount)
        {
            isBtnClicked = true;
            if (snapCoroutine != null)
                StopCoroutine(snapCoroutine);
            snapCoroutine = StartCoroutine(MoveSmooth(direction));
            currentIndex = newIndex;
        }
    }

    protected IEnumerator MoveSmooth(int direction)
    {
        float distance = 0;
        while (distance <= itemWidth + horizontalLayoutGroup.spacing)
        {
            distance += snapSpeed * Time.deltaTime;
            content.localPosition = new Vector3(direction, content.localPosition.y, content.localPosition.z);
            yield return null;
        }
        isBtnClicked = false;
    }
}
