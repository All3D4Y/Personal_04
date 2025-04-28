using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    int perfectCount;
    int goodCount;
    int badCount;
    int missCount;

    static HitZone instance;

    public Action<JudgeEnum> onHit;

    public static HitZone Instance => instance;
    public int PerfectCount => perfectCount;
    public int GoodCount => goodCount;
    public int BadCount => badCount;
    public int MissCount => missCount;

    void Awake()
    {
        if (instance == null)
            instance = this;

        perfectCount = 0;
        goodCount = 0;
        badCount = 0;
        missCount = 0;
    }

    public void HitNote(int index)
    {
        LaneManager manager = NoteManager.Instance.LaneManager;

        List<NoteBase> list = manager[index].OnLaneNotes;
        if (list.Count > 0)
        {
            NoteBase note = list[0];
            if (note != null && note.transform.position.z < 2)
            {
                float distance = Mathf.Abs(note.transform.position.z - 1);
                onHit?.Invoke(CheckTimin(distance));
                note.IsHit = true;
                Debug.Log(CheckTimin(distance));
            }
        }
    }

    public JudgeEnum CheckTimin(float distance)
    {
        JudgeEnum hit;
        if (distance <= 0.25f)
        {
            hit = JudgeEnum.Perfect;
            perfectCount++;
        }
        else if (distance <= 0.65f)
        {
            hit = JudgeEnum.Good;
            goodCount++;
        }
        else
        {
            hit = JudgeEnum.Bad;
            badCount++;
        }
        return hit;
    }

    public void ToggleStart(int index)
    {

    }

    public void ToggleEnd(int index)
    {

    }

    public void CountMiss()
    {
        missCount++;
    }
}
