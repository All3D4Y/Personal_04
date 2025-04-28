using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    TouchPanel[] panels;

    public static InputManager Instance => instance;

    void Awake()
    {
        panels = GetComponentsInChildren<TouchPanel>();

        if (instance == null)
            instance = this;
    }

    public void Initialize()
    {
        foreach (var panel in panels)
        {
            panel.onSwipe += HandleSwipe;
            panel.onHoldStart += HandleHoldStart;
            panel.onHoldEnd += HandleHoldEnd;
        }
    }

    public void CleanUp()
    {
        foreach (var panel in panels)
        {
            panel.onSwipe -= HandleSwipe;
            panel.onHoldStart -= HandleHoldStart;
            panel.onHoldEnd -= HandleHoldEnd;
        }
    }

    void HandleSwipe(int actionCode)
    {
        HitZone.Instance.HitNote(actionCode);
    }

    void HandleHoldStart(int actionCode)
    {
        HitZone.Instance.ToggleStart(actionCode);
    }

    void HandleHoldEnd(int actionCode)
    {
        HitZone.Instance.ToggleEnd(actionCode);
    }
}
