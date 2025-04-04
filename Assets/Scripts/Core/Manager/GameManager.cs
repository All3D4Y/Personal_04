using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    HitZone hitZone;
    NoteManager noteManager;
    UIManager uiManager;

    public HitZone HitZone => hitZone;
    public NoteManager NoteManager => noteManager;
    public UIManager UIManager => uiManager;
    protected override void OnInitialize()
    {   
        uiManager = GetComponent<UIManager>();
        hitZone = FindAnyObjectByType<HitZone>();
        noteManager = FindAnyObjectByType<NoteManager>();
    }
}
