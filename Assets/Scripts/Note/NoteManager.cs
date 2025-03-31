using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteManager : MonoBehaviour 
{
    MusicData currentMusicData;

    NoteData[] noteDatas;

    Transform[] laneTransforms;

    AudioSource audioSource;

    List<NoteBase> activeNotes;

    int currentIndex = 0;

    float currentTime;

    public List<NoteBase> ActiveNotes;

    void Awake()
    {
        laneTransforms = new Transform[transform.childCount];
        for (int i = 0; i < laneTransforms.Length; i++)
        {
            laneTransforms[i] = transform.GetChild(i);
        }
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        currentTime = audioSource.time;

        while (currentIndex < noteDatas.Length && noteDatas[currentIndex].time <= currentTime)
        {
            SpawnNote(noteDatas[currentIndex]);
            currentIndex++;
        }
    }

    public void Initialize(MusicData musicData)
    {
        currentMusicData = musicData;
        noteDatas = musicData.notes;
        activeNotes = new List<NoteBase>();
        audioSource.resource = musicData.music;
    }

    public void SpawnNote(NoteData noteData)
    {
        Transform spawnPos = laneTransforms[(int)noteData.lane];
        NoteBase note = null;

        if (noteData.type == NoteType.Note00_Ghost)
            note = Factory.Instance.GetNote<Note00_Ghost>(spawnPos.position);
        else if (noteData.type == NoteType.Note01_Slime)
            note = Factory.Instance.GetNote<Note01_Slime>(spawnPos.position);

        if (note != null)
        {
            activeNotes.Add(note);
            note.onHit = null;
            note.onHit += () => activeNotes.Remove(note);
        }
    }

    public void MusicStart()
    {
        currentIndex = 0;
        currentTime = 0.0f;
        audioSource.Play();
    }
}
