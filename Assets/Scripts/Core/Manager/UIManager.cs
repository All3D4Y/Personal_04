using UnityEngine;

public class UIManager : MonoBehaviour
{
    TouchPanel[] panels;

    void Awake()
    {
        panels = FindAnyObjectByType<Canvas>().GetComponentsInChildren<TouchPanel>();
    }

    void Start()
    {
        foreach (var panel in panels)
        {
            panel.onSwipe += HandleSwipe;
            panel.onHoldStart += HandleHoldStart;
            panel.onHoldEnd += HandleHoldEnd;
        }
    }

    void OnDisable()
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
