using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_TouchIA : MonoBehaviour
{
    TouchIA inputActions;

    void Awake()
    {
        inputActions = new TouchIA();
    }

    void OnEnable()
    {
        inputActions.Touch.Enable();
    }

    void OnDisable()
    {
        inputActions.Touch.Disable();
    }
}
