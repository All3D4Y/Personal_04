using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public float flashSpeed;

    Button button;
    TextMeshProUGUI text;

    void Awake()
    {
        button = transform.GetChild(0).GetComponent<Button>();
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        button.onClick.AddListener(CloseTitle);
        StartCoroutine(FlashTextCoroutine());
    }

    void OnDisable()
    {
        button?.onClick.RemoveListener(CloseTitle);
        StopAllCoroutines();
    }

    void CloseTitle()
    {

    }

    IEnumerator FlashTextCoroutine()
    {
        float elapsedTime = 0;
        while (true)
        {
            float textAlpha = 0.25f * (Mathf.Sin(elapsedTime) + 3);
            Color textColor = new Color(1, 1, 1, textAlpha);
            text.color = textColor;
            elapsedTime += Time.deltaTime * flashSpeed;
            yield return null;
        }
    }
}
