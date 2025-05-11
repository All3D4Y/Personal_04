
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TrackScrollController : MonoBehaviour
{
    [Header("Scroll View Components")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public RectTransform viewport;

    [Header("Snap Settings")]
    public float snapDelay = 0.3f;
    public float snapSpeed = 10f;

    float scrollVelocityThreshold = 10f;

    AudioSource previewSource;
    MusicPanel currentSelectedItem;

    Coroutine snapCoroutine;
    Coroutine previewLoopCoroutine;
    AsyncOperationHandle<MusicData>? currentPreviewHandle;

    void Awake()
    {
        previewSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (scrollRect.velocity.magnitude < scrollVelocityThreshold)
        {
            if (snapCoroutine == null)
                snapCoroutine = StartCoroutine(SnapToClosestTrackCoroutine());
        }
        else
        {
            if (snapCoroutine != null)
            {
                StopCoroutine(snapCoroutine);
                snapCoroutine = null;
            }
        }
    }

    IEnumerator SnapToClosestTrackCoroutine()
    {
        yield return new WaitForSeconds(snapDelay);

        Transform closest = null;
        float closestDistance = float.MaxValue;
        Vector3 centerInWorld = viewport.TransformPoint(viewport.rect.center);

        foreach (Transform child in content)
        {
            float distance = Mathf.Abs(child.position.x - centerInWorld.x);
            if (distance < closestDistance)
            {
                closest = child;
                closestDistance = distance;
            }
        }

        if (closest != null)
        {
            Vector2 diff = (Vector2)(viewport.position - closest.position);
            Vector2 targetPos = content.localPosition + (Vector3)diff;

            yield return StartCoroutine(SmoothMove(content.localPosition, targetPos, 0.25f));

            MusicPanel newSelected = closest.GetComponent<MusicPanel>();
            if (newSelected != null && newSelected != currentSelectedItem)
            {
                currentSelectedItem = newSelected;
                PlayPreview(currentSelectedItem.MusicMetaData);
            }
        }

        snapCoroutine = null;
    }

    IEnumerator SmoothMove(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            content.localPosition = Vector2.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        content.localPosition = to;
    }

    void PlayPreview(MusicMetaData meta)
    {
        // 재생 중이던 프리뷰 정리
        previewSource.Stop();

        if (previewLoopCoroutine != null)
        {
            StopCoroutine(previewLoopCoroutine);
            previewLoopCoroutine = null;
        }

        // 핸들 릴리즈
        if (currentPreviewHandle.HasValue)
        {
            Addressables.Release(currentPreviewHandle.Value);
            currentPreviewHandle = null;
        }

        // 데이터 로드
        var handle = Addressables.LoadAssetAsync<MusicData>(meta.musicDataAddress);
        currentPreviewHandle = handle;

        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                MusicData musicData = op.Result;
                previewSource.clip = musicData.audioClip;
                previewLoopCoroutine = StartCoroutine(LoopPreview(musicData));
            }
        };
    }
    IEnumerator LoopPreview(MusicData musicData)
    {
        while (true)
        {
            previewSource.Stop();
            previewSource.time = Mathf.Clamp(musicData.previewStartTime, 0f, musicData.audioClip.length);
            previewSource.Play();

            yield return new WaitForSecondsRealtime(musicData.previewLength);

            previewSource.Stop();
            yield return new WaitForSecondsRealtime(1.0f); // 루프 사이 잠깐 멈춤
        }
    }
}
