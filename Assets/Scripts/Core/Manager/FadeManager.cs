using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    public Sprite[] numSprites;
    public float fadeSpeed = 1.0f;
    public float loadingProgressSpeed = 1.0f;
    public float waitTime = 0.5f;

    CanvasGroup fadeImage;
    Image[] numImages;
    Slider loadingBar;
    AsyncOperation async;

    float loadingProgress;

    public Action onLoadComplete;

    void Update()
    {
        ShowNumber(loadingProgress * 100);
    }

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        fadeImage = GetComponentInChildren<CanvasGroup>();
        loadingBar = transform.GetChild(0).GetComponentInChildren<Slider>();
        numImages = new Image[3];
        for (int i = 0; i < numImages.Length; i++)
        {
            numImages[i] = transform.GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Image>();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartFadeOut();
        onLoadComplete?.Invoke();
    }

    void ShowNumber(float number)
    {
        int numInt = Mathf.Min(Mathf.RoundToInt(number), 100);
        string loadStr = numInt.ToString();
        int strCount = loadStr.Length;

        for (int i = 0; i < numImages.Length; i++)
        {
            if (i < strCount)
            {
                int num = loadStr[strCount - 1 - i] - '0';
                numImages[i].sprite = numSprites[num];
                numImages[i].gameObject.SetActive(true);
            }
            else
            {
                numImages[i].gameObject.SetActive(false);
            }
        }
        loadingBar.value = number * 0.01f;
    }

    public void SceneLoadWithFade(int sceneIndex)
    {
        ShowNumber(0.0f);
        StartCoroutine(FadeIn(sceneIndex));
        StartCoroutine(LoadingProgressCoroutine());
    }

    void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn(int sceneIndex)
    {
        if (!fadeImage.blocksRaycasts)
        {
            fadeImage.blocksRaycasts = true;
            async = SceneManager.LoadSceneAsync(sceneIndex);
            async.allowSceneActivation = false; 
        }
        while (fadeImage.alpha <= 0.999f)
        {
            fadeImage.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        fadeImage.alpha = 1.0f;

        if (async.progress >= 0.90f)
        {
            yield return new WaitForSeconds(waitTime);
            async.allowSceneActivation = true;
        }
    }

    IEnumerator FadeOut()
    {
        while (fadeImage.alpha >= 0.001f)
        {
            fadeImage.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        fadeImage.alpha = 0.0f;
        fadeImage.blocksRaycasts = false;
        async = null;
        loadingBar.value = 0.0f;
    }

    IEnumerator LoadingProgressCoroutine()
    {
        loadingProgress = 0;

        while (async.progress < 0.9f)
        {
            loadingProgress += Time.deltaTime * loadingProgressSpeed;
            yield return null;
        }
        float elapsedTime = 0.0f;
        float remainTime = (1 - loadingProgress) / loadingProgressSpeed;
        while (remainTime > elapsedTime)
        {
            elapsedTime += Time.deltaTime;
            loadingProgress += Time.deltaTime * loadingProgressSpeed;
            yield return null;
        }
    }
}
