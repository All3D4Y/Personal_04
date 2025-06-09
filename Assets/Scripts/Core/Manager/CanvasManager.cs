using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    TitleUI titleUI;
    MainMenuUI mainMenuUI;
    FreePlayUI freePlayUI;
    SettingMenuUI settingPanelUI;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        settingPanelUI = FindAnyObjectByType<SettingMenuUI>();

    }
    protected override void OnInitialize()
    {
        // 메인씬이면
        titleUI = FindAnyObjectByType<TitleUI>();
        mainMenuUI = FindAnyObjectByType<MainMenuUI>();
        freePlayUI = FindAnyObjectByType<FreePlayUI>();
    }
}
