using UnityEngine;

public class MainMenuScrollController : SnapScrollController
{
    [Header("Animator")]
    public Animator statAnim;

    bool isOn
    {
        get => statAnim.GetBool("isOn");
        set
        {
            if (value != isOn)
            {
                statAnim.SetBool("isOn", value);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (currentIndex == 3)
            isOn = true;
        else
            isOn = false;
    }
}
