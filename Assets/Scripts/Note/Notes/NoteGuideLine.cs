using UnityEngine;

public class NoteGuideLine : MonoBehaviour
{
    public Vector3 preset;

    LineRenderer lineRenderer;

    MeshRenderer noteLineRenderer;

    NoteBase note;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        noteLineRenderer = transform.GetChild(2).GetComponent<MeshRenderer>();
        note = GetComponent<NoteBase>();
    }

    void Start()
    {
        InitializeLine();
    }

    void OnEnable()
    {
        InitializeLine();
    }

    void Update()
    {
        if (!note.IsHit)
            DrawLine();
        if (transform.position.z < 0.9f)
            EraseLine();
    }

    void InitializeLine()
    {
        noteLineRenderer.enabled = true;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position + preset);
        lineRenderer.SetPosition(1, noteLineRenderer.transform.position);
    }

    public void EraseLine()
    {
        lineRenderer.enabled = false;
        noteLineRenderer.enabled = false;
    }
}
