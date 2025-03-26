using System;
using UnityEngine;

public class NotePool : ObjectPool<NoteBase>
{
    public Type GetNoteType()
    {
        if (prefab != null)
            return prefab.GetComponent<NoteBase>().GetType();
        Debug.LogWarning("노트 타입 확인 실패!");
        return null;
    }
}
