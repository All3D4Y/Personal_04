using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_09 : TestBase
{
    public int id = 1;
    public DialogueManager manager;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        string path = "Assets/TestFiles/test.json";
        string json = File.ReadAllText(path);
        manager.LoadDialogueData(json);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        string id_str = $"intro_00{id}";
        manager.ShowDialogue(id_str);
    }
}
