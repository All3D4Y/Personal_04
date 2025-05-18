using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class TrackScrollController : MonoBehaviour
{
    [Header("References")]
    public ScrollRect scrollRect;               
    public RectTransform content;         
    public HorizontalLayoutGroup horizontalLayoutGroup;

    [Header("Snapping Settings")]
    public float snapSpeed = 10f;               
    public float velocityThreshold = 100f;      

    [Header("Preview Settings")]
    public float previewLoopDelay = 1f;

    AudioSource previewSource;
    int itemCount;
    int currentIndex = 0;
    int previewIndex = -1;
    float itemWidth = 0f;               
    float currentSnapSpeed;
    bool isSnapped;
    bool isBtnClicked;

    Coroutine snapCoroutine;
    Coroutine previewCoroutine;
    AsyncOperationHandle<MusicData>? currentHandle;

    void Awake()
    {
        previewSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // item의 폭, item 개수
        if (content.childCount != 0)
        {
            var item = content.GetChild(0) as RectTransform;
            itemWidth = item.rect.width;
            itemCount = content.childCount;
        }
    }

    void Update()
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
                    LoadPreviewForIndex(currentIndex);
                }
            }
            if (scrollRect.velocity.magnitude > velocityThreshold)
            {
                isSnapped = false;
                currentSnapSpeed = 0;
            } 
        }
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


    void LoadPreviewForIndex(int index)
    {
        if (previewIndex != index)
        {
            previewIndex = index;

            if (previewCoroutine != null)
                StopCoroutine(previewCoroutine);

            previewSource.Stop();

            if (currentHandle.HasValue)
            {
                Addressables.Release(currentHandle.Value);
                currentHandle = null;
            }

            var item = content.GetChild(currentIndex).GetComponent<MusicPanel>();
            if (item == null) return;

            var handle = Addressables.LoadAssetAsync<MusicData>(item.MusicMetaData.musicDataAddress);
            currentHandle = handle;

            handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    MusicData data = op.Result;
                    previewSource.clip = data.audioClip;
                    previewCoroutine = StartCoroutine(LoopPreview(data));
                }
            }; 
        }
    }

    IEnumerator LoopPreview(MusicData data)
    {
        while (true)
        {
            previewSource.Stop();
            previewSource.time = Mathf.Clamp(data.previewStartTime, 0f, data.audioClip.length);
            previewSource.Play();
            yield return new WaitForSecondsRealtime(data.previewLength);
            previewSource.Stop();
            yield return new WaitForSecondsRealtime(previewLoopDelay);
        }
    }

    IEnumerator MoveSmooth(int direction)
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
