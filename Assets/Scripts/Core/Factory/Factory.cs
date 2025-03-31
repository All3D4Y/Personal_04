using UnityEngine;

public class Factory : Singleton<Factory>
{
    NotePoolManager notePoolManager;

    protected override void OnInitialize()
    {
        notePoolManager = GetComponentInChildren<NotePoolManager>();
        if (notePoolManager != null) notePoolManager.Initialize();
    }

    #region 풀에서 오브젝트 가져오는 함수들
    public T GetNote<T>(Vector3? position = null) where T : NoteBase
    {
        T note = notePoolManager.GetPool<T>().GetObject(position, new Vector3(0, 180, 0)).GetComponent<T>();

        return note;
    }
    #endregion
}
