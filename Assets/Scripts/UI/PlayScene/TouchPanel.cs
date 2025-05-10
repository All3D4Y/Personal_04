using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float swipeThreshold = 50.0f;
    public float holdTimeThreshold = 0.5f; // 홀드 감지 시간 (초 단위)

    Vector2 touchStartPos;
    Vector2 touchEndPos;
    Vector2 dir;

    bool isSwipeDetected = false; // 스와이프 감지 여부를 추적
    bool isHolding = false;
    bool holdStarted = false;
    float holdStartTime;

    int siblingIndex;
    int touchId = -1;

    public Action<int> onSwipe;
    public Action<int> onHoldStart;
    public Action<int> onHoldEnd;

    void Awake()
    {
        siblingIndex = transform.GetSiblingIndex();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 터치 또는 클릭 시작 지점 기록
        touchStartPos = eventData.position;
        isSwipeDetected = false; // 새로운 터치가 시작되면 감지를 초기화
        isHolding = true;
        holdStarted = false;
        holdStartTime = Time.time;
        touchId = eventData.pointerId; // 현재 터치 ID 저장
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != touchId)     // 터치 ID가 다르면 무시 (다른 패널에서 발생한 드래그일 수도 있음)
            return;
        
        if (isSwipeDetected)                    // 이미 스와이프가 감지되었으면 추가적으로 감지하지 않음
            return;

        // 드래그 중에는 끝 지점이 계속 갱신됨
        touchEndPos = eventData.position;
        dir = touchEndPos - touchStartPos;

        // 스와이프가 일정 거리를 넘었을 때만 감지
        if (Mathf.Abs(dir.y) >= swipeThreshold)
        {
            DetectSwipe();
            isSwipeDetected = true; // 스와이프가 감지되었으므로, 이후에는 감지하지 않음
            CancelHold();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 터치 ID가 다르면 무시
        if (eventData.pointerId != touchId)
            return;

        if (isHolding && holdStarted)
        {
            onHoldEnd?.Invoke(siblingIndex * 3 + 2);
        }

        isHolding = false;
        touchId = -1; // 터치 해제
    }

    void Update()
    {
        if (isHolding && !holdStarted && Time.time - holdStartTime >= holdTimeThreshold)
        {
            onHoldStart?.Invoke(siblingIndex * 3 + 2);
            isHolding = false; // 한 번만 실행되도록 설정
        }
    }

    // 수직 스와이프 방향 감지
    void DetectSwipe()
    {
        if (dir.y > 0)
        {
            // 위쪽 스와이프
            SwipeUp();
        }
        else
        {
            // 아래쪽 스와이프
            SwipeDown();
        }
    }

    void SwipeUp()
    {
        // 위쪽 스와이프 시 실행할 기능을 여기에 추가
        onSwipe?.Invoke(siblingIndex * 3);
    }

    void SwipeDown()
    {
        // 아래쪽 스와이프 시 실행할 기능을 여기에 추가
        onSwipe?.Invoke(siblingIndex * 3 + 1);
    }

    void CancelHold()
    {
        if (isHolding && holdStarted)
        {
            onHoldEnd?.Invoke(siblingIndex * 3 + 2);
        }
        isHolding = false;
    }
}
