using System;
using UnityEngine;

public enum Language
{
    English = 0,
    Korean
}

public class LanguageSettings : SettingBase
{
    public Language currentLanguage;

    public override void Save()
    {
        PlayerPrefs.SetInt("language", (int)currentLanguage);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        currentLanguage = (Language)PlayerPrefs.GetInt("language", 0);
    }

    public override void Apply()
    {   
    }
}
