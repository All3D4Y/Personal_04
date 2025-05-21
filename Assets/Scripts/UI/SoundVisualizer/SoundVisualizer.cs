using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVisualizer : MonoBehaviour
{
    public int sampleSize = 64;
    public float heightMultiplier = 10.0f;
    public float scaleMultiplier = 1.0f;
    [Range(0f, 1f)] public float resetSpeedModifier = 0.5f;
    public bool mirror = false;


    AudioSource audioSource;
    UILineRenderer lineRenderer;
    TrackScrollController trackScrollController;
    float[] samples;
    float width;
    float tickTime;
    float elapsedTime = 0f;
    List<Vector2> waveformPoints = new List<Vector2>();

    void Awake()
    {
        audioSource = transform.parent.GetComponentInChildren<AudioSource>();
        trackScrollController = audioSource.GetComponent<TrackScrollController>();
        lineRenderer = GetComponent<UILineRenderer>();
        samples = new float[sampleSize];
        width = Screen.width;
        transform.localScale *= scaleMultiplier;
    }

    void Update()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            if (tickTime == 0.0f)
                tickTime = 60.0f / trackScrollController.BPM;

            if (elapsedTime < tickTime)
            {
                elapsedTime += Time.deltaTime;
                lineRenderer.SetPoints(waveformPoints);
            }
            else
            {
                StopAllCoroutines();
                DrawSpectrum();
                elapsedTime = 0f;
                StartCoroutine(ResetWave());
            }
        }
        else
            tickTime = 0.0f;
    }

    void DrawSpectrum()
    {
        audioSource.GetOutputData(samples, 0);

        waveformPoints.Clear();

        for (int i = 0; i < sampleSize; i++)
        {
            float x = (i / (float)(sampleSize - 1)) * width - width * 0.5f;
            float y = Mathf.Abs(samples[i] * heightMultiplier);

            if (i < 1 || i > sampleSize - 2)
                y = 0f;

            waveformPoints.Add(new Vector2(x, y));

            if (mirror)
                waveformPoints.Add(new Vector2(x, -y));
        }

        lineRenderer.SetPoints(waveformPoints);
    }

    IEnumerator ResetWave()
    {
        while (true)
        {
            for (int i = 0; i < waveformPoints.Count; i++)
            {
                float y = waveformPoints[i].y;
                y = Mathf.Lerp(y, 0, resetSpeedModifier);
                
                waveformPoints[i] = new Vector2(waveformPoints[i].x, y);
            }
            yield return null;
        }
    }
}
