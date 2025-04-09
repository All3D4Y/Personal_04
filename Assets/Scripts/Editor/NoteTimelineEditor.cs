using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoteTimelineEditor : EditorWindow
{
    [MenuItem("Tools/Note Timeline Editor")]
    public static void ShowWindow()
    {
        GetWindow<NoteTimelineEditor>("Note Timeline Editor");
    }

    private readonly string[] LaneNames =
    {
        "RightUP", "RightDown", "RightToggle",
        "LeftUp", "LeftDown", "LeftToggle"
    };

    public MusicData musicData;

    AudioSource previewSource;
    Vector2 scrollPos; 
    Vector2 dragStartPos;

    float zoom = 1.0f;
    float pixelsPerBar = 100.0f;
    float currentPlayTime = 0f;
    float autoScrollMargin = 100f;
    double audioStartTime;
    bool isPlaying = false;
    bool hasChanges = false;
    bool isSeeking = false;
    int slotsPerBar = 4;

    int laneCounts => LaneNames.Length;
    

    void OnGUI()
    {
        // 제목
        GUILayout.Label("Note Timeline Editor", EditorStyles.boldLabel);

        // 음악 데이터
        musicData = (MusicData)EditorGUILayout.ObjectField("Music Data", musicData, typeof(MusicData), false);

        // 음악 데이터가 있으면 내용 표시
        if (musicData == null) return;

        if (!previewSource)
        {
            GameObject audioGO = new GameObject("Audio Preview");
            audioGO.hideFlags = HideFlags.HideAndDontSave;
            previewSource = audioGO.AddComponent<AudioSource>();
        }

        // 마디 나누기 (박자에 따라서, 기본 : 4/4)
        slotsPerBar = EditorGUILayout.IntSlider("Slots Per Bar", slotsPerBar, 1, 16);

        // 싱크 조절
        GUILayout.BeginHorizontal();
        GUILayout.Label("Sync Modifier", GUILayout.Width(100));
        EditorGUI.BeginChangeCheck();
        musicData.syncModifier = GUILayout.HorizontalSlider(musicData.syncModifier, -10.0f, 10.0f);
        musicData.syncModifier = EditorGUILayout.FloatField(musicData.syncModifier, GUILayout.Width(60));
        if (EditorGUI.EndChangeCheck())
            hasChanges = true;
        GUILayout.Label("sec");

        // 재정렬 버튼
        if (GUILayout.Button("Sort/Save", GUILayout.Width(80)))
            SortAndSave();
        GUILayout.EndHorizontal();


        #region 재생/정지 버튼, 볼륨조절, 타임라인 확대, 리스트 재정렬 버튼
        GUILayout.BeginHorizontal();

        // 재생/일시정지/정지 버튼
        if (GUILayout.Button(isPlaying ? "∥Pause" : "▶Play", GUILayout.Width(60)))
            TogglePlayPause();
        if (GUILayout.Button("⏹Stop", GUILayout.Width(60)))
            StopPlayback();

        // 볼륨 조절 슬라이더
        GUILayout.Label("Volume", GUILayout.Width(50));
        previewSource.volume = GUILayout.HorizontalSlider(previewSource.volume, 0f, 1f);

        // 마디 너비 조절용 줌
        GUILayout.Label("Zoom", GUILayout.Width(35));
        zoom = GUILayout.HorizontalSlider(zoom, 0.5f, 4.0f);
        
        GUILayout.EndHorizontal();
        #endregion

        // 타임라인
        float gridWidth = position.width;
        float laneWidth = gridWidth / laneCounts;
        //---------------------------------------------------------------BeginScrollView---------------------------------------------------------------
        scrollPos = GUILayout.BeginScrollView(scrollPos);

        // 타임라인 높이 계산 (노트 또는 오디오 기준으로 총 마디 수 추정)
        float songLength = musicData.audioClip.length;
        int totalBars = Mathf.CeilToInt(songLength / GetBarTimeLength()) + 1;

        // 전체 높이 계산
        float gridHeight = totalBars * pixelsPerBar * zoom;

        GUILayoutUtility.GetRect(gridWidth, gridHeight);

        Rect gridRect = new Rect(0, 0, gridWidth, gridHeight);
        EditorGUI.DrawRect(gridRect, new Color(0.1f, 0.1f, 0.1f));

        #region 노트 작성용 그리드 (마디, 레인, 노트, 현재 재생 위치 표시)
        Handles.BeginGUI();

        // 노트 그리기
        foreach (var note in musicData.notes)
        {
            int lane = GetLaneIndex(note);
            float x = lane * laneWidth;
            float y = note.barTime * pixelsPerBar * zoom;
            Rect noteRect = new Rect(x, y, laneWidth, (pixelsPerBar * zoom / slotsPerBar));
            Color noteColor = new Color(0, 1, 0, 0.5f);
            EditorGUI.DrawRect(noteRect, noteColor);
        }

        // 가로 그리드 (박자 기반)
        for (int i = 0; i < totalBars * slotsPerBar; i++)
        {
            float y = i * (pixelsPerBar * zoom / slotsPerBar);
            Handles.color = i % slotsPerBar == 0 ? Color.white : new Color(1, 1, 1, 0.15f);
            Handles.DrawLine(new Vector3(0, y), new Vector3(gridWidth, y));
        }

        // 세로 그리드 (레인 기반, 가로 사이 간격 자동 계산)
        for (int i = 0; i <= laneCounts; i++)
        {
            float x = i * laneWidth;
            Handles.color = new Color(1, 1, 1, 0.1f);
            Handles.DrawLine(new Vector3(x, 0), new Vector3(x, gridHeight));

            // 레인 이름 표시 (중앙에서 살짝 왼쪽)
            if (i < laneCounts)
            {
                Vector2 labelPos = new Vector2(x + laneWidth * 0.5f - 40, 4 + scrollPos.y);
                GUI.Label(new Rect(labelPos.x, labelPos.y, laneWidth, 20), LaneNames[i], EditorStyles.boldLabel);
            }
        }

        // 현재 재생위치 표시
        if (previewSource.clip != null)
        {
            if (isPlaying)
                currentPlayTime = (float)(EditorApplication.timeSinceStartup - audioStartTime);

            float barTimeNow = currentPlayTime / GetBarTimeLength();
            float y = barTimeNow * pixelsPerBar * zoom;

            Handles.color = Color.red;
            Handles.DrawLine(new Vector2(0, y), new Vector2(gridWidth, y));
        }

        Handles.EndGUI();
        #endregion

        // 재생 중일 때 빨간 선 따라 자동 스크롤
        if (isPlaying && previewSource.clip != null)
        {
            float currentTime = previewSource.time;
            float barTimeNow = currentTime / GetBarTimeLength();
            float redLineY = barTimeNow * pixelsPerBar * zoom;

            float viewportHeight = position.height;

            // 중간 지점 기준 스크롤
            float targetScrollY = redLineY - viewportHeight / 2f + autoScrollMargin;

            // 클램프 (음수 스크롤 방지)
            targetScrollY = Mathf.Max(0f, targetScrollY);

            scrollPos.y = targetScrollY;
        }

        // 클릭 시 노트 생성, 재 클릭 시 삭제
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Vector2 mousePos = Event.current.mousePosition;

            int lane = Mathf.FloorToInt(mousePos.x / laneWidth);
            float rawBarTime = mousePos.y / (pixelsPerBar * zoom);

            int barIndex = Mathf.FloorToInt(rawBarTime);
            float subBeat = (rawBarTime - barIndex) * slotsPerBar;
            int subBeatIndex = Mathf.FloorToInt(subBeat);
            float snappedBarTime = barIndex + (subBeatIndex / (float)slotsPerBar);

            if (lane >= 0 && lane < laneCounts)
            {
                NoteHeight height = (NoteHeight)(lane % 3);
                NoteSide side = (NoteSide)(lane / 3);

                int noteIndex = musicData.notes.FindIndex(n =>
                    Mathf.Abs(n.barTime - snappedBarTime) < 0.001f &&
                    n.height == height &&
                    n.side == side
                );

                if (noteIndex >= 0)
                {
                    musicData.notes.RemoveAt(noteIndex);
                    hasChanges = true;
                }
                else
                {
                    musicData.notes.Add(new NoteData
                    {
                        barTime = snappedBarTime,
                        height = height,
                        side = side
                    });
                    hasChanges = true;
                }

                Event.current.Use();
            }
        }

        GUILayout.EndScrollView();
        //---------------------------------------------------------------EndScrollView---------------------------------------------------------------

        // 마우스를 클릭한 후 드래그로 재생 위치 이동
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 2 && gridRect.Contains(e.mousePosition))
        {
            isSeeking = true;
            dragStartPos = e.mousePosition;
            e.Use();
        }
        else if (e.type == EventType.MouseDrag && isSeeking)
        {
            float newScrollY = scrollPos.y - (e.mousePosition.y - dragStartPos.y);
            newScrollY = Mathf.Clamp(newScrollY, 0f, gridHeight - position.height);

            scrollPos.y = newScrollY;
            dragStartPos = e.mousePosition;

            // 마우스 위치를 바탕으로 재생 시간 계산
            float barTime = (scrollPos.y + position.height / 2f) / (pixelsPerBar * zoom);
            currentPlayTime = barTime * GetBarTimeLength();

            e.Use();
        }
        else if (e.type == EventType.MouseUp && isSeeking && e.button == 2)
        {
            isSeeking = false;
            e.Use();
        }

        // 재생 시 시간 표시
        if (isPlaying && previewSource.clip != null)
        {
            float time = (float)(EditorApplication.timeSinceStartup - audioStartTime);
            EditorGUILayout.LabelField("Current Time:", currentPlayTime.ToString("F3"));
        }

        Repaint();
    }

    void OnDestroy()
    {
        if (musicData != null && hasChanges)
        {
            bool confirm = EditorUtility.DisplayDialog(
                "저장하시겠습니까?",
                "변경된 노트 데이터가 있습니다. 저장하시겠습니까?",
                "예", "아니오");

            if (confirm)
            {
                SortAndSave();
                Debug.Log("노트 데이터 저장 완료.");
            }
        }
    }

    /// <summary>
    /// 음악 재생/일시정지
    /// </summary>
    void TogglePlayPause()
    {
        if (!previewSource.clip && musicData.audioClip != null)
            previewSource.clip = musicData.audioClip;

        if (!previewSource.clip) return;

        if (!isPlaying)
        {
            float syncTime = Mathf.Max(0f, musicData.syncModifier);
            previewSource.time = Mathf.Max(0f, currentPlayTime + syncTime);
            previewSource.Play();

            audioStartTime = EditorApplication.timeSinceStartup - currentPlayTime;
            isPlaying = true;
        }
        else
        {
            currentPlayTime = Mathf.Max(0f, previewSource.time - musicData.syncModifier);
            previewSource.Pause();
            isPlaying = false;
        }
    }

    /// <summary>
    /// 음악 정지
    /// </summary>
    void StopPlayback()
    {
        if (!previewSource || !previewSource.clip) return;

        previewSource.Stop();
        previewSource.time = 0f;
        currentPlayTime = 0f;
        isPlaying = false;
        scrollPos.y = 0f;
    }

    // 노트 재정렬 (사용 편의 상 저장이라고 표시)
    void SortAndSave()
    {
        if (musicData != null && musicData.notes.Count > 0)
        {
            musicData.notes.Sort((a, b) => a.barTime.CompareTo(b.barTime));
            EditorUtility.SetDirty(musicData); 
            AssetDatabase.SaveAssets();
        }
        hasChanges = false;
    }

    // 노트의 레인 번호
    int GetLaneIndex(NoteData note)
    {
        return ((int)note.side) * 3 + (int)note.height;
    }

    // BPM에 따른 음악의 1마디 시간
    float GetBarTimeLength() => 240f / musicData.bpm;
}