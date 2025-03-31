using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03 : TestBase
{
    public MusicData music;

    public NoteManager noteManager;

    void Start()
    {
        noteManager.Initialize(music);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        noteManager.MusicStart();
    }
}
