using UnityEngine;
using UnityEngine.InputSystem;

public class Test_07 : TestBase
{
    public AudioClip clip;
    public AudioSource audioSource;
    public SoundVisualizer soundVisualizer;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        audioSource.clip = clip;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        audioSource.Play();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        soundVisualizer.transform.localScale *= soundVisualizer.scaleMultiplier;
    }
}
