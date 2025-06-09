using UnityEngine;
using UnityEngine.UI;

public class GamePlaySettingsUI : MonoBehaviour
{
    Button offsetButton;
    Toggle autoSkill;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        offsetButton = child.GetChild(0).GetComponentInChildren<Button>();
        autoSkill = child.GetChild(1).GetComponentInChildren<Toggle>();
    }

    void OnEnable()
    {
        Load();

        var gamePlay = SettingsManager.Instance.GamePlaySettings;
        autoSkill.onValueChanged.AddListener(val =>
        {
            gamePlay.autoSkill = val;
            gamePlay.Apply();
        });
    }

    void OnDisable()
    {
        autoSkill.onValueChanged.RemoveAllListeners();
        SettingsManager.Instance.GamePlaySettings.Save();
    }

    void Load()
    {
        autoSkill.isOn = SettingsManager.Instance.GamePlaySettings.autoSkill;
    }

    void OpenAdjustOffset()
    {

    }
}
