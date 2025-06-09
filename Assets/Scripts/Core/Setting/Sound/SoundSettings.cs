using UnityEngine;

public class SoundSettings : SettingBase
{
    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    public bool enablePreview = true;

    public override void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        int temp = enablePreview ? 1 : 0;
        PlayerPrefs.SetInt("enablePreview", temp);
        PlayerPrefs.Save();
    }
    public override void Load()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
        int temp = PlayerPrefs.GetInt("enablePreview", 1);
        enablePreview = temp != 0;
    }
    public override void Apply()
    {
    }
}
