using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    public Sprite[] numSprites;

    Image[] numImages;
    Image comboTextImage;

    void Awake()
    {
        numImages = new Image[transform.GetChild(0).childCount];

        for (int i = 0; i < numImages.Length; i++)
        {
            numImages[i] = transform.GetChild(0).GetChild(i).GetComponent<Image>();
            numImages[i].gameObject.SetActive(false);
        }
        comboTextImage = transform.GetChild(1).GetComponent<Image>();
        comboTextImage.enabled = false;
    }

    public void ShowCombo(int comboCount)
    {
        if (comboCount <= 0)
        {
            comboTextImage.enabled = false;
            foreach (var img in numImages)
                img.gameObject.SetActive(false);
            return;
        }
        else
        {
            comboTextImage.enabled = true;

            string comboStr = comboCount.ToString();
            int strCount = comboStr.Length;

            for (int i = 0; i < numImages.Length; i++)
            {
                if (i < strCount)
                {
                    int num = comboStr[strCount - 1 - i] - '0';
                    numImages[i].sprite = numSprites[num];
                    numImages[i].gameObject.SetActive(true);
                }
                else
                {
                    numImages[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
