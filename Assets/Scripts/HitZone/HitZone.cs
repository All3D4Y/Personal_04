using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public Action<HitEnum> onHit;

    public void HitNote(int index)
    {
        LaneManager manager = GameManager.Instance.NoteManager.LaneManager;

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

    public HitEnum CheckTimin(float distance)
    {
        HitEnum hit;
        if (distance <= 0.25f)
            hit = HitEnum.Perfect;
        else if (distance <= 0.65f)
            hit = HitEnum.Good;
        else
            hit = HitEnum.Bad;
        return hit;
    }

    public void ToggleStart(int index)
    {

    }

    public void ToggleEnd(int index)
    {

    }
}
