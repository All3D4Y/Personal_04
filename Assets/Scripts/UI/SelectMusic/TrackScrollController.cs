using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class TrackScrollController : SnapScrollController
{
    [Header("Preview Settings")]
    public float previewLoopDelay = 1f;

    int previewIndex = -1;
    Coroutine previewCoroutine;
    AsyncOperationHandle<MusicData>? currentHandle;

    public int BPM { get; private set; }

    protected override void ExecuteWhenSnapped()
    {
        LoadPreviewForIndex(currentIndex);
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
                    BPM = data.bpm;
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
}
