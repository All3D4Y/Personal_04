using UnityEngine;

public class UIManager : MonoBehaviour
{
    Canvas canvas;
    TouchPanel[] panels;
    ComboUI comboUI;

    void Awake()
    {
        canvas = FindAnyObjectByType<Canvas>();
        panels = canvas.GetComponentsInChildren<TouchPanel>();
        comboUI = canvas.GetComponentInChildren<ComboUI>();
    }

    void Start()
    {
        foreach (var panel in panels)
        {
            panel.onSwipe += HandleSwipe;
            panel.onHoldStart += HandleHoldStart;
            panel.onHoldEnd += HandleHoldEnd;
        }
        GameManager.Instance.NoteManager.ComboManager.onCombo += comboUI.ShowCombo;
    }

    void OnDisable()
    {
        foreach (var panel in panels)
        {
            panel.onSwipe -= HandleSwipe;
            panel.onHoldStart -= HandleHoldStart;
            panel.onHoldEnd -= HandleHoldEnd;
        }
        GameManager.Instance.NoteManager.ComboManager.onCombo -= comboUI.ShowCombo;
    }

    void HandleSwipe(int actionCode)
    {
        GameManager.Instance.HitZone.HitNote(actionCode);
    }

    void HandleHoldStart(int actionCode)
    {
        GameManager.Instance.HitZone.ToggleStart(actionCode);
    }

    void HandleHoldEnd(int actionCode)
    {
        GameManager.Instance.HitZone.ToggleEnd(actionCode);
    }
}
