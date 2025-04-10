using UnityEngine;
using UnityEngine.UI;

public class JudgeUI : MonoBehaviour
{
    public AnimationCurve alphaCulve;

    Image[] images;

    void Awake()
    {
        images = new Image[transform.childCount];

        for (int i = 0; i < images.Length; i++)
        {
            images[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    void Start()
    {
        foreach (Image image in images)
        {
            image.enabled = false;
        }
    }


}
