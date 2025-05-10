using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JudgeUI : MonoBehaviour
{
    public float popTime = 0.1f;
    public AnimationCurve alphaCulve;
    public AnimationCurve sizeCulve;

    Image[] images;

    void Awake()
    {
        images = new Image[transform.childCount];

        for (int i = 0; i < images.Length; i++)
        {
            images[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void Initialize()
    {
        TransparentUI();
        HUDManager.Instance.GameHUDViewModel.onHitNote += Judge;
    }

    public void CleanUp()
    {
        HUDManager.Instance.GameHUDViewModel.onHitNote -= Judge;
    }

    void Start()
    {
        TransparentUI();
    }

    void TransparentUI()
    {
        if (images != null)
        {
            foreach (Image image in images)
            {
                image.enabled = false;
                image.SetNativeSize();
            } 
        }
    }

    void Judge(JudgeEnum judge)
    {
        StopAllCoroutines();
        TransparentUI();
        StartCoroutine(JudgeCoroutine(judge));
    }

    IEnumerator JudgeCoroutine(JudgeEnum judge)
    {
        Image judgeImage = images[(int)judge];
        judgeImage.enabled = true;
        float j_time = 0.0f;
        float j_ratio = 1 / popTime;
        while (j_time < popTime)
        {
            float alpha = alphaCulve.Evaluate(j_time * j_ratio);
            float size = sizeCulve.Evaluate(j_time * j_ratio);
            Color j_color = new Color(1, 1, 1, alpha);
            judgeImage.color = j_color;
            judgeImage.transform.localScale = new Vector3(size, size);
            j_time += Time.deltaTime;
            yield return null;
        }
        judgeImage.enabled = false;
    }

#if UNITY_EDITOR
    public void TestJudge(JudgeEnum judge)
    {
        Judge(judge);
    }
#endif
}
