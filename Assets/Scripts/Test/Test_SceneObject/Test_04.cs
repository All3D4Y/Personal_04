using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04 : TestBase
{
    public MusicData musicData;
    public TouchPanel right;
    public TouchPanel left;

    void Start()
    {
        GameManager.Instance.NoteManager.Initialize(musicData);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        GameManager.Instance.NoteManager.MusicStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Debug.Log("right up");
            right.onSwipe?.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //Debug.Log("right down");
            right.onSwipe?.Invoke(1);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("left up");
            left.onSwipe?.Invoke(3);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("left down");
            left.onSwipe?.Invoke(4);
        }
    }
}
