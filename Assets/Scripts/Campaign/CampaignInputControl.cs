using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CampaignInputControl : MonoBehaviour
{
    public Camera mainCamera;

    public Action<Transform> onTouch;

    static CampaignInputControl instance;

    IDisposable touchSubscription;

    public static CampaignInputControl Instance => instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void OnEnable()
    {
        touchSubscription = InputSystem.onAnyButtonPress.Call(OnInputReceived);
    }
    void OnDisable()
    {
        touchSubscription.Dispose();
        touchSubscription = null;
    }

    void OnInputReceived(InputControl control)
    {
        Vector2 screenPosition = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            return;
        }


        if (screenPosition == Vector2.zero) return;

        Camera cam = mainCamera != null ? mainCamera : Camera.main;
        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Billboard"))
            {
                OnBillboardTouched(hit.transform);
            }
        }
    }

    void OnBillboardTouched(Transform billboard)
    {
        StagePrefab stage = billboard.parent.parent.GetComponent<StagePrefab>();
        // stage.stageData 스테이지 데이터 매니저에 저장하기
        onTouch?.Invoke(stage.transform);
    }
}
