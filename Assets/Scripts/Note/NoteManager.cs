using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteManager : MonoBehaviour 
{
    static NoteManager instance;
    AudioSource audioSource;
    MusicData currentMusicData;
    LaneManager laneManager;
    ComboManager comboManager;

    List<NoteData> noteDatas;

    int currentIndex = 0;
    float currentTime;
    float realTimeRatio;

    public static NoteManager Instance => instance;
    public LaneManager LaneManager => laneManager;
    public ComboManager ComboManager => comboManager;
    public ScoreManager ScoreManager => comboManager.ScoreManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        laneManager = new LaneManager(6);
        
        if (instance == null)
            instance = this;
    }

    void FixedUpdate()
    {
        if (audioSource != null)
        {
            currentTime = audioSource.time;

            if (audioSource.isPlaying)
            {
                if (currentIndex < noteDatas.Count && (noteDatas[currentIndex].barTime * realTimeRatio) + currentMusicData.syncModifier <= currentTime)
                {
                    SpawnNote(noteDatas[currentIndex]);
                    currentIndex++;
                }
            } 
        }
    }

    void OnDisable()
    {
        comboManager.CleanUp();
    }

    public void Initialize(MusicData musicData)
    {
        comboManager = new ComboManager();
        comboManager.Initialize(HitZone.Instance);

        currentMusicData = musicData;
        noteDatas = musicData.notes;
        audioSource.resource = musicData.audioClip;
        realTimeRatio = MathF.Round(240.0f / musicData.bpm, 6);
    }

    public void SpawnNote(NoteData noteData)
    {
        int noteSide = (int)noteData.side;
        NoteBase note = null;

        if (noteSide == 2)
        {
            Transform[] spawnPositions = GetTransforms(noteData.height);
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                if (noteData.height == NoteHeight.Up)
                {
                    note = Factory.Instance.GetNote<Note00_Ghost>(spawnPositions[i].position);
                }
                else if (noteData.height == NoteHeight.Down)
                {
                    note = Factory.Instance.GetNote<Note01_Slime>(spawnPositions[i].position);
                }
                laneManager[i * 3 + (int)noteData.height].AddNote(note);
            }
        }
        else
        {
            Transform spawnPosition = GetTransform(noteData.height, noteData.side);
            if (noteData.height == NoteHeight.Up)
            {
                note = Factory.Instance.GetNote<Note00_Ghost>(spawnPosition.position);
            }
            else if (noteData.height == NoteHeight.Down)
            {
                note = Factory.Instance.GetNote<Note01_Slime>(spawnPosition.position);
            }
            laneManager[3 * (int)noteData.side + (int)noteData.height].AddNote(note);
        }
    }

    public void MusicStart()
    {
        currentIndex = 0;
        currentTime = 0.0f;
        audioSource.Play();
    }

    #region 노트 출현 위치를 리턴하는 함수
    Transform GetTransform(NoteHeight height, NoteSide side)
    {
        Transform result = null;
        if ((int)side != 2)
            result = transform.GetChild((int)side).GetChild((int)height);
        return result;
    }

    Transform[] GetTransforms(NoteHeight height)
    {
        Transform[] results = new Transform[2];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = transform.GetChild(i).GetChild((int)height);
        }

        return results;
    }
    #endregion
}
