using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSettingsUI : MonoBehaviour
{
    TMP_Dropdown language;
    Toggle[] qualities = new Toggle[3];
    Toggle[] fpss = new Toggle[3];
    Slider sfxBrightness;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        language = child.GetChild(0).GetComponentInChildren<TMP_Dropdown>();
        for (int i = 0; i < qualities.Length; i++)
        {
            qualities[i] = child.GetChild(1).GetChild(1).GetChild(i).GetComponent<Toggle>();
        }
        for (int i = 0; i < fpss.Length; i++)
        {
            fpss[i] = child.GetChild(2).GetChild(1).GetChild(i).GetComponent<Toggle>();
        }
        sfxBrightness = child.GetChild(3).GetComponentInChildren<Slider>();
    }

    void OnEnable()
    {
        Load();

        language.onValueChanged.AddListener(val =>
        {
            var lang = SettingsManager.Instance.LanguageSettings;
            lang.currentLanguage = (Language)val;
            lang.Apply();
        });

        var graphics = SettingsManager.Instance.GraphicSettings;

        foreach (var quality in qualities)
        {
            quality.onValueChanged.AddListener(val =>
            {
                if (val)
                {
                    int index = GetToggleIndex(qualities);
                    if (index > -1)
                    {
                        graphics.qualities = (GraphicQualities)index;
                        graphics.Apply();
                    }
                }
            });
        }

        foreach (var fps in fpss)
        {
            fps.onValueChanged.AddListener(val =>
            {
                if (val)
                {
                    int index = GetToggleIndex(fpss);
                    if (index > -1)
                    {
                        index = (int)(30 * Mathf.Pow(2, index));
                        graphics.fps = index;
                        graphics.Apply();
                    }
                }
            });
        }

        sfxBrightness.onValueChanged.AddListener(val =>
        {
            graphics.brightness = val;
            graphics.Apply();
        });
    }

    void OnDisable()
    {
        language.onValueChanged.RemoveAllListeners();
        foreach (var quality in qualities)
        {
            quality.onValueChanged.RemoveAllListeners();
        }
        foreach (var fps in fpss)
        {
            fps.onValueChanged.RemoveAllListeners();
        }
        sfxBrightness.onValueChanged.RemoveAllListeners();

        SettingsManager.Instance.GraphicSettings.Save();
        SettingsManager.Instance.LanguageSettings.Save();
    }

    void Load()
    {
        GraphicSettings graphics = SettingsManager.Instance.GraphicSettings;
        int temp = (int)graphics.qualities;
        qualities[temp].isOn = true;
        temp = graphics.fps;
        switch (temp)
        {
            case 30:
                temp = 0;
                break;
            case 60:
                temp = 1;
                break;
            case 120:
                temp = 2;
                break;
        }
        fpss[temp].isOn = true;
        sfxBrightness.value = graphics.brightness;
    }

    int GetToggleIndex(Toggle[] toggles)
    {
        int result = -1;
        Toggle target = null;
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
                target = toggle;
        }
        result = target.transform.GetSiblingIndex();

        return result;
    }
}
