using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MusicPanel : MonoBehaviour
{
    MusicMetaData musicMetaData;
    Button button;
    Image coverImage;
    Image[] stars;
    TextMeshProUGUI musicName;
    TextMeshProUGUI bestScore;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPanelClick);
        coverImage = transform.GetChild(0).GetComponent<Image>();
        stars = new Image[7];
        for (int i = 0; i < 7; i++)
        {
            stars[i] = transform.GetChild(1).GetChild(i).GetComponent<Image>();
            stars[i].gameObject.SetActive(false);
        }
        musicName = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        bestScore = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }
    public void Initialize(MusicMetaData metaData)
    {
        musicMetaData = metaData;
        coverImage.sprite = musicMetaData.coverImage;
        musicName.text = $"{metaData.title} - {metaData.artist}";
        SetDifficulty(metaData.difficulty);
    }

    void SetDifficulty(int difficulty)
    {
        for (int i = 0; i < difficulty; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
    }

    void OnPanelClick()
    {
        throw new NotImplementedException();
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
