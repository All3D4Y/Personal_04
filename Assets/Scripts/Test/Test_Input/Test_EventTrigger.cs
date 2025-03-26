using UnityEngine;
using UnityEngine.EventSystems;

public class Test_EventTrigger : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public string side = "Right";

    public float swipeThreshold = 1.0f;

    Vector2 touchStartPos;
    Vector2 touchEndPos;
    Vector2 dir;

    bool isSwipeDetected = false; // 스와이프 감지 여부를 추적

    public void OnPointerDown(PointerEventData eventData)
    {
        // 터치 또는 클릭 시작 지점 기록
        touchStartPos = eventData.position;
        isSwipeDetected = false; // 새로운 터치가 시작되면 감지를 초기화
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이미 스와이프가 감지되었으면 추가적으로 감지하지 않음
        if (isSwipeDetected)
            return;

        // 드래그 중에는 끝 지점이 계속 갱신됨
        touchEndPos = eventData.position;
        dir = touchEndPos - touchStartPos;

        // 스와이프가 일정 거리를 넘었을 때만 감지
        if (Mathf.Abs(dir.y) >= swipeThreshold)
        {
            DetectSwipe();
            isSwipeDetected = true; // 스와이프가 감지되었으므로, 이후에는 감지하지 않음
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 터치가 끝난 후 추가 처리
        Debug.Log("드래그 종료");
    }

    // 수직 스와이프 방향 감지 (위/아래만 감지)
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
        Debug.Log($"{side} : Swipe Up");
        // 위쪽 스와이프 시 실행할 기능을 여기에 추가
    }

    void SwipeDown()
    {
        Debug.Log($"{side} : Swipe Down");
        // 아래쪽 스와이프 시 실행할 기능을 여기에 추가
    }
}
