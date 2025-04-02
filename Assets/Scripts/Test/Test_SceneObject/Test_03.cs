using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03 : TestBase
{
    public MusicData music;

    public NoteManager noteManager;

    public HitZone hitZone;

    void Start()
    {
        noteManager.Initialize(music);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        noteManager.MusicStart();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        hitZone.HitNote(0);
        hitZone.HitNote(3);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        hitZone.HitNote(1);
        hitZone.HitNote(4);
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        hitZone.HitNote(0);
    }
}
