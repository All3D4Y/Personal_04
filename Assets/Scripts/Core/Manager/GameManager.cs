using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    HitZone hitZone;
    NoteManager noteManager;

    public HitZone HitZone => hitZone;
    public NoteManager NoteManager => noteManager;
    protected override void OnInitialize()
    {   
        hitZone = FindAnyObjectByType<HitZone>();
        noteManager = FindAnyObjectByType<NoteManager>();
    }
}
