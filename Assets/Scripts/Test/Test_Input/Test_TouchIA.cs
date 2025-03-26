using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_TouchIA : MonoBehaviour
{
    public float swipeThreshold = 0.09f;
    
    TouchIA inputActions;

    Vector2 touchStartPos;
    Vector2 touchEndPos;
    Vector2 swipeDelta;

    bool IsSwipeUp => swipeDelta.y >= 0;

    void Awake()
    {
        inputActions = new TouchIA();
    }

    void OnEnable()
    {
        inputActions.Touch.Enable();
        inputActions.Touch.Swipe.started += OnTouchStart;
        inputActions.Touch.Swipe.performed += OnSwipe;
    }

    void OnDisable()
    {
        inputActions.Touch.Swipe.started -= OnTouchStart;
        inputActions.Touch.Swipe.performed -= OnSwipe;
        inputActions.Touch.Disable();
    }

    void OnTouchStart(InputAction.CallbackContext context)
    {
        touchStartPos = context.ReadValue<Vector2>();
    }

    void OnSwipe(InputAction.CallbackContext context)
    {
        touchEndPos = context.ReadValue<Vector2>();
        swipeDelta = touchEndPos - touchStartPos;

        // 스와이프가 일정 거리를 넘었는지 확인
        if (swipeDelta.sqrMagnitude >= swipeThreshold)
        {
            if (IsSwipeUp)
            {
                Debug.Log("Swipe Up");
            }
            else
            {
                Debug.Log("Swipe Down");
            }
        }
    }
}
