using System.Collections;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager>
{
    public void Clear()
    {
        StopAllCoroutines();
    }
    public void DelayMusicStart(float delayTime)
    {
        StartCoroutine(DelayMusicStartCoroutine(delayTime));
    }

    IEnumerator DelayMusicStartCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        NoteManager.Instance.MusicStart();
    }
}
