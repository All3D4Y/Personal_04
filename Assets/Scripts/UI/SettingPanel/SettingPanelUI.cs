using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelUI : MonoBehaviour
{
    CanvasGroupBase[] panels = new CanvasGroupBase[3];
    Button[] buttons = new Button[3];

    void Awake()
    {
        Transform child = transform.GetChild(0);
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i] = child.GetChild(i).GetComponent<CanvasGroupBase>();
        }
        child = transform.GetChild(1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = child.GetChild(i).GetComponent<Button>();
        }
    }

    public void SwitchPanel(int index)
    {
        if (panels[index].CanvasGroup.alpha > 0) return;

        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
            {
                panels[i].OnVisible();
            }
            else
            {
                panels[i].OnTransparent();
            }
        }
    }
}
