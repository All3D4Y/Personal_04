using System;
using System.Collections.Generic;
using UnityEngine;

public class NotePoolManager : MonoBehaviour
{
    NotePool[] pools;

    Dictionary<Type, NotePool> notePools;

    void Awake()
    {
        pools = new NotePool[transform.childCount];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = transform.GetChild(i).GetComponent<NotePool>();
        }
    }

    public void Initialize()
    {
        notePools = new Dictionary<Type, NotePool>();

        foreach (var pool in pools)
        {
            if (pool != null)
            {
                pool.Initialize();

                Type noteType = pool.GetNoteType();
                if (noteType != null)
                {
                    notePools.Add(pool.GetNoteType(), pool);
                }
            }
        }
    }

    public NotePool GetPool<T>() where T : NoteBase
    {
        Type type = typeof(T);

        if (notePools.TryGetValue(type, out NotePool pool))
        {
            return pool;
        }
        Debug.LogWarning("타입에 맞는 풀이 없음!");
        return null;
    }
}
