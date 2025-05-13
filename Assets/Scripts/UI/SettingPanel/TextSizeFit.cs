using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSizeFit : MonoBehaviour
{
    TextMeshProUGUI[] texts;

    void Awake()
    {
        texts = new TextMeshProUGUI[transform.childCount];
        float textSize = 0;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            if (i == 0)
            {
                textSize = texts[i].fontSize;
            }
            else
            {
                texts[i].fontSize = textSize;
            }
        }

    }
}
