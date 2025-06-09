using UnityEngine;

public class GamePlaySettings : SettingBase
{
    public bool autoSkill;
    public override void Save()
    {
        int temp  = autoSkill ? 1 : 0;
        PlayerPrefs.SetInt("autoSkill", temp);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        int temp = PlayerPrefs.GetInt("autoSkill", 1);
        autoSkill = temp != 0;
    }
    public override void Apply()
    {
    }
}
