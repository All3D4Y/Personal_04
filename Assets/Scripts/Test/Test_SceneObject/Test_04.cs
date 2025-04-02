using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04 : TestBase
{
    public MusicData musicData;

    void Start()
    {
        GameManager.Instance.NoteManager.Initialize(musicData);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        GameManager.Instance.NoteManager.MusicStart();
    }
}
