using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    List<NoteBase> notes;

    public Action<HitEnum> onHit;

    void Awake()
    {
        notes = new List<NoteBase>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
            notes.Add(other.GetComponent<NoteBase>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            NoteBase temp = other.GetComponent<NoteBase>();
            if (!temp.IsHit)
            {
                notes.Remove(temp);
                onHit?.Invoke(HitEnum.Miss);
                Debug.Log("Miss");
            }
        }
    }

    void HitNote(int laneIndex)
    {
        switch (laneIndex)
        {
            case 0:
                // right up
                break;
            case 1:
                // right down
                break;
            case 2:
                // right toggle
                break;
            case 3:
                // left up
                break;
            case 4:
                // left down
                break;
            case 5:
                // left toggle
                break;
            case 6:
                // both up
                break;
            case 7:
                // both down
                break;
            case 8:
                // both toggle
                break;
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
            hit = HitEnum.Miss;
        return hit;
    }
}
