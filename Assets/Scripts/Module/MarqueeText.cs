using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Mask))]
public class MarqueeText : MonoBehaviour
{
    public TextMeshProUGUI movingText;
    public float scrollSpeed = 50f;
    public float delayBeforeStart = 1f;
    public float pauseAtEnd = 1f;

    RectTransform containerRect;
    RectTransform textRect;

    float startX;
    float endX;

    void Awake()
    {
        containerRect = GetComponent<RectTransform>();
        textRect = movingText.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        StartCoroutine(ScrollLoop());
    }

    IEnumerator ScrollLoop()
    {
        yield return null; // Layout이 완전히 계산될 때까지 대기

        float containerWidth = containerRect.rect.width;
        float textWidth = movingText.preferredWidth + 100.0f;

        if (textWidth <= containerWidth)
        {
            textRect.anchoredPosition = Vector2.zero;
            yield break;
        }

        startX = 0f;
        endX = -(textWidth - containerWidth);

        while (true)
        {
            // 시작 딜레이
            yield return new WaitForSeconds(delayBeforeStart);

            // 위치 초기화
            textRect.anchoredPosition = new Vector2(startX, 0);

            // 왼쪽으로 스크롤
            while (textRect.anchoredPosition.x > endX)
            {
                textRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
                yield return null;
            }

            // 끝에 도달 후 대기
            yield return new WaitForSeconds(pauseAtEnd);
        }
    }

    void OnDisable()
    {
        if (textRect != null)
            textRect.anchoredPosition = Vector2.zero;
    }
}
