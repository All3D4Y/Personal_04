using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    [Header("Music")]
    public AudioClip audioClip;
    public int bpm;
    [Space(2)]
    [Header("Sync")]
    [Range(-10.0f, 10.0f)] public float syncModifier;
    [Space(2)]
    [Header("Note")]
    public NoteData[] notes;
}
