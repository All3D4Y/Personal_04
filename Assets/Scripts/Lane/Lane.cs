using System.Collections.Generic;
using UnityEngine;

public class Lane
{
    int laneIndex;
    List<NoteBase> onLaneNotes;

    public List<NoteBase> OnLaneNotes => onLaneNotes;

    public Lane(int index)
    {
        laneIndex = index;
        onLaneNotes = new List<NoteBase>();
    }

    public void AddNote(NoteBase note)
    {
        onLaneNotes.Add(note);
        note.onHit += () => RemoveNote(note);
    }
    void RemoveNote(NoteBase note)
    {
        onLaneNotes.Remove(note);
        note.onHit -= () => RemoveNote(note);
    }
}
