using System;
using UnityEngine;

public enum NoteType
{
    Note00_Ghost = 0,
    Note01_Slime
}

[System.Serializable]
public struct NoteData
{
    public NoteType type;
    public float time;                  // 노트가 떨어지는 시간(초 단위)
    public LaneIndex lane;              // 노트의 레인
}

[CreateAssetMenu(fileName = "NewMusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    [Range(-10.0f, 10.0f)] public float syncModifier;
    public AudioClip music;
    public NoteData[] notes;
}
