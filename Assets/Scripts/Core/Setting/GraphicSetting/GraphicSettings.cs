using UnityEngine;

public enum GraphicQualities
{
    Low = 0,
    Middle,
    High
}

public class GraphicSettings : SettingBase
{
    public GraphicQualities qualities;
    public int fps;
    public float brightness;

    public override void Save()
    {
        PlayerPrefs.SetInt("qualities", (int)qualities);
        PlayerPrefs.SetInt("fps", fps);
        PlayerPrefs.SetFloat("brightness", brightness);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        qualities = (GraphicQualities)PlayerPrefs.GetInt("qualities", 1);
        fps = PlayerPrefs.GetInt("fps", 60);
        brightness = PlayerPrefs.GetFloat("brightness", 1.0f);
    }
    public override void Apply()
    {
    }
}
