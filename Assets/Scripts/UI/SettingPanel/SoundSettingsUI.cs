using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    Slider masterVolumeSlider;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;
    Toggle enablePreview;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        masterVolumeSlider = child.GetChild(0).GetComponentInChildren<Slider>();
        musicVolumeSlider = child.GetChild(1).GetComponentInChildren<Slider>();
        sfxVolumeSlider = child.GetChild(2).GetComponentInChildren<Slider>();
        enablePreview = child.GetChild(3).GetChild(1).GetComponentInChildren<Toggle>();
    }

    void OnEnable()
    {
        Load();

        var sound = SettingsManager.Instance.SoundSettings;

        masterVolumeSlider.onValueChanged.AddListener(val => 
        { 
            sound.masterVolume = val;
            sound.Apply();
        });

        musicVolumeSlider.onValueChanged.AddListener(val =>
        {
            sound.musicVolume = val;
            sound.Apply();
        });

        sfxVolumeSlider.onValueChanged.AddListener(val =>
        {
            sound.sfxVolume = val;
            sound.Apply();
        });

        enablePreview.onValueChanged.AddListener(val =>
        {
            sound.enablePreview = val;
            sound.Apply();
        });
    }

    void OnDisable()
    {
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        enablePreview.onValueChanged.RemoveAllListeners();
        SettingsManager.Instance.SoundSettings.Save();
    }

    void Load()
    {
        SoundSettings soundSettings = SettingsManager.Instance.SoundSettings;
        masterVolumeSlider.value = soundSettings.masterVolume;
        musicVolumeSlider.value = soundSettings.musicVolume;
        sfxVolumeSlider.value = soundSettings.sfxVolume;
        enablePreview.isOn = soundSettings.enablePreview;
    }
}
