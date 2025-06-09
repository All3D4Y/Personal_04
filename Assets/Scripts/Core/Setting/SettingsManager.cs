using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    LanguageSettings languageSettings = new LanguageSettings();
    SoundSettings soundSettings = new SoundSettings();
    GamePlaySettings gamePlaySettings = new GamePlaySettings();
    GraphicSettings graphicSettings = new GraphicSettings();

    SettingBase[] allSettings;

    public LanguageSettings LanguageSettings => languageSettings;
    public SoundSettings SoundSettings => soundSettings;
    public GamePlaySettings GamePlaySettings => gamePlaySettings;
    public GraphicSettings GraphicSettings => graphicSettings;

    protected override void Awake()
    {
        base.Awake();
        allSettings = new SettingBase[]
        {
            languageSettings,
            soundSettings, 
            gamePlaySettings,
            graphicSettings
        };

        LoadAll();
        ApplyAll();
    }

    public void SaveAll()
    {
        foreach (var setting in allSettings)
        {
            setting.Save();
        }
    }

    public void LoadAll()
    {
        foreach (var setting in allSettings)
        {
            setting.Load();
        }
    }

    public void ApplyAll()
    {
        foreach (var setting in allSettings)
        {
            setting.Apply();
        }
    }
}
