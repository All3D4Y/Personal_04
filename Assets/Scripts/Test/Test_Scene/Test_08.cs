using UnityEngine;
using UnityEngine.InputSystem;

public class Test_08 : TestBase
{
    public StagePrefab[] stagePrefabs;

    GameProgressData data;

    protected override void Awake()
    {
        base.Awake();
        data = new GameProgressData();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        data.lastClearedStage = 0;
        data.clearedStages.Add(0);

        SaveManager.SaveData(data);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        data.lastClearedStage = 1;
        data.clearedStages.Add(1);

        SaveManager.SaveData(data);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        data.lastClearedStage = 2;
        data.clearedStages.Add(2);

        SaveManager.SaveData(data);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        SaveManager.DeleteData();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        foreach (var stage in stagePrefabs)
        {
            stage.Initialize();
        }
    }
}
