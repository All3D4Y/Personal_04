using UnityEngine;

public class NoteBase : RecycleObject
{
    public float noteSpeed = 10.0f;

    protected override void OnReset()
    {
        DisableTimer(5.0f);
    }

    void Update()
    {
        transform.Translate(noteSpeed * Time.deltaTime * Vector3.forward);
    }
}
